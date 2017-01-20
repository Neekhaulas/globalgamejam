using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    // the peak deviation of the function from zero.
    public WaveVariable Amplitude;

    // the number of oscillations (cycles) that occur each second of time.
    public WaveVariable OrdinaryFrequency;

    // specifies (in radians) where in its cycle the oscillation is at t = 0. 
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
		Amplitude.Actualize(Time.deltaTime, _randomGenerator);
		OrdinaryFrequency.Actualize(Time.deltaTime, _randomGenerator);
		Phase.Actualize(Time.deltaTime, _randomGenerator);
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
