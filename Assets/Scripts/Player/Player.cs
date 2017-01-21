using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Character> CharacterList;

    // Use this for initialization
	void Awake ()
	{
        CharacterList = new List<Character>();
	}
	
	// Update is called once per frame
	void Update () {

    }
}
