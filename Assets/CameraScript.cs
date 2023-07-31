using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    public GameObject breaktile;
    public GameObject kowase;
    private Camera mainCamera;
    Vector3 startposkowase;
   public bool IsGameOver = false;
   public bool IsGameClear = false;
    public TextMeshProUGUI GameCleartex;
    public TextMeshProUGUI GameOvertex;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        startposkowase = kowase.transform.position;
        GameCleartex.gameObject.SetActive(false);
        GameOvertex.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (breaktile == null)
        {
            kowase.gameObject.SetActive(false);
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;

            if (transform.position.y > 30)
            {
                if (mainCamera.orthographicSize < 9.0f)
                {
                    mainCamera.orthographicSize += Time.deltaTime;
                }
               // mainCamera.orthographicSize = 7.0f;
            }
        }else
        {
            kowase.transform.position = new Vector3(startposkowase.x, startposkowase.y+Mathf.Sin(Time.time * 5.0f) * 0.175f, 0);
        }

        if (IsGameClear)
        {
            GameCleartex.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (IsGameOver)
        {

            GameOvertex.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
