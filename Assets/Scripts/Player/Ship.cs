using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    public float Height;
    public GameObject Water;
    public float SinkAngle = 90f;
    public Transform RecoverPoint;
    private Rigidbody2D _rigidbody2D;
    private float _startPosition;
    public GameObject CharacterGameObject;

    void Start()
    {
        _startPosition = transform.position.x;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), Water.GetComponent<PolygonCollider2D>(), true);
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = 200;
        position.x = _startPosition;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 500f, LayerMask.GetMask("Water"));

        if (hit)
        {
            Vector2 positionToMove = hit.point;
            positionToMove.x = _startPosition;
            _rigidbody2D.MovePosition(positionToMove);
            transform.position = positionToMove;
        }
        Debug.Log(transform.eulerAngles.z);
        if (transform.rotation.z > 0.707 || transform.rotation.z < -0.707)
        {
            Debug.Log("Sink");
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
            enabled = false;
        }
    }

    public void AddCharacter()
    {
        Vector3 positionSpawn = transform.position;
        positionSpawn.y += 2;
        Instantiate(CharacterGameObject, positionSpawn, Quaternion.identity);
    }
}
