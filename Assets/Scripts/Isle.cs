using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Isle : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Vector2 position = transform.position;
        position.x -= speed * Time.deltaTime;
        transform.position = position;
    }
}

