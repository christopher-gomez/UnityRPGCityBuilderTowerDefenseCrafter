using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerControlInput))]
public class PlayerController : MonoBehaviour
{
	private NavMeshAgent agent;

	[SerializeField]
	private GameManager gm;

	private CharacterController control;

	[SerializeField]
	private float speed = 10f;



	public void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		control = GetComponent<CharacterController>();
	}

	public bool NextToTile(Vector3 pos)
	{
		return Vector3.Distance(pos, transform.position) < 1.5f;
	}

	public void Move(Vector3 pos)
	{
		control.Move(pos * Time.deltaTime * speed);
		if (pos != Vector3.zero)
			transform.forward = pos;
	}

	public void Update()
	{
		Vector3 mousePos = gm.MousePosition();
		Vector3 lookAt = new Vector3(mousePos.x, transform.position.y, mousePos.z);
		var heading = lookAt - transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		
		if(direction != transform.position/transform.position.magnitude)
			transform.LookAt(lookAt);
	}
}
