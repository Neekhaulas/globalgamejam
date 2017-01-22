using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShipWrecked : MonoBehaviour
{
    public float RotateSpeed;
    public float TranslationSpeed;

    public WavePoint TargetPosition;

    private bool _isRecovered = false;
    private Transform _target;
    private Renderer _render;

    private ShipWreckedRecover _bufferRecover;

    private void Start()
    {
        _render = GetComponent<Renderer>();
    }

    public void UpdatePosition(Horizon horizon)
    {
        if (_isRecovered)
        {
            //rotate party !
        }
        else
        {
            gameObject.transform.position = horizon.GetPosition(TargetPosition.IndexInTheList);
        }
    }

    public void SetIsRecovered(bool state)
    {
        _isRecovered = state;
    }

    public void SetTarget(Transform transform, ShipWreckedRecover recover)
    {
        _bufferRecover = recover;
        _target = transform;
    }

    private void Update()
    {
        if (_isRecovered)
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();

            direction *= TranslationSpeed * Time.deltaTime;

            transform.position += new Vector3(direction.x, direction.y);

            //transform.Translate(direction);

            transform.RotateAround(_render.bounds.center, new Vector3(0, 0, 1), RotateSpeed * Time.deltaTime);

            if (transform.position.x < _target.position.x)
            {
                transform.position = _target.position;
                _bufferRecover.AddCharacter();
                _isRecovered = false;
                gameObject.SetActive(false);
            }
        }
    }

    public bool IsRecovered()
    {
        return _isRecovered;
    }
}