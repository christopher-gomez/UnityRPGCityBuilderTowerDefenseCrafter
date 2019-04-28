using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{

	/* public float xAcceleration = 0;
	public float yAcceleration = 0;
	public float accelerationRate; //set to 1 in inspector
	public Vector2 acceleration;
	public Vector2 velocity;
	float friction = .5f;
	public float xMin; //set to -5 in inspector
	public float xMax; //set to 5 in inspector
	public float yMin; //set to -5 in inspector
	public float yMax; //set to 5 in inspector
							 //these -5/+d5 are because a Unity plane's origin is in the centre.

	void Update()
	{
		xAcceleration = 0;
		yAcceleration = 0;

		xAcceleration += Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;
		xAcceleration += Input.GetKey(KeyCode.RightArrow) ? +1 : 0;
		yAcceleration += Input.GetKey(KeyCode.UpArrow) ? +1 : 0;
		yAcceleration += Input.GetKey(KeyCode.DownArrow) ? -1 : 0;
		acceleration = new Vector2(xAcceleration, yAcceleration); //use Vector2 here or we incur extra, costly sqrt calls in magnitude.
		acceleration = Vector2.ClampMagnitude(acceleration, 1.0f); //normalize it.
		acceleration *= accelerationRate;

		//speed
		velocity += acceleration;
		velocity *= friction;

		//position
		float xPos = transform.localPosition.x + velocity.x;
		float yPos = transform.localPosition.y + velocity.y; //note the interchanged y/z here due to Unity's coord system.

		float buffer = 2f; //2 units in world space which might be 2 tiles across in your code.
		float xMinLimited = xMin + buffer;
		float xMaxLimited = xMax - buffer;
		float yMinLimited = yMin + buffer;
		float yMaxLimited = yMax - buffer;

		//We'd usually just use 2 Mathf.Clamp(val, min, max) calls here, but we need to know
		//the outcome of the clamping, so as to also restrict velocity if we hit a map side.
		if (xPos < xMinLimited)
		{
			xPos = xMinLimited;
			velocity = new Vector2(0, velocity.y);
		}
		if (xPos > xMaxLimited)
		{
			xPos = xMaxLimited;
			velocity = new Vector2(0, velocity.y);
		}
		if (yPos < yMinLimited)
		{
			yPos = yMinLimited;
			velocity = new Vector2(velocity.x, 0);
		}
		if (yPos > yMaxLimited)
		{
			yPos = yMaxLimited;
			velocity = new Vector2(velocity.x, 0);
		}


		xPos = Mathf.Clamp(xPos, xMin, xMax); //limit to the plane's x     extent in local space
		yPos = Mathf.Clamp(yPos, yMin, yMax); //limit to the plane's y (z) extent in local space

		//update transform
		transform.localPosition = new Vector3(xPos, yPos, 0);
	}*/
	public Transform target;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.

	Vector3 offset;                     // The initial offset from the target.

	void Start()
	{
		// Calculate the initial offset.
		offset = transform.position - target.position;
		Camera.main.transform.LookAt(target);
	}

	void FixedUpdate()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = target.position + offset;

		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
