using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharegParScript : MonoBehaviour
{
    public GameObject target;
    float lifeTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target !=null)
        {

        Vector3 vel = target.transform.position - transform.position;

        transform.position += vel.normalized*Time.deltaTime*10;
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
