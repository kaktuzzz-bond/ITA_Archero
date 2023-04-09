using System;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    private const float OffsetZ = -10f;

    private const float ShiftY = 4f;

    private Camera _camera;

    private Transform _target;

    private float _exitPointY;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (_target == null) return;

        //доходим до определенной высоты и останавливаем движение камеры
        if (_target.position.y > _exitPointY) return;

        Vector3 camPos = _camera.transform.position;

        camPos.y = _target.position.y + ShiftY;

        _camera.transform.position = camPos;
    }

    private void SetTarget(Transform target)
    {
        _target = target;

        _camera.transform.position =
                new Vector3(_target.position.x, _target.position.y + ShiftY, OffsetZ);
    }

    private void SetOrthographicSize(Vector3 origin, int width, int height)
    {
        float boundSize = Battlefield.Instance.field.cellSize.x * width + 1f;

        _camera.orthographicSize = boundSize * Screen.height / Screen.width * 0.5f;

        _exitPointY = height - _camera.orthographicSize;

        Vector3 pointPos = _camera.transform.position;
        pointPos.y = _exitPointY;
        pointPos.z = 0;
        Debug.Log(pointPos);
       
        Debug.DrawRay(pointPos, Vector3.right * 10, Color.red);
    }

    #region - Enable / Disable -

    private void OnEnable()
    {
        PlayerManager.Instance.OnPlayerCreatedEvent += SetTarget;
        Battlefield.Instance.OnMapGenerated += SetOrthographicSize;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnPlayerCreatedEvent -= SetTarget;
        Battlefield.Instance.OnMapGenerated -= SetOrthographicSize;
    }

    #endregion
}