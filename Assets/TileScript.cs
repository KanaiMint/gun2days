using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public float Movespeed=-2.0f;
    public GameObject particle;
    public float time=0;
    int partime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -2.0f, 0) * Time.deltaTime;
        if (time >= 5.0f)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        partime++;
        if (partime % 4 == 0)
        {

            GameObject par;
            par = Instantiate(particle, transform.position, Quaternion.identity);
            par.transform.position = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y+0.5f);
            par.transform.parent = transform.parent;
        }
    }
}
