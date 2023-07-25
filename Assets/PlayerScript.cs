using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MaxHP=10;
    public float HP=10;

    public GameObject bullet;
    float ShootColltime = 0.3f;
    
   const float KShootColltime = 0.3f;
    public SpriteRenderer spriteRenderer;
    float ChargeTime = 0.0f;
    const float KChargeTime = 0.3f;
    private Vector3 originalChildScale;
    // Start is called before the first frame update
    void Start()
    {
       spriteRenderer=GetComponent<SpriteRenderer>();
        // 子オブジェクトの元のスケールを保存
        originalChildScale = transform.GetChild(0).localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)) 
        {
            transform.position += new Vector3(-5, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(5, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.J)&&ShootColltime<=0.0f&&HP>1)
        {
            HP -= 1;
            GameObject bullet_;
            bullet_=Instantiate(bullet);
            bullet_.transform.position = transform.position;
            bullet_.transform.parent = transform.parent;
            ShootColltime = KShootColltime;
        }else
        if (Input.GetKey(KeyCode.K)&&!Input.GetKey(KeyCode.J))
        {
            ChargeTime += Time.deltaTime;
        }
        if ((ChargeTime > KChargeTime)&&HP<=MaxHP)
        {
            HP += 1;
            ChargeTime = 0.0f;
        }
        ShootColltime-=Time.deltaTime;

        float spriteScalX = (HP / 10);

        transform.localScale = new Vector2(spriteScalX, 1.0f);
        transform.GetChild(0).localScale = originalChildScale;
        transform.GetChild(1).localScale = originalChildScale;
        transform.GetChild(2).localScale = originalChildScale;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            MaxHP -= 1;
            HP -= 1;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Enemy"))
        {
            MaxHP -= 2;
            HP -= 2;
            Destroy(collision.gameObject);
        }
    }
}
