using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public float Amplitude;

   /* void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
        _randomGenerator = GameObject.FindGameObjectWithTag("RandomGenerator").GetComponent<RandomGenerator>();
    }
	    
	// Update is called once per frame  
	void Update () {
		Amplitude.Actualize(Time.deltaTime, _randomGenerator);
		OrdinaryFrequency.Actualize(Time.deltaTime, _randomGenerator);
		//Phase.Actualize(Time.deltaTime, _randomGenerator);
	}

    private float GetHeight(float time)
    {
        //return Amplitude.Value * Mathf.Sin(angularFrequency * time + Phase.Value);
        return Amplitude.Value * Mathf.Sin(angularFrequency * time);
    }
    */
    public WavePoint GetWavePoint(float time)
    {
        return new WavePoint(Mathf.PerlinNoise(time, 0) * Amplitude);
    }
}
