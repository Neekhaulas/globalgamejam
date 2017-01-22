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
    private float _lastHeight;
    public GameObject CharacterGameObject;
    public int PeopleCount = 1;
    private Vector2 _centerOfMassStart;
    public float CenterOfMassModified;
    public float MaxImbalance;
    public float SpeedImbalance;

    void Start()
    {
        _startPosition = transform.position.x;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), Water.GetComponent<PolygonCollider2D>(), true);
        _lastHeight = transform.position.y;
        _centerOfMassStart = _rigidbody2D.centerOfMass;
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
            positionToMove.y += Height;
            _rigidbody2D.MovePosition(positionToMove);
            transform.position = positionToMove;
        }
        if (transform.rotation.z > 0.707 || transform.rotation.z < -0.707)
        {
            Debug.Log("Sink");
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
            enabled = false;
        }

        if (transform.position.y > _lastHeight)
        {
            CenterOfMassModified = Mathf.Lerp(CenterOfMassModified, MaxImbalance, SpeedImbalance * Time.deltaTime);
        }
        else
        {
            CenterOfMassModified = Mathf.Lerp(CenterOfMassModified, -MaxImbalance, SpeedImbalance * Time.deltaTime);
        }
        _rigidbody2D.centerOfMass = _centerOfMassStart + new Vector2(CenterOfMassModified, 0);
        _lastHeight = transform.position.y;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_rigidbody2D.worldCenterOfMass, 1);
    }

    public void AddCharacter()
    {
        Vector3 positionSpawn = transform.position;
        positionSpawn.y += 2;
        Instantiate(CharacterGameObject, positionSpawn, Quaternion.identity);
        PeopleCount++;
    }

    public void RemoveCharacter()
    {
        PeopleCount--;
    }
}
