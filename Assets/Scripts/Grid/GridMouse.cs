using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


[RequireComponent(typeof(GridV4))]
public class GridMouse : MonoBehaviour
{
	GridV4 _tileMap;

	Vector3 currentTileCoord;

	public Transform selectionCube;

	public GameObject building = null;

	public GameObject preview = null;
	private Collider col;

	void Start()
	{
		_tileMap = GetComponent<GridV4>();
		col = GetComponent<Collider>();
	}

	public void ActivatePreview(GameObject building)
	{
		this.building = building;
		this.preview = Instantiate(building, new Vector3(1000, 0, 1000), Quaternion.identity);
	}

	// Update is called once per frame
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;

		// Cursor on the map
		if (col.Raycast(ray, out hitInfo, Mathf.Infinity))
		{
			int x = Mathf.FloorToInt((float)hitInfo.point.x / _tileMap.tileSize);
			int z = Mathf.FloorToInt((float)hitInfo.point.z / _tileMap.tileSize);

			currentTileCoord = _tileMap.GetTileCoordinate(hitInfo.point);

			selectionCube.gameObject.SetActive(true);
			selectionCube.transform.position = currentTileCoord;
			// In build mode and cursor on the map
			if (_tileMap.buildMode)
			{
				//selectionCube.gameObject.SetActive(false);
				if (preview != null)
				{
					preview.transform.position = currentTileCoord;
				}
				if (Input.GetMouseButtonDown(0))
				{
					_tileMap.PlaceOnCell(building, currentTileCoord, _tileMap.gameObject.transform);
					_tileMap.DestroyCells();
				}
			}

			// Not in build mode but cursor on the map
			else
			{
				if (Input.GetMouseButtonDown(0))
				{
					_tileMap.MovePlayer(currentTileCoord);
				}
			}
		}

		// Cursor Off the map
		else
		{
			selectionCube.gameObject.SetActive(false);
		}
	}

	public Vector3 GetMousePosition()
	{
		return currentTileCoord;
	}
}
