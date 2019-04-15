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
			Vector3 t = gameObject.transform.position;
			Destroy(gameObject);
			if (dropsItem)
			{
				Instantiate(dropPrefab, t, Quaternion.identity);
			}
		}
	}

	public override void OnRightClick()
	{
		base.OnRightClick();

		// Display health
		Debug.Log("Health: " + health);

		// Check if player is next to object
		if(gameManager.IsPlayerNextToTile(transform.position))
		{
			TakeDamage(1);
		}
		else
		{
			gameManager.MovePlayer(transform.position);
		}

		// Take damage
	}
}
