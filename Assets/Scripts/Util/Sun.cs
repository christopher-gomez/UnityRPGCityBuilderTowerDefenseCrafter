using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

	public float dayLength;
	float rotationSpeed;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		rotationSpeed = Time.deltaTime / dayLength;
		transform.Rotate(0, rotationSpeed, 0);
	}
}
