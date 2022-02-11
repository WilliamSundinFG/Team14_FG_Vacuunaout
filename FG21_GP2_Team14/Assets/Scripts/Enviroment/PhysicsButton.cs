using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour, Iinteriact
{
    
    
    public float threshold = 0.1f;
    public float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;
    
    
    public UnityEvent onPress, OnReleased;

    private void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }

        if (_isPressed && GetValue() - threshold <= 0)
        {
            Realeased();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if (Mathf.Abs((value))< deadZone)
        {
            value = 0;
        }
        

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        onPress.Invoke();
    }

    private void Realeased()
    {
        _isPressed = false;
        OnReleased.Invoke();
    }

    public void Interiact()
    {
        Pressed();
    }
}
