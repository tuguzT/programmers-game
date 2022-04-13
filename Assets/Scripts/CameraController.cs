using Field;
using Model;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera currentCamera;

    [SerializeField]
    [Range(0, 200)]
    private float cameraSpeed = 100;

    [SerializeField]
    [Range(0, 20)]
    private float zoomSpeed = 5;

    [SerializeField]
    [Range(0, 40)]
    private float minimumAngle = 30;

    [SerializeField]
    [Range(50, 90)]
    private float maximumAngle = 60;

    private Vector3 _center;
    private float _rotationX;
    private float _rotationY;

    private float _zoomDistance;
    private float _distance;
    private float _minDistance;
    private float _maxDistance;

    private void Start()
    {
        var myTransform = transform;
        _center = myTransform.position;

        var fieldWidth = GameManager.Instance.GameMode.FieldWidth();
        _minDistance = fieldWidth * ChunkData.Width;
        _maxDistance = fieldWidth * 1.5f * ChunkData.Width;
        _zoomDistance = _distance = (_minDistance + _maxDistance) / 2;

        currentCamera.transform.position = _center + myTransform.forward * _distance;
        var angle = (minimumAngle + maximumAngle) / 2;
        currentCamera.transform.RotateAround(_center, -myTransform.right, angle);
        currentCamera.transform.LookAt(_center);
    }

    private void LateUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = -Input.GetAxis("Mouse Y");

        _rotationY += mouseX * cameraSpeed * Time.deltaTime;
        _rotationX += mouseY * cameraSpeed * Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX, minimumAngle, maximumAngle);

        var cameraTransform = currentCamera.transform;
        cameraTransform.localEulerAngles = new Vector3(_rotationX, _rotationY);

        _zoomDistance -= Input.mouseScrollDelta.y * 0.25f;
        _zoomDistance = Mathf.Clamp(_zoomDistance, _minDistance, _maxDistance);
        _distance = Mathf.MoveTowards(_distance, _zoomDistance, zoomSpeed * Time.deltaTime);
        cameraTransform.position = _center - cameraTransform.forward * _distance;
    }
}
