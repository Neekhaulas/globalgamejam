using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float MoveSpeed;

    private Ship _ship;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        Vector2 velocity = _rigidbody2D.velocity;
        velocity.x = MoveSpeed * move;
        _rigidbody2D.velocity = velocity;
    }

    void FixedUpdate()
    {
        transform.rotation = _ship.transform.rotation;
    }
}