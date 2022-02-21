using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera currentCamera;

    [SerializeField]
    [Range(0, 100)]
    private float cameraSpeed = 50;

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

    private void Start()
    {
        var myTransform = transform;
        _center = myTransform.position;

        currentCamera.transform.position = _center + Vector3.forward * distance;
        currentCamera.transform.RotateAround(_center, Vector3.left, minimumAngle);
        currentCamera.transform.LookAt(_center);
    }

    private void Update()
    {
        currentCamera.transform.RotateAround(_center, Vector3.up, cameraSpeed * Time.deltaTime);
    }
}
