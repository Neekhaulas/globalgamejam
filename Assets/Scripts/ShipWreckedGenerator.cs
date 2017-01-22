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
    private Horizon _horizon;

    private bool IsTimeToSpawn
    {
        get { return _currentTimeUntilSpawn < 0 && !ShipWreckedBuffer.isActiveAndEnabled; }
    }

    private void Awake()
    {
        ShipWreckedBuffer.gameObject.SetActive(false);
    }

    private void Start()
    {
        _randomGenerator = GameObject.FindGameObjectWithTag("RandomGenerator").GetComponent<RandomGenerator>();
        _horizon = GameObject.FindGameObjectWithTag("Horizon").GetComponent<Horizon>();

        SetNewTime();
    }

    private void Update()
    {
        _currentTimeUntilSpawn -= Time.deltaTime;

        if (ShipWreckedBuffer.TargetPosition.IndexInTheList == -1
            && ShipWreckedBuffer.isActiveAndEnabled
            && !ShipWreckedBuffer.IsRecovered())
        {
            DisableShipWrecked();
        }

        if (IsTimeToSpawn)
        {
            Spawn();
        }
    }

    private void ActualizePosition()
    {
        ShipWreckedBuffer.UpdatePosition(_horizon);
    }

    public void TryToActualizeShipWrecked()
    {
        if (ShipWreckedBuffer.isActiveAndEnabled
            && !ShipWreckedBuffer.IsRecovered()
            && ShipWreckedBuffer.TargetPosition.IndexInTheList != -1)
        {
            ActualizePosition();
        }
    }

    private void SetNewTime()
    {
        _currentTimeUntilSpawn = _randomGenerator.NextBinomialFloat(TimeRangeForSpawn);
    }

    private void Spawn()
    {
        if (!ShipWreckedBuffer.isActiveAndEnabled)
        {
            ActivateShipWrecked();
        }
    }

    private void ActivateShipWrecked()
    {
        ShipWreckedBuffer.TargetPosition = _horizon.Last;
        ShipWreckedBuffer.gameObject.SetActive(true);
    }

    private void DisableShipWrecked()
    {
        ShipWreckedBuffer.gameObject.SetActive(false);
        SetNewTime();
    }
}