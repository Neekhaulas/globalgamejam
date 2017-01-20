using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rigidbody2D;
	// Use this for initialization
	void Start ()
	{
	    _rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetAxis("Horizontal") != 0)
	    {
            _rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed, _rigidbody2D.velocity.y);
	    }
	    else
	    {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
    }
}
