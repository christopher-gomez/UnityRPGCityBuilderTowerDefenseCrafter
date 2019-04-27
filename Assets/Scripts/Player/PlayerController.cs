using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerControlInput))]
public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private GameManager gm;
	private CharacterController control;
	[SerializeField]
	private float speed = 10f;
	private Animator mo7Anim;
	private Player player;

	public void Start()
	{
		control = GetComponent<CharacterController>();
		mo7Anim = GetComponent<Animator>();
		player = GetComponent<Player>();
	}

	public bool NextToTile(Vector3 pos)
	{
		return Vector3.Distance(pos, transform.position) < 1.5f;
	}

	public void Move(Vector3 pos, bool walking)
	{
		mo7Anim.SetFloat("walk_speed", speed);
		mo7Anim.SetBool("walking", walking);
		control.Move(pos * Time.deltaTime * speed);
		if (pos != Vector3.zero)
			transform.forward = pos;
	}

	public void FixedUpdate()
	{

		if (Input.GetMouseButtonDown(1))
		{
			mo7Anim.SetTrigger("attack");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
					transform.LookAt(lookAt);
					transform.forward = heading;
				}
				//
			}
		}
	}
}
