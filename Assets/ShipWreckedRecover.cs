using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ShipWreckedRecover : MonoBehaviour
{
    public Collider2D Collider;
    private Ship _ship;

    private void Start()
    {
        _ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ShipWrecked")
        {
            Debug.Log("Recover");
        }
    }
}