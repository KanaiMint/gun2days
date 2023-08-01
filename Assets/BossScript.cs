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
    float AttackTime = 2.0f;
    public int Rand;
    public SpriteRenderer spriteRenderer;
    float damagedtime = 0.0f;
    public CameraScript cameraScript;
    public Slider BosHpBar;
    public int BulletNum= 0;
    public GameObject player;
   
        float DrainTime;
        float KDrainTime;
        bool isAttack;
 
    
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
                }
                Rand = Random.Range(0, 2);
                AttckMove();
                AttackTime -= Time.deltaTime;

            }
            if (HP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
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
                //GameObject enemy;
                //enemy = Instantiate(Enemy, transform.position, Quaternion.identity);
                //enemy.transform.parent = transform.parent;
                //AttackTime = KAttackTime;
            }
            if (Rand == 2)
            {
                
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
            BulletNum++;
           // Destroy(collision.gameObject);

        }
    }
    
}
