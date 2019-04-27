using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class GridV4 : MonoBehaviour
{
	[SerializeField]
	private int sizeX = 50, sizeZ = 50;

	[SerializeField]
	public float tileSize;
	[SerializeField]
	private GameObject[] trees;

	private Mesh land;
	private GameObject grid;
	private List<Vector3> locations;
	private List<Vector3> filledLocations;
	private List<GameObject> objectsInScene;
	public bool buildMode = false;
	[SerializeField]
	private bool placeRandomTrees = true;

	[SerializeField]
	private int numTrees;

	[SerializeField]
	private bool optimzeTrees = false;

	[SerializeField]
	private Material treeMaterial;

	private GridMouse mouse;
	private GameManager gm;
	private int limitX;
	private int limitZ;

	void Awake()
	{
		mouse = GetComponent<GridMouse>();
		gm = GetComponent<GameManager>();
		limitX = sizeX - 1;
		limitZ = sizeZ - 1;
	}
	void Start()
	{
		Generate();
		//SpawnPlayer();
	}
	public void Generate()
	{
		locations = new List<Vector3>();
		filledLocations = new List<Vector3>();
		objectsInScene = new List<GameObject>();
		if (transform.childCount > 0)
		{
			foreach (Transform child in transform)
			{
				DestroyImmediate(child.gameObject);
				DestroyImmediate(child);
			}
		}
		GameObject t = null, g = null, w = null;
		BuildMesh();
		if (placeRandomTrees)
		{
			t = placeRandTrees();
		}

		if (generateGrass)
		{
			g = placeGrass();

		}
		if (generateFlowers)
		{
			w = placeFlowers();
		}
		if (t != null)
		{
			if (optimzeTrees)
				OptimzeMesh(t, treeMaterial);
		}
		if (g != null)
		{
			if (optimzeGrassMesh)
				OptimzeMesh(g, grassMaterial);
		}
		if (w != null)
		{
			if (optimizeFlowersMesh)
				OptimzeMesh(w, flowerMaterial);
		}
		//PlaceBaackground();
		if (buildMode)
		{
			InitCells();
		}
	}

	public void MovePlayer(Vector3 pos)
	{
		// gm.MovePlayer(pos);
	}

	private void SpawnPlayer()
	{
		gm.Spawn();
	}

	private void BuildMesh()
	{
		int numTiles = sizeX * sizeZ;
		int numTris = numTiles * 2;

		int vsize_x = sizeX + 1;
		int vsize_z = sizeZ + 1;
		int numVerts = vsize_x * vsize_z;

		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[numTris * 3];

		int x, z;
		for (z = 0; z < vsize_z; z++)
		{
			for (x = 0; x < vsize_x; x++)
			{
				Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);
				vertices[z * vsize_z + x] = pos;
				if (pos.x != 0 && pos.z != 0 && pos.x < limitX && pos.z < limitZ)
					locations.Add(pos);
				normals[z * vsize_x + x] = Vector3.up;
				uv[z * vsize_x + x] = new Vector2((float)x / vsize_x, (float)z / vsize_z);
			}
		}

		for (z = 0; z < sizeZ; z++)
		{
			for (x = 0; x < sizeX; x++)
			{
				int squareIndex = z * sizeX + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = z * vsize_x + x + 0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;

				triangles[triOffset + 3] = z * vsize_x + x + 0;
				triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 5] = z * vsize_x + x + 1;
			}

		}

		land = new Mesh();
		land.vertices = vertices;
		land.triangles = triangles;
		land.normals = normals;
		land.uv = uv;

		MeshFilter mf = GetComponent<MeshFilter>();
		MeshCollider mc = GetComponent<MeshCollider>();

		mf.mesh = land;
		mc.sharedMesh = land;
	}

	[SerializeField]
	private Material grassMaterial;

	private void OptimzeMesh(GameObject parent, Material mat)
	{
		// Debug.Log("parent: " + parent.name);
		MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];

		int i = 0;
		while (i < meshFilters.Length)
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			// Debug.Log("meshFilters[i].gameObject: " + meshFilters[i].gameObject.name);
			meshFilters[i].gameObject.transform.parent.transform.parent.gameObject.SetActive(false);

			i++;
		}
		MeshFilter mf = parent.AddComponent<MeshFilter>();
		MeshRenderer mr = parent.AddComponent<MeshRenderer>();
		mf.sharedMesh = new Mesh();
		mf.sharedMesh.CombineMeshes(combine);
		mr.material = mat;
		parent.transform.gameObject.SetActive(true);
	}

	private GameObject placeRandTrees()
	{
		GameObject y = new GameObject();
		y.gameObject.name = "Trees";
		y.transform.parent = transform;

		for (int i = 0; i < numTrees; i++)
		{
			int rInd2 = Random.Range(0, locations.Count);
			int rInd = Random.Range(0, trees.Length);
			GameObject t = trees[rInd] as GameObject;
			Vector3 rLoc = locations[rInd2];
			if (CheckCellEmpty(rLoc))
			{
				PlaceOnCell(t, rLoc, y.transform);
			}
		}
		return y;
	}

	[SerializeField]
	private GameObject grassPrefab;
	[SerializeField]
	private int numGrass = 200;
	[SerializeField]
	private bool generateGrass = false;

	[SerializeField]
	private bool optimzeGrassMesh = false;
	private GameObject placeGrass()
	{
		GameObject y = new GameObject();
		y.gameObject.name = "Grass Root";
		y.transform.parent = transform;

		for (int i = 0; i < numGrass; i++)
		{
			int rInd2 = Random.Range(0, locations.Count);
			Vector3 rLoc = locations[rInd2];
			while (!CheckCellEmpty(rLoc))
			{
				rInd2 = Random.Range(0, locations.Count);
				rLoc = locations[rInd2];
			}
			PlaceOnCell(grassPrefab, rLoc, y.transform);
		}
		return y;
	}

	[SerializeField]
	private GameObject[] flowerPrefabs;
	[SerializeField]
	private int numFlowers = 200;
	[SerializeField]
	private bool generateFlowers = true;
	[SerializeField]
	private Material flowerMaterial;

	[SerializeField]
	private bool optimizeFlowersMesh;

	private GameObject placeFlowers()
	{
		GameObject y = new GameObject();
		y.gameObject.name = "Flowers Root";
		y.transform.parent = transform;

		for (int i = 0; i < numFlowers; i++)
		{
			int rInd2 = Random.Range(0, locations.Count);
			Vector3 rLoc = locations[rInd2];
			GameObject flowerPrefab = flowerPrefabs[Random.Range(0, flowerPrefabs.Length)];
			if (CheckCellEmpty(rLoc) == true)
			{
				PlaceOnCell(flowerPrefab, rLoc, y.transform);
			}
		}
		return y;
	}

	private Vector3 FindCenter(Vector3 pos)
	{
		int xCount = Mathf.FloorToInt(pos.x / tileSize);
		int zCount = Mathf.FloorToInt(pos.z / tileSize);
		pos = new Vector3((float)xCount * tileSize, 0, (float)zCount * tileSize);
		return pos;
	}

	public void InitCells(GameObject obj)
	{
		grid = new GameObject();
		grid.gameObject.name = "Cells";
		grid.transform.parent = transform;
		MeshFilter filter = grid.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = grid.AddComponent<MeshRenderer>();
		var mesh = new Mesh();
		var verticies = new List<Vector3>();

		var indicies = new List<int>();
		for (int i = 0; i < sizeX; i++)
		{
			verticies.Add(new Vector3(i * tileSize, 0f, 0));
			verticies.Add(new Vector3(i * tileSize, 0f, sizeX * tileSize));

			indicies.Add(4 * i + 0);
			indicies.Add(4 * i + 1);

			verticies.Add(new Vector3(0, 0f, i * tileSize));
			verticies.Add(new Vector3(sizeX * tileSize, 0f, i * tileSize));

			indicies.Add(4 * i + 2);
			indicies.Add(4 * i + 3);
		}

		mesh.vertices = verticies.ToArray();
		mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
		filter.mesh = mesh;

		meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
		meshRenderer.material.color = Color.white;
		buildMode = true;
		mouse.ActivatePreview(obj);
	}

	public void InitCells()
	{
		grid = new GameObject();
		grid.gameObject.name = "Cells";
		grid.transform.parent = transform;
		MeshFilter filter = grid.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = grid.AddComponent<MeshRenderer>();
		var mesh = new Mesh();
		var verticies = new List<Vector3>();

		var indicies = new List<int>();
		for (int i = 0; i < sizeX; i++)
		{
			verticies.Add(new Vector3(i * tileSize, 0f, 0));
			verticies.Add(new Vector3(i * tileSize, 0f, sizeX * tileSize));

			indicies.Add(4 * i + 0);
			indicies.Add(4 * i + 1);

			verticies.Add(new Vector3(0, 0f, i * tileSize));
			verticies.Add(new Vector3(sizeX * tileSize, 0f, i * tileSize));

			indicies.Add(4 * i + 2);
			indicies.Add(4 * i + 3);
		}

		mesh.vertices = verticies.ToArray();
		mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
		filter.mesh = mesh;
		meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
		meshRenderer.material.color = Color.white;
		buildMode = true;
	}

	public void DestroyCells()
	{
		buildMode = false;
		if (mouse.preview != null)
		{
			Destroy(mouse.preview);
			mouse.preview = null;
		}
		Destroy(grid);
	}

	public bool CheckCellEmpty(Vector3 pos)
	{
		return !filledLocations.Contains(pos);
	}

	public GameObject GetCellObject(GameObject ob)
	{
		return objectsInScene.Find(obj => obj == ob);
	}

	public void PlaceOnCell(GameObject obj, Vector3 pos, Transform parent)
	{
		GameObject o = obj;
		GameObject e = Instantiate(o, pos, Quaternion.identity);
		e.transform.parent = parent;
		GBuilding t = e.GetComponentInChildren<GBuilding>();
		if (t != null)
		{
			t.BeginProduction();
			t.GetComponentInChildren<MeshCollider>().isTrigger = false;
			t.GetComponentInChildren<Building>().selectionArea.SetActive(false);
		}
		Camera.main.fieldOfView = 10f;
		RegisterObject(pos, e);
	}

	public void PlaceOnCell(GameObject obj, Vector3 pos, Quaternion rotation, Transform parent)
	{
		GameObject o = obj;
		GameObject e = Instantiate(o, pos, rotation);
		e.transform.parent = parent;
		GBuilding t = e.GetComponentInChildren<GBuilding>();
		if (t != null)
		{
			t.BeginProduction();
			t.GetComponentInChildren<MeshCollider>().isTrigger = false;
			t.GetComponentInChildren<Building>().selectionArea.SetActive(false);
		}
		Camera.main.fieldOfView = 10f;
		RegisterObject(pos, e);
	}

	public void RegisterObject(Vector3 pos, GameObject obj)
	{
		AddToFilled(pos);
		AddToObjectsInScene(obj);
	}

	private void AddToFilled(Vector3 pos)
	{
		filledLocations.Add(pos);
	}

	private void AddToObjectsInScene(GameObject obj)
	{
		objectsInScene.Add(obj);
	}

	public void RemoveObjectFromLocations(Vector3 pos, GameObject obj)
	{
		filledLocations.Remove(pos);
		objectsInScene.Remove(obj);
	}

	public Vector3 GetMouseTileCoordinate()
	{
		return mouse.GetMousePosition();
	}

	public Vector3 GetTileCoordinate(Vector3 hitInfo)
	{
		Vector3 currentTileCoord = new Vector3();
		int x = Mathf.FloorToInt(hitInfo.x / tileSize);
		int z = Mathf.FloorToInt(hitInfo.z / tileSize);

		currentTileCoord.x = x;
		currentTileCoord.z = z;
		currentTileCoord = currentTileCoord * tileSize;

		return currentTileCoord;

	}

	public bool IsPlacingBuilding()
	{
		return mouse.preview != null;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
