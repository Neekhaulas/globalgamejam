using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class WavePoint
{
    public float Height;

    public int IndexInTheList;

    public WavePoint()
        : this(0)
    { }

    public WavePoint(float height)
    {
        Height = height;
    }
}

