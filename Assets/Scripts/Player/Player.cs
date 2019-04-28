using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Inventory))]
public class Player : MonoBehaviour
{

	Inventory inventory;
	PlayerController controller;
	PlayerControlInput ci;
	[SerializeField]
	private GameObject[] handTools;
	[HideInInspector]
	public Tool activeTool;

	[SerializeField]
	private float maxStamina = 100f;

	[HideInInspector]
	public float stamina;
	[HideInInspector]
	public bool walking, running, attacking;
	[SerializeField]
	public float walkSpeed = 2.0f;
	[SerializeField]
	public float runSpeed = 2.0f;
	public float staminaRefreshRate = 2;
	public float runStaminaCost = 4;
	public float dashStaminaCost = 10;

	public HUDManager hud;

	void Awake()
	{
		inventory = GetComponent<Inventory>();
		controller = GetComponent<PlayerController>();
		ci = GetComponent<PlayerControlInput>();
		EquipAxe();
		stamina = maxStamina;
		walking = false; 
		running = false;
		attacking = false;
	}

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if(running == false && attacking == false)
		{
			if (stamina < maxStamina)
			{
				if(walking == true)
					stamina += (Time.deltaTime * staminaRefreshRate);
				else
					stamina += (Time.deltaTime * staminaRefreshRate * staminaRefreshRate);
				hud.UpdateStamina(stamina.ToString("f0"));
			}
		}
		// Debug.Log("Stamina: " + stamina);
	}

	public bool NextToTile(Vector3 pos)
	{
		return controller.NextToTile(pos);
	}

	public void Move(Vector3 pos, bool running)
	{
		controller.Move(pos, true, running);
	}

	void OnTriggerEnter(Collider col)
	{
		Pickupable item = col.gameObject.GetComponent<Pickupable>();
		if (item != null)
		{
			inventory.AddItem(col);
			//item.PickUp();
		}
		else
		{
			return;
		}
	}

	public void EquipAxe()
	{
		activeTool = handTools[0].GetComponentInChildren<Tool>();
		activeTool.gameObject.SetActive(true);
	}

	public void UnequipTool()
	{
		activeTool.gameObject.SetActive(false);
		activeTool = null;
	}

	public void BeginToolAction()
	{
		activeTool.inUse = true;
		attacking = true;
	}

	public void EndToolAction()
	{
		activeTool.inUse = false;
		attacking = false;
	}
}
