using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float MoveSpeed;
    private Ship _ship;
    public bool Disabled;
    public SpriteRenderer Head, Torso, Legs;
    public Sprite[] HeadsSprites, TorsoSprites, LegsSprites;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        GenerateCharacter();
    }

    public void GenerateCharacter()
    {
        RandomGenerator randomGenerator = GameObject.FindGameObjectWithTag("RandomGenerator").GetComponent<RandomGenerator>();
        Sprite head = HeadsSprites[randomGenerator.NextInt(0, HeadsSprites.Length)];
        Sprite torso = TorsoSprites[randomGenerator.NextInt(0, TorsoSprites.Length)];
        Sprite leg = LegsSprites[randomGenerator.NextInt(0, LegsSprites.Length)];
        Head.sprite = head;
        Torso.sprite = torso;
        Legs.sprite = leg;
    }

    void Update()
    {
        if (!Disabled)
        {
            float move = Input.GetAxis("Horizontal");
            Vector2 velocity = _rigidbody2D.velocity;
            velocity.x = MoveSpeed * move;
            _rigidbody2D.velocity = velocity;
        }
    }

    void FixedUpdate()
    {
        if (!Disabled)
        {
            transform.rotation = _ship.transform.rotation;
        }
    }

    public void Disable()
    {
        Disabled = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}