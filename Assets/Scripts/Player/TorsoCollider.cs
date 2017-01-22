﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoCollider : MonoBehaviour
{
    public Character Character;

    void Start()
    {
        Character = transform.parent.GetComponent<Character>();
    }

    public void FallInWater()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        Character.Disable();
    }
}