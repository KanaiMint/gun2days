using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemybulletScript : MonoBehaviour
{
    float lifetime=5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -5, 0) * Time.deltaTime;
        lifetime-=Time.deltaTime;
        if( lifetime < 0 )
        {
            Destroy(gameObject);
        }
    }
}
