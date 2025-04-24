using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Attack : MonoBehaviour
{
    [Header("Thông số tấn công")]
    public List<GameObject> dart; //Danh sách phi tiêu
    [SerializeField] private float timeShot; //Thời gian bắn đạn
    [SerializeField] private float visibility; //Khoảng cách nhìn thấy enemy
    [SerializeField] GameObject enemy; //Kẻ thù để lấy tag
    [SerializeField] private float force; //Lực ném phi tiêu

    internal bool canAttack; //Biến kiểm tra có thể tấn công hay không

    [Header("Âm thanh")]
    public AudioSource source;
    public AudioClip shotAudio;

    internal float levelOfDart; //Cấp độ phi tiêu

    //Biến lưu trữ vị trí mục tiêu
    private Vector3 targetPos;
    private GameObject[] listEnemy;

    //Biến lưu trữ khoảng cách tấn công enemy gần nhất
    private float minDistance;

    private void Awake()
    {
        targetPos = Vector3.zero;
        levelOfDart = 0;
        canAttack = true;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Shoting), 0.5f, timeShot); //Gọi hàm bắn đạn sau mỗi khoảng thời gian timeShot
    }

    private void Shoting() //Hàm bắn đạn
    {
        listEnemy = GameObject.FindGameObjectsWithTag(enemy.tag); //Danh sách enemy có trong Scene
        if(listEnemy.Length != 0 && canAttack)
        {
            minDistance = Vector2.Distance(listEnemy[0].transform.position, transform.position); //Đặt giá trị mặc định khoảng cách nhỏ nhất
            foreach (var enemy in listEnemy) //Tìm enemy gần nhất
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) <= minDistance)
                {
                    minDistance = Vector2.Distance(enemy.transform.position, transform.position);
                    targetPos = enemy.transform.position;
                }
            }
            if (minDistance <= visibility)
            {
                GameObject dartInstance = Instantiate(dart[(int)levelOfDart], transform.position, transform.rotation); //Bắn phi tiêu theo cấp độ hiện tại
                source.PlayOneShot(shotAudio); //Phát âm thanh ném phi tiêu
                dartInstance.GetComponent<Rigidbody2D>().velocity = (targetPos - transform.position).normalized * force; //Thêm lực ném, và hướng ném phi tiêu
            }
        }
    }
}
