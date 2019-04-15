using UnityEngine;

public class Grid : MonoBehaviour
{
	[SerializeField]
	private float size = 1f;

	//grid specifics
	[SerializeField]
	private int rows;
	[SerializeField]
	private int cols;
	[SerializeField]
	private Vector3 gridSize;
	[SerializeField]
	private Vector3 gridOffset;

	//about cells
	[SerializeField]
	private Sprite cellSprite;
	private Vector2 cellSize;
	private Vector2 cellScale;
	private GameObject cellsObj;

	private float[] filledLocations;
	[SerializeField]
	private GameManager gm;


	public Vector3 GetNearestPointOnGrid(Vector3 position)
	{
		position -= transform.position;

		int xCount = Mathf.RoundToInt(position.x / size);
		//int yCount = Mathf.RoundToInt(position.y / size);
		int zCount = Mathf.RoundToInt(position.z / size);

		Vector3 result = new Vector3(
			 (float)xCount * size,
			 .5f,
			 (float)zCount * size);

		result += transform.position;

		return result;
	}

	void Start()
	{

	}

	public void InitCells()
	{
		cellsObj = new GameObject();
		cellsObj.transform.parent = transform;
		GameObject cellObject = new GameObject();

		//creates an empty object and adds a sprite renderer component -> set the sprite to cellSprite
		cellObject.AddComponent<SpriteRenderer>().sprite = cellSprite;
		//cellObject.GetComponent<SpriteRenderer>().color = Color.green;
		//catch the size of the sprite
		cellSize = cellSprite.bounds.size;

		//get the new cell size -> adjust the size of the cells to fit the size of the grid
		Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.z / (float)rows);

		//Get the scales so you can scale the cells and change their size to fit the grid
		cellScale.x = newCellSize.x / cellSize.x;
		cellScale.y = newCellSize.y / cellSize.y;

		cellSize = newCellSize; //the size will be replaced by the new computed size, we just used cellSize for computing the scale

		cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

		//fix the cells to the grid by getting the half of the grid and cells add and minus experiment
		gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
		gridOffset.z = -(gridSize.z / 2) + cellSize.y / 2;

		//fill the grid with cells by using Instantiate
		for (float row = 0; row < rows; row += 1)
		{
			for (float col = 0; col < cols; col += 1)
			{
				//add the cell size so that no two cells will have the same x and y position
				var point = GetNearestPointOnGrid(new Vector3(row, .5f, col));
				//instantiate the game object, at position pos, with rotation set to identity
				if (gm.CheckLocationEmpty(point))
				{
					GameObject cO = Instantiate(cellObject, point, Quaternion.identity) as GameObject;
					cO.tag = "Cell";
					cO.AddComponent<BoxCollider>();
					cO.AddComponent<Cell>();

					//set the parent of the cell to GRID so you can move the cells together with the grid;
					cO.transform.parent = cellsObj.transform;
					cO.transform.Rotate(90, 0, 0, Space.World);
				}
			}
		}

		//destroy the object used to instantiate the cells
		Destroy(cellObject);
	}

	public void DestroyCells()
	{
		Destroy(cellsObj);
	}

	//so you can see the width and height of the grid on editor
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, gridSize);
		Gizmos.color = Color.yellow;
		for (float x = 0; x < rows; x += 1)
		{
			for (float z = 0; z < cols; z += 1)
			{
				var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
				Gizmos.DrawSphere(point, 0.5f);
			}

		}
	}

}