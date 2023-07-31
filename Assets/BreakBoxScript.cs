using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakBoxScript : MonoBehaviour
{

    int HP = 2;
    public GameObject Zandan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0)
        {
            Destroy(gameObject);
            GameObject zandan;
            zandan = Instantiate(Zandan, transform.position, Quaternion.identity);
            zandan.transform.parent = transform.parent;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            HP -= 1;
        }
    }
}
