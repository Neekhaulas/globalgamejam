using System.Collections;
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
        if (!Character.Disabled)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            Character.Disable();
            GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>().RemoveCharacter();
        }
    }
}
