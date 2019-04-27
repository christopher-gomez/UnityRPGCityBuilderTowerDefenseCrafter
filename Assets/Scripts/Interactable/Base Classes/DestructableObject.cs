using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableObject : WorldObject, Destructable
{

	[SerializeField]
	protected int health = 1;

	[SerializeField]
	protected GameObject dropPrefab;

	[SerializeField]
	protected bool dropsItem;

	public int GetHealth()
	{
		return health;
	}

	public virtual void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			if (dropsItem)
			{
				Instantiate(dropPrefab, transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		}
	}

	public void OnCollisionEnter(Collision col)
	{
		Debug.Log("Collision detected in tree");
	}

	public void OnTriggerEnter(Collider col)
	{
		//Debug.Log("trigger ("+col.gameObject.name+") entered "+gameObject.name);
		TakeDamage(1);
	}

	public override void OnRightClick()
	{
		base.OnRightClick();

		// Display health
		// Debug.Log("Health: " + health);

		// Check if player is next to object
		if (gameManager.IsPlayerNextToTile(transform.position))
		{
			// Take damage
			// TakeDamage(1);
		}
	}
}
