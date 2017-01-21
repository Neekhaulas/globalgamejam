using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipWreckedGenerator : MonoBehaviour
{
    public Vector2 TimeRangeForSpawn;

    public ShipWrecked ShipWreckedBuffer;

    private float _currentTimeUntilSpawn;
    private RandomGenerator _randomGenerator;

    private void Awake()
    {
        _randomGenerator = GameObject.FindGameObjectWithTag("RandomGenerator").GetComponent<RandomGenerator>();

        ShipWreckedBuffer.gameObject.SetActive(false);

        SetNewTime();
    }

    private void Update()
    {
        _currentTimeUntilSpawn -= Time.deltaTime;

        if (_currentTimeUntilSpawn < 0)
        {
            Spawn();
            SetNewTime();
        }
    }

    private void SetNewTime()
    {
        _currentTimeUntilSpawn = _randomGenerator.NextBinomialFloat(TimeRangeForSpawn);
    }

    private void Spawn()
    {
        if (!ShipWreckedBuffer.gameObject.activeSelf)
        {
            ActivateShipWrecked();
        }
    }

    private void ActivateShipWrecked()
    {
        ShipWreckedBuffer.gameObject.SetActive(true);
        //ShipWreckedBuffer.transform.position = 
    }
}