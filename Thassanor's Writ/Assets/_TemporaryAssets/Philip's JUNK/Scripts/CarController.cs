using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    [SerializeField]
    public float speed = 10.0f;
    [SerializeField]
    public float rotationSpeed = 100.0f;
	
	void Update () {
        float accel = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        accel *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, accel);
        transform.Rotate(0, rotation, 0);
	}
}
