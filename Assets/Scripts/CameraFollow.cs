using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ToFollow;
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (ToFollow != null)
	    {
	        Vector3 position = ToFollow.position;
	        position.z = -10;
	        transform.position = position;
	    }
	}
}
