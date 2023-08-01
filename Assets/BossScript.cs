using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class BossScript : MonoBehaviour
{
    private Camera mainCamera;
    public bool isOnCamera = false;
    public GameObject BreakBox;
    public GameObject Enemy;
    public float Xpos=2.0f;
    bool Isreturn = false;
   public int HP=20;
    const int MaxHP = 20;
    const float KAttackTime = 7.0f;
   public float AttackTime = 2.0f;
    public int Rand;
    public SpriteRenderer spriteRenderer;
    float damagedtime = 0.0f;
    public CameraScript cameraScript;
    public Slider BosHpBar;
    public int BulletNum= 0;
    public GameObject player;
    public GameObject tile;
    public GameObject particle;
    public GameObject bullet;
        float DrainTime=0;
        float KDrainTime=5.0f;
        bool isAttack;
    int time=0;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        BosHpBar.value = 1;
        HP = MaxHP;
        BosHpBar.gameObject.SetActive(false);
     
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            cameraScript.IsGameClear = true;
            gameObject.SetActive(false);
        }
        else
        {
            BosHpBar.value = (float)HP / (float)MaxHP;
            if (damagedtime > 0.0f)
            {
                spriteRenderer.color = Color.red;
                damagedtime -= Time.deltaTime;
            }
            else { spriteRenderer.color = Color.white; }

            if (InCamera())
            {
                BosHpBar.gameObject.SetActive(true);
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
                //if (Isreturn)
                //{
                //    transform.position += new Vector3(Xpos, 0, 0) * Time.deltaTime;
                //    if (transform.position.x > 7.0f)
                //    {
                //        Isreturn = false;
                //    }
                //}
                //if (!Isreturn)
                //{
                //    transform.position += new Vector3(-Xpos, 0, 0) * Time.deltaTime;
                //    if (transform.position.x < -7.0f)
                //    {
                //        Isreturn = true;
                //    }
                //}
                Vector2 tmp = (Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime*40.0f));
                if (!isAttack)
                {
                 transform.position = new Vector3(tmp.x,transform.position.y, 0);
                 Rand = Random.Range(0, 9);
                }
                AttackTime -= Time.deltaTime;

            }
            if (HP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void FixedUpdate()
    {
                AttckMove();

    }
    private void AttckMove()
    {
        if (AttackTime <= 0)
        {
            if (Rand == 0|| Rand == 1)
            {
            isAttack = true;
                GameObject Breakbox;
                Breakbox = Instantiate(BreakBox, transform.position, Quaternion.identity);
                Breakbox.transform.parent = transform.parent;
                AttackTime = 1.5f;
            }
            else if (Rand == 2|| Rand == 3)
            {
            isAttack = true;
                GameObject enemy;
                enemy = Instantiate(Enemy, transform.position, Quaternion.identity);
                enemy.transform.parent = transform.parent;
                AttackTime = KAttackTime;
            }
            else if (Rand == 4 || Rand == 5 || Rand == 6 || Rand == 7)
            {
            isAttack = true;
                GameObject Tile;
                Tile=Instantiate(tile, transform.position,Quaternion.identity);
                Tile.transform.parent = transform.parent;
                AttackTime = 1.5f;

            }
            else if(Rand == 8 )
            {
            isAttack = true;
                DrainTime += Time.deltaTime;
                if(DrainTime>=KDrainTime)
                {
                   
                        if (time % 10 == 0&&BulletNum>0)
                        {
                            GameObject bullet_;
                            bullet_ = Instantiate(bullet);
                            bullet_.transform.position = transform.position;
                            bullet_.transform.position=new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + 0.5f);
                            bullet_.transform.parent = transform.parent;
                            BulletNum--;
                        }
                    
                    if (BulletNum <= 0)
                    {
                        AttackTime = 2.0f;
                        DrainTime = 0.0f;
                        Rand = -1;
                        BulletNum = 0;
                    }
                }
                else
                {
                    Vector3 rand = new Vector3(transform.position.x + Random.Range(-7.0f,7.0f), transform.position.y + Random.Range(-15, -15), 0);
                    Vector3 randpos = new Vector3(transform.position.x + Random.Range(-0.1f,0.1f), transform.position.y + Random.Range(-0.1f,0.1f), 0);
                    if (time % 5 == 0)
                    {
                        GameObject particle_;
                        particleScript particleScript_;
                        particle_ = Instantiate(particle, rand, Quaternion.identity);
                        particleScript_ = particle_.GetComponent<particleScript>();
                        particleScript_.lifetime = 3.0f;
                        particle_.transform.parent = transform.parent;
                        Vector3 tmp = (transform.position - particle_.transform.position).normalized*1.0f;
                        particleScript_.vel = tmp;
                        transform.position = randpos;
                    }
                }
                    time++;
            }
        }
        else
        {
            isAttack = false;
        }
    }

    private bool InCamera()
    {
        // オブジェクトの位置をViewport座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport座標がカメラの範囲内かどうかを判定
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 0.9f &&
                         viewportPosition.z > 0f;

        // カメラに映っている場合の処理
        if (isInCamera)
        {
            isOnCamera = true; // カメラに映っていることを示すbool変数をtrueに設定

            Debug.Log("Object is in camera view!");
        }
        else
        {
            isOnCamera = false; // カメラに映っていないことを示すbool変数をfalseに設定
        }

        return isInCamera;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("bullet"))
        {
            HP -= 1;
            damagedtime = 0.2f;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Paticle")){
           BulletNum+=1;
           Destroy(collision.gameObject);

        }
    }
    
}
