using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

	[SerializeField] private float _speed = 10;

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * _speed));
	}
}
