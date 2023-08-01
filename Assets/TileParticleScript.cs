using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParticleScript : MonoBehaviour
{
    public float lifetime = 1.0f;
    public SpriteRenderer spriteRenderer;
    public float newAlpha = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime-=Time.deltaTime;
        newAlpha-=Time.deltaTime;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
        if (lifetime < 0.0f)Destroy(gameObject);
    }
}
