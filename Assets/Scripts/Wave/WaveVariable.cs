using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class WaveVariable
{
    [Range(-5, 5)]
    public float Value;

    public float Speed;

    public Vector2 Limits;

    public float MinForceTime;

    public float MaxTime;

    private float average
    {
        get { return (Limits.x + Limits.y) / 2; }
    }

    private bool isForced
    {
        get { return _forceChangementTime > 0; }
    }

    public float Acceleration;

    public float AccelerationStep;

    private float _timeMaxUntilNextChange;

    private float _forceChangementTime;

    public WaveVariable()
    {
        _timeMaxUntilNextChange = MaxTime;
        _forceChangementTime = -1;
    }

    public void Actualize(float time, RandomGenerator randomGenerator)
    {
        ActualizeTimes(time);

        ActualizeValues(time);

        if (Clamp(ref Value, Limits))
        {
            ForceChangement();
        }

        if (!isForced)
        {
            if (HasToChangeDirection(randomGenerator))
            {
                ChangeDirection();
            }
        }

        if (Clamp(ref Acceleration, new Vector2(-3, 3)))
        {
            // nothing ? 
        }

        if (Clamp(ref Speed, new Vector2(-Limits.y/3, Limits.y/3)))
        {
            // nothing ?
        }
    }

    private void ActualizeValues(float time)
    {
        Value += time * Speed;

        Speed *= Acceleration;

        Acceleration += AccelerationStep * time;

        ActualizeAccelerationStep();
    }

    private void ActualizeAccelerationStep()
    {
        if (isForced)
        {
            
        }
        else
        {
            float distToMax = Limits.y - Value;

            distToMax = Normalize(distToMax, Limits.y - Limits.x);

            distToMax *= 2;

            distToMax -= 1;

            AccelerationStep = distToMax;
        }
    }

    private bool HasToChangeDirection(RandomGenerator randomGenerator)
    {
        float randomFloat = randomGenerator.NextFloat();

        return randomFloat > Normalize(_timeMaxUntilNextChange, MaxTime);
    }

    private void ActualizeTimes(float time)
    {
        _timeMaxUntilNextChange -= time;
        _forceChangementTime -= time;
    }

    private void ForceChangement()
    {
        ChangeDirection();
        _forceChangementTime = MinForceTime;
    }

    private float Normalize(float value, float MaxValue = float.MaxValue)
    {
        return Math.Abs(value / MaxValue);
    }

    private bool Clamp(ref float value, Vector2 limits)
    {
        if (value < limits.x)
        {
            value = limits.x;
            return true;
        }

        if (value > limits.y)
        {
            value = limits.y;
            return true;
        }

        return false;
    }

 
    private void ChangeDirection()
    {
        Acceleration = -Math.Sign(Acceleration);
        AccelerationStep = Math.Sign(Acceleration);

        _timeMaxUntilNextChange = MaxTime;
        _forceChangementTime = -1;
    }
}

