using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    public AudioMixer mixer;
    public float cutoffFreq = 1000;
    private AudioMixerSnapshot Snapshot;
    private AudioMixerSnapshot Snapshot2;
    private AudioMixerSnapshot[] Snapshots;
    float[] transitionRate;
    float[] samples = new float[4096];
    // Start is called before the first frame update
    void Start()
    {
        Snapshot = mixer.FindSnapshot("Snapshot");
        Snapshot2 = mixer.FindSnapshot("Snapshot2");
        Snapshots = new AudioMixerSnapshot[] { mixer.FindSnapshot("Snapshot"), mixer.FindSnapshot("Snapshot2") };
        transitionRate = new float[] { 0.2f, 0.8f };
    }

    // Update is called once per frame
    void Update()
    {
        mixer.SetFloat("OutsideLowpassfreq", cutoffFreq);
        if (Input.GetKeyDown(KeyCode.Z)) { Snapshot.TransitionTo(1); }
        if (Input.GetKeyDown(KeyCode.X)) { Snapshot2.TransitionTo(1); }

        mixer.TransitionToSnapshots(Snapshots, transitionRate, 1.0f);
    }
}
