using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Camera mainCamera;
    public bool isOnCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(InCamera())
        {
            transform.position += new Vector3(0, 1, 0)*Time.deltaTime;

            
        }
    }

    private bool InCamera()
    {
        // オブジェクトの位置をViewport座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport座標がカメラの範囲内かどうかを判定
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 0.95f &&
                         viewportPosition.z > 0f;

        // カメラに映っている場合の処理
        if (isInCamera)
        {
            isOnCamera = true; // カメラに映っていることを示すbool変数をtrueに設定

            Debug.Log("Object is in camera view!");
        }
        else
        {
            isOnCamera = false; // カメラに映っていないことを示すbool変数をfalseに設定
        }

        return isInCamera;
    }
}
