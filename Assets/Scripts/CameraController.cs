using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera currentCamera;

    [SerializeField]
    [Range(0, 200)]
    private float cameraSpeed = 100;

    [SerializeField]
    [Range(0, 90)]
    private float minimumAngle = 30;

    [SerializeField]
    [Range(0, 90)]
    private float maximumAngle = 60;

    [SerializeField]
    [Range(0, 100)]
    private float distance = 10;

    private Vector3 _center;
    private float _rotationX;
    private float _rotationY;

    private void Start()
    {
        var myTransform = transform;
        _center = myTransform.position;

        currentCamera.transform.position = _center + myTransform.forward * distance;
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
        cameraTransform.position = _center - cameraTransform.forward * distance;
    }
}
