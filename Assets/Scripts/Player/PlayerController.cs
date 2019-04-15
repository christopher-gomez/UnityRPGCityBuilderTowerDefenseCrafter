using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
	private NavMeshAgent agent;
	[SerializeField]
	private float moveSpeed = 4f;

	[SerializeField]
	private GameManager gm;

	Vector3 right, forward;
	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);

		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
	}

	public void Move(Vector3 pos)
	{
		agent.SetDestination(pos);
	}

	public bool NextToTile(Vector3 pos)
	{
		return Vector3.Distance(pos, transform.position) < 1.5f;
	}

	public void Update()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
			Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

			if (rightMovement == new Vector3(0, 0, 0))
			{
				upMovement = upMovement * 1.2f;
			}
			if (upMovement == new Vector3(0, 0, 0))
			{
				rightMovement = rightMovement * 1.2f;
			}

			Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

			transform.forward = heading;
			transform.position += rightMovement;
			transform.position += upMovement;
			agent.SetDestination(transform.position);
		}
		Vector3 mp = gm.MousePosition();
		Vector3 la = new Vector3();
		if (Vector3.Distance(mp, transform.position) < 1.5)
			la = new Vector3(mp.x, 0, mp.z);
		else  //Vector3 la = new Vector3(mp.x*30f, 45, mp.z);
			la = new Vector3(mp.x, .5f, mp.z);
		if (Vector3.Angle(transform.position, la) > .5){}
			//transform.LookAt(la);
	}
}
