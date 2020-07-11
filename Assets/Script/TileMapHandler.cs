using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileMapHandler : MonoBehaviour
{
	public static TileMapHandler Instance;

	[System.Serializable] public class MapInfo {
		public string tileMapName;
		public Tilemap tilemap;
	}
	[System.Serializable] public class TileInfo {
		public char tileRep;
		public Tile tile;
	}

	[SerializeField] List<MapInfo> mapInfoList = new List<MapInfo>();
	[SerializeField] List<TileInfo> tileInfoList = new List<TileInfo>();

	Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
 	Dictionary<char, Tile> tileIdentifier = new Dictionary<char, Tile>();

	[SerializeField] Tile wallDefault;
	[SerializeField] Tile wallTopMid;
	[SerializeField] Tile wallTopDownRight;
	[SerializeField] Tile wallTopDownLeft;
	[SerializeField] Tile wallTopLeft;
	[SerializeField] Tile wallTopRight;
	[SerializeField] Tile invisWall;

	void Awake() {
		Instance = this;
		foreach (MapInfo info in mapInfoList) {
			tilemaps[info.tileMapName] = info.tilemap;
		}
		foreach (TileInfo info in tileInfoList) {
			tileIdentifier[info.tileRep] = info.tile;
		}
	}

	void ReRender(string mapName, string[] mapString) {
		Tilemap current = tilemaps[mapName];
		current.ClearAllTiles();

		for (int i = 0; i < mapString.Length; i++) {
			for (int j = 0; j < mapString[i].Length; j++) {
				if (tileIdentifier.ContainsKey(mapString[i][j])) {
					current.SetTile(new Vector3Int(j, i, 0), tileIdentifier[mapString[i][j]]);
				}
			}
		}
	}	

	void SetTile(Tilemap map, int x, int y, Tile tile) {
		map.SetTile(new Vector3Int(x, y, 0), tile);
	}

	public void GenerateWalls(string wallTopTileMapName, string wallBotTileMapName, string floorTileMapName, int dimX, int dimY) {
		print("DRAWING WALL");
		Tilemap floorMap = tilemaps[floorTileMapName];
		Tilemap wallTopMap = tilemaps[wallTopTileMapName];
		Tilemap wallBotMap = tilemaps[wallBotTileMapName];
		wallTopMap.ClearAllTiles();
		wallBotMap.ClearAllTiles();

		bool[,] s = new bool[4*dimX, 4*dimY];

		int o = 2*dimX;

		for (int x = -dimX; x < dimX; x++)
			for (int y = -dimY; y < dimY; y++)
				s[x + o, y + o] = floorMap.GetTile(new Vector3Int(x, y, 0)) != null;


		for (int x = -dimX; x <= dimX; x++) {
			for (int y = -dimY; y <= dimY; y++) {
				if (s[x + o, y - 2 + o] && !s[x + o, y - 1 + o] && s[x + o - 1, y - 1 + o])
					SetTile(wallTopMap, x, y, wallTopDownRight);
				else if (s[x + o, y - 2 + o] && !s[x + o, y - 1 + o] && s[x + o + 1, y - 1 + o])
					SetTile(wallTopMap, x, y, wallTopDownLeft);
				else if (!s[x + o, y - 1 + o] && !s[x + o, y + o] && s[x + o - 1, y - 1 + o])
					SetTile(wallBotMap, x, y, wallTopLeft);
				else if (!s[x + o, y - 1 + o] && !s[x + o, y + o] && s[x + o + 1, y - 1 + o])
					SetTile(wallBotMap, x, y, wallTopRight);
				else if (s[x + o, y - 1 + o] && !s[x + o, y - 2 + o])
					SetTile(wallBotMap, x, y, wallTopMid);
				else if (s[x + o, y - 2 + o] && !s[x + o, y - 1 + o]) 
					SetTile(wallTopMap, x, y, wallTopMid);
				else if (s[x + o, y - 1 + o] && !s[x + o, y + o])
					SetTile(wallTopMap, x, y, wallDefault);
				else if (s[x + o, y + o] && !s[x + o, y - 1 + o])
					SetTile(wallBotMap, x, y, wallDefault);
			}
		}
	}

	public void GenerateBounds(string floorTileMapName, string invisWallTileMapName, int dimX, int dimY) {
		Tilemap floorMap = tilemaps[floorTileMapName];
		Tilemap boundsMap = tilemaps[invisWallTileMapName];
		boundsMap.ClearAllTiles();

		int[] dx = new int[]{-1, 1, 0, 0, 1, -1, 1, -1};
		int[] dy = new int[]{0, 0, -1, 1, 1, 1, -1, -1};

		for (int x = -dimX; x <= dimX; x++) {
			for (int y = -dimY; y <= dimY; y++) {
				int around = 0;
				for (int k = 0; k < dx.Length; k++) {
					int xx = x + dx[k], yy = y + dy[k];
					around += floorMap.GetTile(new Vector3Int(xx, yy, 0)) != null ? 1 : 0;	
				}
				if (around > 0 && !floorMap.GetTile(new Vector3Int(x, y, 0)))
					SetTile(boundsMap, x, y, invisWall);
			}
		}
	}

	public void GenerateProjectileBounds(string floorTileMapName, string invisWallTileMapName, int dimX, int dimY) {
		Tilemap floorMap = tilemaps[floorTileMapName];
		Tilemap boundsMap = tilemaps[invisWallTileMapName];
		boundsMap.ClearAllTiles();

		int[] dx = new int[]{-1, 1, 0, 0, 1, -1, 1, -1};
		int[] dy = new int[]{0, 0, -1, 1, 1, 1, -1, -1};

		for (int x = -dimX; x <= dimX; x++) {
			for (int y = -dimY; y <= dimY; y++) {
				int around = 0;
				for (int k = 0; k < dx.Length; k++) {
					int xx = x + dx[k], yy = y + dy[k];
					around += floorMap.GetTile(new Vector3Int(xx, yy, 0)) != null ? 1 : 0;	
				}
				if (around > 0 && !floorMap.GetTile(new Vector3Int(x, y, 0)))
					SetTile(boundsMap, x, y, invisWall);
			}
		}
	}
	
	public void LoadTileMap(string mapName, string fileName) {
		string[] lines = System.IO.File.ReadAllLines(fileName);	
		ReRender(mapName, lines);
	}
}
