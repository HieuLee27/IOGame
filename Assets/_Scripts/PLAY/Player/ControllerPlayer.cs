using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private Joystick joystickScript; //Script của joystick
    [SerializeField] private float speed = 5f; //Tốc độ di chuyển của player
    public Slider sliderMana; //Thanh cấp độ của mana
    public TMP_Text amountOfCoin_text; //Số lượng coin đã thu được   
    public int amountOfCoin; //Số lượng coin có trong game
    internal int level; //Cấp độ của player

    [Header("Thông số cơ bản")]
    public float health; //Máu của player
    internal bool isLive; //Trạng thái sống hay chết
    public Slider sliderHealth; //Thanh cấp độ của máu
    public Vector3 offer;

    //Các biến cần thiết cho hoạt ảnh
    private Rigidbody2D rb; //Rigidbody của player
    private Animator anim; //Animator của player
    private int parameter; //Tham số để chuyển động của player giữa các animationClip
    private SpriteRenderer sp; //SpriteRenderer của player để lật hình

    public BSPMapGenerator room;


    private void Start()
    {
        StartCoroutine(nameof(ResetPos));
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        parameter = 2;
        isLive = true;

        level = 1;
        amountOfCoin = int.Parse(amountOfCoin_text.text); //Số lượng coin có trong game
        Debug.Log("Current Coin: " + amountOfCoin); //Hiển thị số lượng coin đã thu được
    }

    private void FixedUpdate()
    {
        Move();
        AnimationPlayer();
        FlipSprite();
        ResetValue();
    }

    private void LateUpdate()
    {
        UpdatePosSlider(); //Cập nhật vị trí của slider health
    }

    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = room.rooms[0].center;
    }

    private void Move() //Hàm di chuyển player
    {
        if (joystickScript.Direction.x != 0 || joystickScript.Direction.y != 0) //Nhận lệnh di chuyển từ joystick
        {
            rb.velocity = new Vector2(joystickScript.Direction.x, joystickScript.Direction.y).normalized * speed;
        }
        else //Dừng player khi không nhận lệnh từ joystick
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void AnimationPlayer()  //Hàm chuyển động của player
    {

        if (Math.Abs(joystickScript.Direction.x) > Math.Abs(joystickScript.Direction.y)) //Nếu giá trị x của joystick lơn hơn giá trị y của joystick
                                                                                         //-> hoạt ảnh di chuyển trái được phát
        {
            parameter = 0; //Tham số để chuyển động của player đến animationClip di chuyển trái
        }
        else if (joystickScript.Direction.y > 0) //Nếu giá trị y của joystick lớn hơn 0
                                                 //-> hoạt ảnh di chuyển lên được phát
        {
            parameter = 1; //Tham số để chuyển động của player đến animationClip di chuyển lên
        }
        else if (joystickScript.Direction.y < 0) //Nếu giá trị y của joystick lớn hơn 0
                                                 //-> hoạt ảnh di chuyển xuống được phát
        {
            parameter = -1; //Tham số để chuyển động của player đến animationClip di chuyển xuống
        }
        anim.SetInteger("Move", parameter); //Phát hoạt ảnh di chuyển của player
    }

    private void FlipSprite() //Hàm lật hình của player
    {
        if (parameter == 0) //Nếu player di chuyển trái
        {
            if(joystickScript.Direction.x < 0) 
            {
                sp.flipX = false; //Hình của player không bị lật
            }
            else if (joystickScript.Direction.x > 0)
            {
                sp.flipX = true; //Hình của player bị lật
            }
        }
        if (health < 0.1f) //Nếu máu của player nhỏ hơn 0.1
        {
            anim.SetTrigger("Die"); //Phát hoạt ảnh chết của player
            rb.velocity = Vector2.zero; //Player không di chuyển
            gameObject.GetComponent<Attack>().canAttack = false; //Player không thể tấn công
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Xử lý va chạm với mana và coin
    {
        if (collision.gameObject.CompareTag("Mana")) //Nếu player va chạm với mana
        {
            float valueLevel = (float)collision.gameObject.GetComponent<DestroyMana>().powerUp / (10 * level); //Giá trị cấp độ của mana
            sliderMana.value += valueLevel; //Tăng giá trị của slider mana
        }
        if (collision.gameObject.CompareTag("Coin")) //Nếu player va chạm với coin
        {
            amountOfCoin += 10; //Tăng số lượng coin hiện tại lên 10
            amountOfCoin_text.text = (int.Parse(amountOfCoin_text.text) + 10).ToString(); //Hiển thị số lượng coin đã thu được
        }
    }

    private void ResetValue() //Hàm reset giá trị của slider
    {
        if (sliderMana.value == 1) //Tăng cấp độ của player khi giá trị của slider đạt 1
        {
            level += 1;
        }
    }

    public void CheckLife() //Hàm kiểm tra mạng của player
    {
        ManagerGame.instance.result = ManagerGame.Results.Lose; //Kết quả game là thất bại
    }

    private void UpdatePosSlider(){
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position); //Lấy vị trí của player trên
        sliderHealth.transform.position = pos + offer; //Đặt vị trí của slider health trên player
    }
}
