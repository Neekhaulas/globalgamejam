using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float Height;
    public GameObject Water;

    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), Water.GetComponent<PolygonCollider2D>(), true);
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = 100;
        RaycastHit2D hit = Physics2D.Raycast(position, -transform.up, 500f, LayerMask.GetMask("Water"));
        if (hit)
        {
            transform.position = hit.point;
        }
    }
}
