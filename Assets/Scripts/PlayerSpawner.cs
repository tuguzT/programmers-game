using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private FieldGenerator fieldGenerator;

    private void Start()
    {
        var carData = fieldGenerator.Cars[Random.Range(0, fieldGenerator.Cars.Length)];
        var original = fieldGenerator.FieldDistribution.GetCar(carData.TeamColor);
        var instantiated = PhotonNetwork.Instantiate(original.name, Vector3.zero, Quaternion.identity);
        instantiated.transform.parent = fieldGenerator.transform;

        var car = instantiated.AddComponent<Car>();
        instantiated.AddComponent<PlayerController>();
        car.FieldGenerator = fieldGenerator;
        car.FromData(carData);
    }
}
