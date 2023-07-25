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
        // �I�u�W�F�N�g�̈ʒu��Viewport���W�ɕϊ�
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Viewport���W���J�����͈͓̔����ǂ����𔻒�
        bool isInCamera = viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
                         viewportPosition.y >= 0f && viewportPosition.y <= 0.95f &&
                         viewportPosition.z > 0f;

        // �J�����ɉf���Ă���ꍇ�̏���
        if (isInCamera)
        {
            isOnCamera = true; // �J�����ɉf���Ă��邱�Ƃ�����bool�ϐ���true�ɐݒ�

            Debug.Log("Object is in camera view!");
        }
        else
        {
            isOnCamera = false; // �J�����ɉf���Ă��Ȃ����Ƃ�����bool�ϐ���false�ɐݒ�
        }

        return isInCamera;
    }
}
