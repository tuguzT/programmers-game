using Model;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera currentCamera;

    [SerializeField] [Range(0, 200)] private float cameraSpeed = 100;

    [SerializeField] [Range(0, 20)] private float zoomSpeed = 10;

    [SerializeField] [Range(0, 40)] private float minimumAngle = 30;

    [SerializeField] [Range(50, 90)] private float maximumAngle = 60;

    private Vector3 center;
    private float rotationX;
    private float rotationY;

    private float zoomDistance;
    private float distance;
    private float minDistance;
    private float maxDistance;

    private void Start()
    {
        var myTransform = transform;
        center = myTransform.position;

        var fieldWidth = GameManager.Instance.GameMode.FieldWidth();
        minDistance = fieldWidth * Model.Tile.TileData.Width;
        maxDistance = minDistance * 1.5f;
        zoomDistance = distance = (minDistance + maxDistance) / 2;

        currentCamera.transform.position = center - myTransform.forward * distance;
        var angle = rotationX = (minimumAngle + maximumAngle) / 2;
        currentCamera.transform.RotateAround(center, myTransform.right, angle);
        currentCamera.transform.LookAt(center);
    }

    private void LateUpdate()
    {
        var cameraTransform = currentCamera.transform;

        if (Input.GetMouseButton(0))
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = -Input.GetAxis("Mouse Y");

            rotationY += mouseX * cameraSpeed * Time.deltaTime;
            rotationX += mouseY * cameraSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minimumAngle, maximumAngle);

            cameraTransform.localEulerAngles = new Vector3(rotationX, rotationY);
        }

        zoomDistance -= Input.mouseScrollDelta.y * 0.5f;
        zoomDistance = Mathf.Clamp(zoomDistance, minDistance, maxDistance);
        distance = Mathf.MoveTowards(distance, zoomDistance, zoomSpeed * Time.deltaTime);
        cameraTransform.position = center - cameraTransform.forward * distance;
    }
}
