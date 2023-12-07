using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to wind turbines. <br/>
/// This script has no functional purpose. It is only for rotating the turbine blades.
/// </summary>
public class WindTurbine : MonoBehaviour
{

    public GameObject turbineBlades;
    public float rotationSpeed;
    void Update()
    {
        turbineBlades.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}
