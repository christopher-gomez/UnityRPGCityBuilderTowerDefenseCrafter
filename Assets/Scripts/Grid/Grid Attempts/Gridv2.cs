using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridv2 : MonoBehaviour
{
	[SerializeField]
	private Vector3 landDims;

	[SerializeField]
	private int landHeight;

	[SerializeField]
	private Material grassMaterial;

	[SerializeField]
	private float cellWidth = 2;

	[SerializeField]
	private List<Vector3> gridVecs;

	[SerializeField]
	private Sprite cellSprite;
	private GameObject cells;

	[SerializeField]
	private GameObject[] trees;

	[SerializeField]
	private List<Vector3> filledGridVecs;

	void Awake()
	{
		gridVecs = new List<Vector3>();
		filledGridVecs = new List<Vector3>();
		transform.position = new Vector3(24, 0, 24);
		for (float i = 0; i < landHeight; i += 1.0f)
		{
			createMass(i);
		}
		placeRandTrees();
	}

	private void createMass(float height)
	{
		GameObject layer = new GameObject();
		layer.transform.parent = transform;
		layer.gameObject.name = "Layer " + height.ToString();
		Vector3 size = new Vector3();
		for (int x = 0; x < landDims.x; x++)
		{
			for (int z = 0; z < landDims.z; z++)
			{
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				size = cube.GetComponent<Renderer>().bounds.size;
				Vector3 pos = new Vector3(x, height, z);
				cube.transform.position = pos;
				if (height == landHeight - 1)
				{
					int xCount = Mathf.RoundToInt(pos.x / cellWidth);
					int zCount = Mathf.RoundToInt(pos.z / cellWidth);
					pos = new Vector3((float)xCount * cellWidth, pos.y+.6f, (float)zCount * cellWidth);
					if(!gridVecs.Contains(pos) && pos.x != 0 && pos.z != 0 && pos.x != 49 && pos.z != 49)
						gridVecs.Add(pos);
				}
				cube.transform.parent = layer.transform;
				cube.GetComponent<Renderer>().material = grassMaterial;
			}
		}
		// Debug.Log("Every cube is: " + size);
	}

	private void placeRandTrees()
	{
		GameObject y = new GameObject();
		y.gameObject.name = "Trees";
		y.transform.parent = transform;
		int rAm = Random.Range(20, 80);
		for (int i = 0; i < rAm; i++)
		{
			int rInd = Random.Range(0, trees.Length);
			int rInd2 = Random.Range(0, gridVecs.Count);
			Vector3 rLoc = gridVecs[rInd2];
			if (CheckCellEmpty(rLoc))
			{
				GameObject t = trees[rInd] as GameObject;
				PlaceOnCell(t, rLoc, y.transform);
			}
		}
	}

	public void InitCells()
	{
		cells = new GameObject();
		cells.name = "Cells";
		cells.transform.parent = transform;
		GameObject t = new GameObject();
		t.AddComponent<SpriteRenderer>().sprite = cellSprite;
		t.transform.localScale = new Vector3(6.5f*cellWidth, 6.5f*cellWidth, 6.5f);
		Vector3 s = t.GetComponent<Renderer>().bounds.size;

		foreach (Vector3 pos in gridVecs)
		{
			if (CheckCellEmpty(pos))
			{
				GameObject cO = Instantiate(t, pos, Quaternion.identity) as GameObject;
				cO.tag = "Cell";
				cO.AddComponent<BoxCollider>();
				cO.AddComponent<Cell>();
				//set the parent of the cell to GRID so you can move the cells together with the grid;
				cO.transform.parent = cells.transform;
				cO.transform.Rotate(90, 0, 0, Space.World);
			}

		}
		Debug.Log("Every Cell is: " + s);
		Destroy(t);
	}

	public void DestroyCells()
	{
		Destroy(cells);
	}

	public bool CheckCellEmpty(Vector3 pos)
	{
		return !filledGridVecs.Contains(pos);
	}

	public void PlaceOnCell(GameObject obj, Vector3 pos, Transform parent)
	{
		obj = Instantiate(obj, pos, Quaternion.identity);
		obj.transform.parent = parent;
		filledGridVecs.Add(pos);
	}


	/*void OnDrawGizmos()
	{
		// Gizmos.DrawWireCube(transform.position, gridSize);
		Gizmos.color = Color.yellow;
		foreach(Vector3 pos in gridVecs)
		{
			Gizmos.DrawSphere(pos, .5f);
		}
	}*/
}
