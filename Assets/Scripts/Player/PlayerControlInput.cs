using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerController))]
public class PlayerControlInput : MonoBehaviour
{
	private PlayerController m_Character; // A reference to the ThirdPersonCharacter on the object
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;

	private float hit_angle = 45;
	private float normal_angle = 0;
	private float smooth = 5f;

	private void Start()
	{
		// get the transform of the main camera
		if (Camera.main != null)
		{
			m_Cam = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning(
				 "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}

		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<PlayerController>();
	}


	private void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			m_Character.Dash();
		}
		else if (Input.GetMouseButtonDown(1))
		{
			m_Character.Attack();
		}

	}


	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{

		/*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 lookAt = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
			var heading = lookAt - transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance;

			if (direction != transform.position / transform.position.magnitude)
			{
				//transform.LookAt(lookAt);
				//transform.forward = heading;
				//m_Character.Attack(heading);
			}
		}*/

		bool walking = false;
		bool running = false;
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		Vector2 DirVec = new Vector2(v, h);
		DirVec.Normalize();

		float forwardSpeed = DirVec.x;
		float sideSpeed = DirVec.y;

		// calculate move direction to pass to character
		if (m_Cam != null)
		{
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = forwardSpeed * m_CamForward + sideSpeed * m_Cam.right;
			if (m_Move.magnitude > 0)
			{
				walking = true;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					running = true;
				}
			}
		}
		else
		{
			// we use world-relative directions in the case of no main camera
			m_Move = v * Vector3.forward + h * Vector3.right;
			if (m_Move.magnitude > 0)
			{
				walking = true;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					running = true;
				}
			}
		}

		// pass all parameters to the character control script
		m_Character.Move(m_Move, walking, running);
	}
}
