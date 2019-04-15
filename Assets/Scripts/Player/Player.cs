using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	Inventory inventory;
	PlayerController controller;
	void Start()
    {
        inventory = GetComponent<Inventory>();
		controller = GetComponent<PlayerController>();
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
}
