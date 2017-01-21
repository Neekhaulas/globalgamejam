using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipWrecked : MonoBehaviour
{
    public float RotateSpeed;

    public WavePoint TargetPosition;

    private bool _isRecovered = false;

    public void UpdatePosition(Horizon horizon)
    {
        if (_isRecovered)
        {
            //rotate party !
        }
        else
        {
            gameObject.transform.position = horizon.GetPosition(TargetPosition.IndexInTheList);
        }
    }

    public void SetIsRecovered(bool state)
    {
        _isRecovered = state;
    }

    private void Update()
    {
        if (_isRecovered)
        {
            transform.Rotate(new Vector3(0, 0, 1), RotateSpeed * Time.deltaTime);
        }
    }
}

