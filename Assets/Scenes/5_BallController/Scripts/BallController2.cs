using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2 : BoxController2
{
    private Vector2 _inputPositionPivot;


    // Start is called before the first frame update

    protected override void OnPut(Vector3 pos)
    {
        var rigidbody = HoldingObject2.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        var direction = mainCamera2.transform.TransformDirection(Vector3.forward).normalized;
        var delta = (pos.y - _inputPositionPivot.y) * 100f / Screen.height;
        rigidbody.AddForce((direction + Vector3.up) * 4.5f * delta);
        HoldingObject2.transform.SetParent(null);
        _inputPositionPivot.y = pos.y;

    }

    protected override void OnHold()
    {
        _inputPositionPivot = InputPosition2;
    }
}
