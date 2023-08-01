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
        float KDrainTime=3.0f;
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
                 Rand = Random.Range(3, 4);
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
            isAttack = true;
            if (Rand == 0)
            {
                GameObject Breakbox;
                Breakbox = Instantiate(BreakBox, transform.position, Quaternion.identity);
                Breakbox.transform.parent = transform.parent;
                AttackTime = 1.5f;
            }
            if (Rand == 1)
            {
                GameObject enemy;
                enemy = Instantiate(Enemy, transform.position, Quaternion.identity);
                enemy.transform.parent = transform.parent;
                AttackTime = KAttackTime;
            }
            if (Rand == 2)
            {
                GameObject Tile;
                Tile=Instantiate(tile, transform.position,Quaternion.identity);
                tile.transform.parent = transform.parent;
                AttackTime = 0.5f;

            }
            if(Rand == 3)
            {
                DrainTime += Time.deltaTime;
                if(DrainTime>=KDrainTime)
                {
                    while (BulletNum != 0)
                    {
                        if (time % 5 == 0)
                        {
                            GameObject bullet_;
                            bullet_ = Instantiate(bullet);
                            bullet_.transform.position = transform.position;
                            bullet_.transform.parent = transform.parent;
                            BulletNum--;
                        }
                    }
                    if (BulletNum <= 0)
                    {
                        AttackTime = 2.0f;
                        DrainTime = 0.0f;
                    }
                }
                else
                {
                    Vector3 rand = new Vector3(transform.position.x + Random.Range(-10.0f,10.0f), transform.position.y + Random.Range(-15, -15), 0);
                    if (time % 8 == 0)
                    {
                        GameObject particle_;
                        particleScript particleScript_;
                        particle_ = Instantiate(particle, rand, Quaternion.identity);
                        particleScript_ = particle_.GetComponent<particleScript>();
                        particleScript_.lifetime = 5.0f;
                        particle_.transform.parent = transform.parent;
                        Vector3 tmp = (transform.position - particle_.transform.position).normalized;
                        particleScript_.vel = tmp;
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
        // �I�u�W�F�N�g�̈ʒu��Viewport���W�ɕϊ�
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport���W���J�����͈͓̔����ǂ����𔻒�
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 0.9f &&
                         viewportPosition.z > 0f;

        // �J�����ɉf���Ă���ꍇ�̏���
        if (isInCamera)
        {
            isOnCamera = true; // �J�����ɉf���Ă��邱�Ƃ�����bool�ϐ���true�ɐݒ�

            Debug.Log("Object is in camera view!");
        }
        else
        {
            isOnCamera = false; // �J�����ɉf���Ă��Ȃ����Ƃ�����bool�ϐ���false�ɐݒ�
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
            BulletNum++;
           // Destroy(collision.gameObject);

        }
    }
    
}
