using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureObject : DestructableObject, Pickupable
{

	public int stacks;
    [Tooltip("Instantiated Icon Reference, don't set it")]
	public GameObject icon;
	[SerializeField]
	private GameObject iconPrefab;
	// Start is called before the first frame update
	public override void Start()
    {
		stacks = 1;
		base.Start();
	}

    // Update is called once per frame
    public override void Update()
    {
		base.Update();
	}

    public int GetStacks()
    {
		return stacks;
	}

    public void SetStacks(int stacks)
    {
		this.stacks = stacks;
	}

    public void IncrementStacks(int stacks)
    {
        this.stacks +=stacks;
    }

    public void PickUp()
    {

    }

    public GameObject GetIcon()
    {
		return icon;
	}

    public GameObject GetIconPrefab()
    {
		return iconPrefab;
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		gameManager.DeregisterObject(gameObject);
	}

}
