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

    public Vector2 SpeedLimits;

    public float TimeToReachTheMaxValue;

    public int Direction;

    public float ChanceAcceleration;

    private int _targetValue;

    private float currentChanceToChange;

    public WaveVariable()
    {
        _targetValue = 1;
        Direction = 1;
    }

    public void Actualize(float time, RandomGenerator randomGenerator)
    {
        ActualizeValues(time);

        TryToChangeDirection(randomGenerator);
    }

    private void ActualizeValues(float time)
    {
        Value += time * Speed * Direction;

        Speed = Math.Abs((ValueLimits[_targetValue] - Value) / TimeToReachTheMaxValue);

        Speed = Clamp(Speed, SpeedLimits);

        currentChanceToChange += time * ChanceAcceleration;
    }

    private float Clamp(float value, Vector2 limits)
    {
        if (value < limits.x)
        {
            value = limits.x;
        }

        else if (value > limits.y)
        {
            value = limits.y;
        }

        return value;
    }

    private void TryToChangeDirection(RandomGenerator random)
    {
        int percent = random.NextInt(100);

        if(Value < ValueLimits.x)
        {
            Value = ValueLimits.x;
            ChangeDirection();
        }
        else if (Value > ValueLimits.y)
        {
            Value = ValueLimits.y;
            ChangeDirection();
        }
        else if (percent < currentChanceToChange )
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        Direction *= -1;

        _targetValue++;

        if (_targetValue == 2)
            _targetValue = 0;

        currentChanceToChange = 0;

    }
}

