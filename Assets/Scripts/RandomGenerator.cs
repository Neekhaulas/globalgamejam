using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public bool GenerateRandomSeed;
    public int Seed;

    private System.Random _pseudoRandom;

    private void Awake()
    {
        if (GenerateRandomSeed)
        {
            GenerateSeed();
        }

        _pseudoRandom = new System.Random(Seed);
    }

    private void GenerateSeed()
    {
        Seed = DateTime.Now.Ticks.GetHashCode();
    }

    public float NextFloat()
    {
        return (float) _pseudoRandom.NextDouble();
    }

    public int NextInt()
    {
        return _pseudoRandom.Next();
    }

    public int NextInt(int max)
    {
        return _pseudoRandom.Next(max);
    }

    public int NextInt(int min, int max)
    {
        return _pseudoRandom.Next(min, max);
    }

    public float NextBinomialFloat(Vector2 range)
    {
        float randomFloat = NextFloat();

        randomFloat *= range.y - range.x;

        return randomFloat;
    }
}

