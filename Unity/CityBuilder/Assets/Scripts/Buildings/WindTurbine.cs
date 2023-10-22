using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : MonoBehaviour
{
    // this script has no functional purpose. It is only for rotating the turbine blades
    public GameObject turbineBlades;
    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turbineBlades.transform.Rotate(new Vector3(0,rotationSpeed,0) * Time.deltaTime);
    }
}
