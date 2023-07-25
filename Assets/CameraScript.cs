using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject breaktile;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (breaktile == null)
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;

            if (transform.position.y > 30)
            {
                if (mainCamera.orthographicSize < 7.0f)
                {
                    mainCamera.orthographicSize += Time.deltaTime;
                }
               // mainCamera.orthographicSize = 7.0f;
            }
        }
    }
}
