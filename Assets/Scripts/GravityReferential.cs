using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReferential : MonoBehaviour
{
    public float Gravity = -10.0f;

    public void Attract(Transform body)
    {
        Vector2 gravityUp = transform.up;
        body.GetComponent<Rigidbody2D>().AddForce(gravityUp * Gravity);
    }
}
