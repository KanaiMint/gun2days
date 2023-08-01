using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float MaxHP = 10;
    public float HP = 10;

    public GameObject bullet;
    float ShootColltime = 0.3f;

    const float KShootColltime = 0.3f;
    public SpriteRenderer spriteRenderer;
    float ChargeTime = 0.0f;
    const float KChargeTime = 0.2f;
    private Vector3 originalChildScale;

    public TextMeshProUGUI hpText;
    public SpriteRenderer HPsprite;
    public SpriteRenderer Zandansprite;
    public GameObject particle;
    int time;
    public CameraScript cameraScript;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private Camera mainCamera;
    public Rigidbody2D rb;
    float damagedtime = 0.0f;
    public float MoveSpeed;
    public RightCanChargeScript rightchargeScript;
    public LeftCanChargeScript leftchargeScript;
    // Start is called before the first frame update
    void Start()
    {
        // spriteRenderer=GetComponent<SpriteRenderer>();
        // 子オブジェクトの元のスケールを保存
        originalChildScale = transform.GetChild(0).localScale;
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 || transform.position.y < mainCamera.transform.position.y - 10.0f) 
        {
            cameraScript.IsGameOver = true;
        }
        else
        {
            if (damagedtime > 0.0f)
            {
                spriteRenderer.color = Color.red;
                damagedtime -= Time.deltaTime;
            }
            else { spriteRenderer.color = Color.white; }

           
            if (Input.GetKey(KeyCode.J) && ShootColltime <= 0.0f && HP > 1)
            {
                HP -= 1;
                GameObject bullet_;
                bullet_ = Instantiate(bullet);
                bullet_.transform.position = transform.position;
                bullet_.transform.parent = transform.parent;
                ShootColltime = KShootColltime;
                audioSource.Play();
            }
            else
            if (Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J)&& !(rightchargeScript.isWallIN&&leftchargeScript.isWallIN))
            {
                MoveSpeed = 2.5f;
                ChargeTime += Time.deltaTime;
                time++;
                Vector3 rand = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10), 0);
                if (time % 8 == 0)
                {
                    GameObject particle_;
                    particle_ = Instantiate(particle, rand, Quaternion.identity);
                    particle_.transform.parent = transform.parent;
                }
                    
                if(time%100==0) { audioSource.PlayOneShot(audioClip); }
            }
            else { MoveSpeed = 5.0f; }
            if ((ChargeTime > KChargeTime) && HP < MaxHP)
            {
                HP += 1;
                ChargeTime = 0.0f;
            }
            ShootColltime -= Time.deltaTime;

            float spriteScalX = (HP / 10);

            spriteRenderer.transform.localScale = new Vector3(spriteScalX, 1.0f, 0);
            //transform.GetChild(0).localScale = originalChildScale;
            //transform.GetChild(1).localScale = originalChildScale;
            //transform.GetChild(2).localScale = originalChildScale;
        }

        hpText.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.2f, -0.8f, 0f));
        hpText.text = HP.ToString("F0") + "/" + MaxHP.ToString("F0"); // 小数点以下1桁まで表示
        HPsprite.transform.position = (transform.position + new Vector3(-1.5f, -1.0f, 0f));
        Zandansprite.transform.position = (transform.position + new Vector3(1.5f, -0.8f, 0f));

    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
            //rb.velocity += new Vector2(-5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
            //rb.velocity += new Vector2(5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, MoveSpeed, 0) * Time.deltaTime;
            //rb.velocity += new Vector2(0, 5) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -MoveSpeed, 0) * Time.deltaTime;
            //rb.velocity +=new Vector2(0, -5) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            MaxHP -= 1;
            HP -= 1;
            damagedtime = 0.2f;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            MaxHP -= 2;
            HP -= 2;
            damagedtime = 0.2f;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("zandan"))
        {
            MaxHP += 1;
            Destroy(collision.gameObject);

        }
    }
    private bool InCamera()
    {
        // オブジェクトの位置をViewport座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport座標がカメラの範囲内かどうかを判定
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 0.95f &&
                         viewportPosition.z > 0f;



        return isInCamera;
    }
}
