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

	public void Move(Vector3 pos, bool walking, bool running)
	{
		player.walking = walking;
		player.running = running;
		mo7Anim.SetFloat("walk_speed", player.walkSpeed);
		mo7Anim.SetBool("walking_forward", walking);
		if(pos.magnitude == 0)
		{
			return;
		}
		Vector3 move = pos * Time.deltaTime * player.walkSpeed;
		if(running)
		{
			player.stamina -= (Time.deltaTime * player.runStaminaCost);
			if(player.stamina <= 0)
			{
				player.stamina = 0;
				player.running = false;
			}
			else
			{
				move *= player.runSpeed;
			}
			player.hud.UpdateStamina(player.stamina.ToString("f0"));
		}
		mo7Anim.SetBool("running", player.running);
		control.Move(move);
		if (pos != Vector3.zero)
		{
			transform.forward = pos;	
		}
	}

	public void Dash()
	{
		if (player.stamina - player.dashStaminaCost < 0)
		{
			return;
		}
		else
		{
			player.stamina -= player.dashStaminaCost;
			mo7Anim.SetTrigger("dash");
			player.hud.UpdateStamina(player.stamina.ToString("f0"));
			transform.position += transform.forward * 2f;
		}
	}
	public void Attack()
	{
		if(player.stamina - player.activeTool.staminaCost < 0)
		{
			return;
		}
		else
		{
			player.stamina -= player.activeTool.staminaCost;
			mo7Anim.SetTrigger("attack");
			player.hud.UpdateStamina(player.stamina.ToString("f0"));
		}
	}

	public void Update()
	{

		
	}
}
