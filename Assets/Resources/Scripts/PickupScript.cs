using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(35, 35, 35);
    void Update()
    {
        RotateScript();
    }

    void RotateScript()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
