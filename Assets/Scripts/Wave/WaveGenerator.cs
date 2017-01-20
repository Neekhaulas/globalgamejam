using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public float Amplitude;
    public float OrdinaryFrequency;
    public float Phase;

    private float angularFrequency
    {
        get { return 2 * (float)Math.PI * OrdinaryFrequency; }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private float GetHeight(float time)
    {
        return Amplitude * Mathf.Sin(angularFrequency * time + Phase);
    }

    public WavePoint GetWavePoint(float time)
    {
        return new WavePoint(GetHeight(time));
    }
}
