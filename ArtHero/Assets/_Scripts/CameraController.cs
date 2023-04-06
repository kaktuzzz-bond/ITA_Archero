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

    private void SetPosition(Vector3 origin, int width, int height)
    {
        _camera.transform.position = new Vector3(width * 0.5f, height *0.5f, OffsetZ) + origin;
    }
    
    #region - Enable / Disable -

    private void OnEnable()
    {
        Battlefield.Instance.onMapGenerated += SetPosition;
    }

    private void OnDisable()
    {
        Battlefield.Instance.onMapGenerated += SetPosition;
    }

    #endregion
}