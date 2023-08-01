using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class particleScript : MonoBehaviour
{
   public Vector3 vel;
   public float lifetime=5.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(vel.x, vel.y, vel.z)*10*Time.deltaTime;
        lifetime-=Time.deltaTime;
        if (lifetime < 0.0f) { Destroy(gameObject); };
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
