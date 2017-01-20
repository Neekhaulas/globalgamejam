using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public WaveVariable Amplitude;
    public WaveVariable OrdinaryFrequency;
    public WaveVariable Phase;

    private RandomGenerator _randomGenerator;

    private float angularFrequency
    {
        get { return 2 * (float)Math.PI * OrdinaryFrequency.Value; }
    }

    void Awake()
    {
        _randomGenerator = GameObject.FindGameObjectWithTag("RandomGenerator").GetComponent<RandomGenerator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame  
	void Update () {
		
	}

    private float GetHeight(float time)
    {
        return Amplitude.Value * Mathf.Sin(angularFrequency * time + Phase.Value);
    }

    public WavePoint GetWavePoint(float time)
    {
        return new WavePoint(GetHeight(time));
    }
}
