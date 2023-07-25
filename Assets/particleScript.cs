using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class particleScript : MonoBehaviour
{
   public Vector3 vel;
   public float lifetime=1.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(vel.x, vel.y, vel.z)*10*Time.deltaTime;
        lifetime-=Time.deltaTime;
        if(lifetime < 1.0f)Destroy(gameObject);
    }
   
}
