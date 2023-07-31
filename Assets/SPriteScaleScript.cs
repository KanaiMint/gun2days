using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPriteScaleScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.transform.localScale=new Vector3(1.0f, 1.0f, 1.0f);
    }
}
