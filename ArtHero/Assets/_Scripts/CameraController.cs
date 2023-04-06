using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private const float OffsetZ = -10f;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void SetPosition(Vector2 position)
    {
        _camera.transform.position = new Vector3(position.x, position.y, OffsetZ);
    }
    
    #region - Enable / Disable -

    private void OnEnable()
    {
        Battlefield.Instance.OnMapGenerated += SetPosition;
    }

    private void OnDisable()
    {
        Battlefield.Instance.OnMapGenerated += SetPosition;
    }

    #endregion
}