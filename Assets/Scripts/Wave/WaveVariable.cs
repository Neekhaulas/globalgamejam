using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class WaveVariable
{
    [Range(-5, 5)]
    public float Value;

    public Vector2 ValueLimits;

    public float Speed;

    public float TimeToReachTheMaxValue;

    public int Direction;

    public float ChanceAcceleration;

    private int _targetValue;

    private float currentChanceToChange;

    public WaveVariable()
    {
        _targetValue = 0;
        Direction = 1;
    }

    public void Actualize(float time, RandomGenerator randomGenerator)
    {
        ActualizeValues(time);

        ChangeDirection(randomGenerator);
    }

    private void ActualizeValues(float time)
    {
        Value += time * Speed * Direction;

        Speed = Math.Abs((ValueLimits[_targetValue] - Value) / TimeToReachTheMaxValue);

        currentChanceToChange += time * ChanceAcceleration;
    }
 
    private void ChangeDirection(RandomGenerator random)
    {
        int percent = random.NextInt(100);

        if (percent < currentChanceToChange || Value < ValueLimits.x || Value > ValueLimits.y)
        {
            Direction *= -1;

            _targetValue++;

            if (_targetValue == 2)
                _targetValue = 0;

            currentChanceToChange = 0;
        }
    }
}

