using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float lifetime = 1.5f;
    bool isInCamera;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 7, 0)*Time.deltaTime;
        lifetime -= Time.deltaTime;
       
        if (!InCamera())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private bool InCamera()
    {
        // オブジェクトの位置をViewport座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport座標がカメラの範囲内かどうかを判定
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1.0f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 1.0f &&
                         viewportPosition.z > 0f;      

        return isInCamera;
    }
}
