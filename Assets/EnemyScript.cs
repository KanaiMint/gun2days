using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyScript : MonoBehaviour
{
    public GameObject bullet;
    private float ShootCoolTime = 0.1f;
    const float KShootCoolTime = 2.0f;
    SpriteRenderer spriteRenderer;
    float damagedtime = 0.0f;
    private int HP = 3;
    public GameObject particle;
    bool isInCamera;
    private Camera mainCamera;
    public bool isOnCamera = false;
    public AudioSource audioSource;
    public AudioClip sounddamage;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInCamera)
        {
            ShootCoolTime -= Time.deltaTime;
            if (ShootCoolTime <= 0)
            {
                GameObject bullet_;
                bullet_ = Instantiate(bullet);
                bullet_.transform.position = transform.position;
                bullet_.transform.parent = transform.parent;
                ShootCoolTime = KShootCoolTime;
                audioSource.Play();
            }
            if (damagedtime > 0.0f)
            {
                spriteRenderer.color = Color.red;
                damagedtime -= Time.deltaTime;
            }
            else { spriteRenderer.color = Color.white; }

            
            
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        }

        isInCamera = InCamera();
        if (HP <= 0)
        {
            Destroy(gameObject);


        }
    }

    private bool InCamera()
    {
        // �I�u�W�F�N�g�̈ʒu��Viewport���W�ɕϊ�
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport���W���J�����͈͓̔����ǂ����𔻒�
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 0.8f &&
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
            Destroy(collision.gameObject);
            damagedtime = 0.1f;
            audioSource.PlayOneShot(sounddamage);
            for (int i = 0; i <= 10; i++)
            {
                GameObject particle_;
                particleScript particleScript;
                particleScript = particle.GetComponent<particleScript>();
                particle_ = Instantiate(particle);
                particle_.transform.position = transform.position;
                particle_.transform.parent = transform.parent;
                particleScript.vel = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
                particleScript.lifetime = 1.0f;
            }
        }
    }



    private void OnDestroy()
    {
        //for (int i = 0; i <= 20; i++)
        //{
        //    GameObject particle_ = Instantiate(particle);
        //    particle_.transform.position = transform.position;  
        //    particle_.transform.parent = transform.parent;

        //}
        Debug.Log("isdesstory");

    }
}
