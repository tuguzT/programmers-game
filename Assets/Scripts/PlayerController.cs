using System.Diagnostics.CodeAnalysis;
using Attributes;
using Photon.Pun;
using UnityEngine;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public class PlayerController : MonoBehaviour
{
    [field: SerializeField] [field: ReadOnly]
    private Car car;

    private PhotonView _photonView;

    private void Awake()
    {
        car = GetComponent<Car>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (car.IsMoving || !_photonView.IsMine) return;

        if (Input.GetKeyDown("w"))
        {
            car.MoveForward();
        }
        else if (Input.GetKeyDown("a"))
        {
            car.TurnLeft();
        }
        else if (Input.GetKeyDown("d"))
        {
            car.TurnRight();
        }
        else if (Input.GetKeyDown("s"))
        {
            car.TurnAround();
        }
        else if (Input.GetKeyDown("space"))
        {
            car.Jump();
        }
    }
}
