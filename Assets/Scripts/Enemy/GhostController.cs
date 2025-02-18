using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : EnemyController
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject silverChest;
    [SerializeField] private GameObject goldChest;
    private Slider bloodBar;

    [SerializeField] private float distanceKeeping;
    public float timeExist;
    internal float timeAppear, timeDisappear;

    private int count;
    private float defaultBlood;

    internal bool isAppear;
    public static GhostController instance;

    public override void Awake()
    {
        base.Awake();
        timeAppear = Time.time;
        defaultBlood = health;
    }

    private void Start()
    {
        bloodBar = GameObject.Find("BloodOfBoss").GetComponent<Slider>();
        isAppear = true;
        count = 0;
    }

    public override void Update()
    {
        base.Update();
        bloodBar.value = health/defaultBlood;
        AnimDisappear();
        DestroyEnemyAndSpawnMana();
        instance = this;
    }

    public override void CatchPlayer() // Đuổi theo player
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= distanceKeeping)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            base.CatchPlayer();
        }
    }

    private void SpawnBullet() // Bắn đạn
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= distanceKeeping + 2f)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = CommonMethod.GetDirection(gameObject, player).normalized * newBullet.GetComponent<Bullet>().speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Xử lý va chạm với cây, gỗ
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Wood"))
        {
            transform.position += (Vector3)CommonMethod.GetDirection(gameObject, collision.gameObject).normalized;
        }
    }

    private void AnimDisappear() // Xử lý hiệu ứng biến mất
    {
        if (Time.time > timeAppear + timeExist && count < 2)
        {
            anim.SetTrigger("Disappear");
        }
    }

    private void Disappear() // Xử lý biến mất
    {
        if (count < 2 && Time.time > timeAppear + timeExist && isAppear)
        {
            count++;
            timeDisappear = Time.time;
            gameObject.SetActive(false);
            bloodBar.gameObject.SetActive(false);
            isAppear = false;
            Debug.LogFormat("TimeDisappear = {0}", timeDisappear);
            ManagerGame.instance.modeBoss = false;
        }
    }

    private void SpawnBullets(int bulletPattern) // Bắn đạn theo mẫu
    {
        int bulletCount = 8; // Số lượng viên đạn mặc định
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        switch (bulletPattern)
        {
            case 1: // Bắn đạn vòng tròn
                bulletCount = 8;
                break;

            case 2: // Bắn đạn theo hình nửa vòng tròn phía trước
                bulletCount = 5;
                angle = (Mathf.Atan2(player.transform.position.x, player.transform.position.y) * Mathf.Rad2Deg) - 90f;
                angleStep = 180f / (bulletCount - 1);
                break;

            case 3: // Bắn đạn theo hình chữ thập (4 viên đạn)
                bulletCount = 4;
                angleStep = 90f;
                break;

            default:
                return;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 bulletMoveDirection = new(bulletDirX, bulletDirY, 0f);

            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = bulletMoveDirection * newBullet.GetComponent<Bullet>().speed;

            angle += angleStep; // Cộng góc để tạo hiệu ứng hình tròn / nửa vòng tròn / chữ thập
        }
    }

    public void Attacking() // Xử lý tấn công
    {
        if (count == 0)
        {
            SpawnBullet();
        }
        else if(count >= 1)
        {
            int indexY = Random.Range(0, 9);
            if(indexY == 0)
            {
                SpawnBullet();
            }
            else
            {
                int index = Random.Range(1, 4);
                SpawnBullets(index);
            }
        }
    }

    public override void DestroyEnemyAndSpawnMana() // Xử lý khi enemy bị hủy
    {
        if (health <= 0.05f)
        {
            int number1 = Random.Range(0, 5);
            for (int i = 0; i < number1; i++)
            {
                Instantiate(mana, transform.position,
                    Quaternion.identity);
            }
            int number2 = Random.Range(0, 3);
            for (int i = 0; i < number2; i++)
            {
                Instantiate(coin, transform.position,
                    Quaternion.identity);
            }
            if (count < 2)
            {
                Instantiate(silverChest, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(goldChest, transform.position, Quaternion.identity);
            }
            ManagerGame.instance.result = ManagerGame.Results.Win;
            Destroy(gameObject);
        }
    }
}