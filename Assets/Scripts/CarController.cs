using System.Diagnostics.CodeAnalysis;
using Attributes;
using UnityEngine;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
public class CarController : MonoBehaviour
{
    public FieldGenerator fieldGenerator;

    [field: SerializeField]
    [field: ReadOnly]
    public Car Car { get; private set; }

    private void Start()
    {
        Car = fieldGenerator.Cars[Random.Range(0, fieldGenerator.Cars.Length)];
    }

    private void Update()
    {
        if (Car.IsMoving) return;

        if (Input.GetKeyDown("w"))
        {
            Car.MoveForward();
        }
        else if (Input.GetKeyDown("a"))
        {
            Car.TurnLeft();
        }
        else if (Input.GetKeyDown("d"))
        {
            Car.TurnRight();
        }
        else if (Input.GetKeyDown("s"))
        {
            Car.TurnAround();
        }
        else if (Input.GetKeyDown("space"))
        {
            Car.Jump();
        }
    }
}
