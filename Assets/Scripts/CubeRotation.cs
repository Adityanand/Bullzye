using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public GameObject Cube;
    public float RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        RotationSpeed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        Cube.transform.Rotate(RotationSpeed * Time.deltaTime, RotationSpeed * Time.deltaTime, 0);
    }
}
