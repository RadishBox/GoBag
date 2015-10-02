using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Controls the map in the exploration game part
/// </summary>
public class MapController : MonoBehaviour
{

	[SerializeField]
	private int _height;
	[SerializeField]
	private int _width;

	public GameObject PathVisualizerPrefab;

	private static MapController _instance;
	public static MapController Instance
	{
		get { return _instance; }
		set { _instance = value; }
	}

	// Use this for initialization
	void Awake ()
	{
		Instance = this;
	}

	public virtual void GenerateMap()
	{

	}

	/// <summary>
	/// Gets a tile from the map, given its coordinates
	/// </summary>
	public MapTile GetTile(Vector2 coordinates)
	{
		MapTile tile = null;

		string childName = coordinates.x + "," + coordinates.y;

		Transform tileGO = transform.FindChild("Tiles").FindChild(childName);

		if (tileGO)
		{
			tile = tileGO.gameObject.GetComponent<MapTile>();
		}

		return tile;
	}

	/// <summary>Returns a random tile from the map, which is passable.</summary>
	public MapTile GetRandomTile(bool passable = true)
	{
		int x, y = 0;
		bool validTile = false;
		Vector2 targetCoords;
		MapTile tile = null;

		// Get a random coord
		while (!validTile)
		{
			x = Random.Range(0, Width);
			y = Random.Range(0, Height);

			targetCoords = new Vector2(x, y);

			tile = GetTile(targetCoords);

			if (tile.Passable && !tile.Occupied && !tile.IsObjective) // Tile cant be an objective tile
			{
				validTile = true;
			}
		}

		return tile;
	}

	public List<MapTile> GetRow(int rowNumber)
	{
		List<MapTile> tiles = new List<MapTile>();

		for (int i = 0; i < Width; i++)
		{
			Vector2 coords = new Vector2(i, rowNumber);
			MapTile tile = GetTile(coords);
			tiles.Add(tile);
		}

		return tiles;
	}

	public void ClearPathToObjective(MapTile fromTile)
	{
		List<MapTile> path = GetPath(fromTile, GetObjectiveTile());
		ClearPath(path);
	}

	// Clears all impassable obstacles from a given path of tiles
	public void ClearPath(List<MapTile> pathTiles)
	{
		foreach (MapTile tile in pathTiles) 
		{
			if ((tile.EntityInTile != null) && !(tile.EntityInTile is IMovable))
			{
				// Destroy that entity
				ExploreGameController.Instance.Entities.Remove(tile.EntityInTile);
				Destroy(tile.EntityInTile.gameObject);
			}
		}
	}

	// Path finding algorithm
	/// <summary>Will return a set of tiles that make a path from one tile to another.</summary>
	public List<MapTile> GetPath(MapTile fromTile, MapTile toTile, bool visualize = false)
	{
		List<MapTile> pathTiles = new List<MapTile>();
		List<MapTile> frontier = new List<MapTile>();
		Dictionary<string, MapTile> CameFrom = new Dictionary<string, MapTile>();
		Dictionary<string, float> CostSoFar = new Dictionary<string, float>();

		fromTile.Priority = 0;
		frontier.Add(fromTile);
		CameFrom.Add(fromTile.name, null);
		CostSoFar.Add(fromTile.name, 0);

		MapTile current = null;

		while (frontier.Count > 0)
		{
			frontier.Sort();
			current = frontier[0]; // Get first element in list
			frontier.RemoveAt(0);

			// Check if current is goal
			if (current.Position == toTile.Position)
				break;

			// Iterate through each neighbor in current
			foreach (MapTile next in GetNeighbors(current))
			{
				float newCost = CostSoFar[current.name] + DistanceBetweenTiles(current, next);
				if ((!CostSoFar.ContainsKey(next.name)) || newCost < CostSoFar[next.name] )
				{
					if (CostSoFar.ContainsKey(next.name))
					{
						CostSoFar[next.name] = newCost;
						CameFrom[next.name] = current;
					}
					else
					{
						CostSoFar.Add(next.name, newCost);						
						CameFrom.Add(next.name, current);
					}
					float priority = newCost + DistanceBetweenTiles(toTile, next);
					next.Priority = priority;
					frontier.Add(next);
				}
			}
		}

		// Get path taken
		do
		{
			if(visualize)
			{
				GameObject visualizer = Instantiate(PathVisualizerPrefab);
				visualizer.transform.SetParent(current.transform, false);
			}
			pathTiles.Add(current);

			current = CameFrom[current.name];
		}
		while (current != null);

		return pathTiles;
	}

	private float Heuristic(MapTile fromTile, MapTile toTile)
	{

		return 0;
	}

	private float DistanceBetweenTiles(MapTile fromTile, MapTile toTile)
	{
		return Mathf.Abs(Vector2.Distance(fromTile.Position, toTile.Position));
	}

	/// <summary>The neighbors of a tile is all tiles around it that are passable.</summary>
	private List<MapTile> GetNeighbors(MapTile fromTile)
	{
		List<MapTile> neighbors = new List<MapTile>();

		// Up
		MapTile tile;
		tile = GetTile(fromTile.Position + Vector2.up);
		if (tile && tile.Passable)
			neighbors.Add(tile);

		// Down
		tile = GetTile(fromTile.Position + Vector2.down);
		if (tile && tile.Passable)
			neighbors.Add(tile);

		// Left
		tile = GetTile(fromTile.Position + Vector2.left);
		if (tile && tile.Passable)
			neighbors.Add(tile);

		// Right
		tile = GetTile(fromTile.Position + Vector2.right);
		if (tile && tile.Passable)
			neighbors.Add(tile);

		return neighbors;
	}

	private float Heuristic(MapTile tile)
	{
		return 0;
	}

	public MapTile GetObjectiveTile(bool passable = true)
	{
		// Iterate through all tiles from top to bottom
		for (int i = Height - 1; i > 0; i--)
		{
			for (int j = Width - 1; j > 0; j--)
			{
				MapTile tile = GetTile(new Vector2(j, i));
				if (tile.IsObjective && tile.Passable)
				{
					return tile;
				}
			}
		}

		return null;
	}

	#region Properties

	public int Width
	{
		get { return _width; }
		set { _width = value; }
	}

	public int Height
	{
		get { return _height; }
		set { _height = value; }
	}

	#endregion

}
