using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NatureObject
{
	// Start is called before the first frame update
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}

	public override void TakeDamage(int damage, Tool.ToolType t)
	{
		base.TakeDamage(damage, t);
		if (dropsItem)
		{
			if (t == Tool.ToolType.Axe)
				Instantiate(dropPrefab, transform.position, Quaternion.identity);
		}
	}

	public override void OnTriggerEnter(Collider col)
	{
		Player player = col.GetComponent<Player>();
		if (player != null)
		{
			return;
		}
		Tool tool = col.transform.parent.GetComponent<Tool>();
		if (tool != null && tool.inUse == true)
			TakeDamage(tool.damage, tool.toolType);
	}
}
