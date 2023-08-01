using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public float Movespeed=-2.0f;
    public GameObject particle;
    int time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, Movespeed, 0) * Time.deltaTime;
        if (time >= 5.0f)
        {
            Destroy(gameObject);
        }

    }
    private void FixedUpdate()
    {
        time++;
        if (time %4 == 0)
        {

            GameObject par;
            par = Instantiate(particle, transform.position, Quaternion.identity);
            par.transform.position = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y+0.5f);
            par.transform.parent = transform.parent;
        }
    }
}
