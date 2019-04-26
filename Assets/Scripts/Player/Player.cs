using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent (typeof(Inventory))]
[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{

	Inventory inventory;
	PlayerController controller;
    PlayerControlInput ci;

	[SerializeField]
	public GameObject hand;
	void Start()
    {
        inventory = GetComponent<Inventory>();
		controller = GetComponent<PlayerController>();
		ci = GetComponent<PlayerControlInput>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool NextToTile(Vector3 pos)
    {
		return controller.NextToTile(pos);
	}

    public void Move(Vector3 pos)
    {
		controller.Move(pos);
	}

    void OnTriggerEnter(Collider col)
    {
		Pickupable item = col.gameObject.GetComponent<Pickupable>();
        if(item != null) 
        {
			inventory.AddItem(col);
			//item.PickUp();
		}
	}
}
