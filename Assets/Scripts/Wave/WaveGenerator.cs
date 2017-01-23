using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public float Amplitude;
    public float TimeElapsed;

    void Start()
    {
        TimeElapsed = 0;
    }

    void Update()
    {
        TimeElapsed += Time.deltaTime;
    }

    public WavePoint GetWavePoint(float time)
    {
        return new WavePoint(Mathf.PerlinNoise(time, 0) * Amplitude * TimeElapsed);
    }
}
