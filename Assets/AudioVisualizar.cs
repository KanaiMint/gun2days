using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizar : MonoBehaviour
{
    public GameObject visualizerPrefab;
    int sampleNum = 4096;
    float[] samples;
    GameObject[] visualizers;
    // Start is called before the first frame update
    void Start()
    {
        samples = new float[sampleNum];
        visualizers = new GameObject[sampleNum];
        for(int i = 0; i < sampleNum; i++)
        {
            visualizers[i] = Instantiate(visualizerPrefab, transform);
            visualizers[i].transform.localPosition = new Vector3(0.02f * i, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetOutputData(samples, 0);
        for(int i = 0; i < sampleNum; i++)
        {
            visualizers[i].transform.localScale = new Vector3(0.02f, samples[i] * 100, 1);
        }
    }
}
