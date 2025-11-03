using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Combined DungeonGenerator with tilemap painting, directional wall variants,
// and spawning for: chests (with rare/legendary/etc. chances using the provided ranges),
// monster spawns, a start point and a finish point.
// Attach to an empty GameObject, assign Tilemap + tile variants and prefabs in Inspector,
// then press the component context menu "Generate" or enable generateOnStart.

[ExecuteAlways]
public class DungeonGenerator : MonoBehaviour
{
    [Header("Grid")]
    public int width = 100;
    public int height = 60;
    public int seed = 0;
    public bool randomSeed = true;

    [Header("Rooms")]
    public int maxRooms = 20;
    public int roomAttempts = 100;
    public int minRoomWidth = 4;
    public int maxRoomWidth = 12;
    public int minRoomHeight = 4;
    public int maxRoomHeight = 12;
    public bool allowRoomOverlap = false;

    [Header("Tilemap & Tiles")]
    public Tilemap tilemap;
    public TileBase floorTile;

    public TileBase wallHorizontalNorth;
    public TileBase wallHorizontalSouth;
    public TileBase wallVerticalEast;
    public TileBase wallVerticalWest;
    public TileBase wallCornerNE;
    public TileBase wallCornerNW;
    public TileBase wallCornerSE;
    public TileBase wallCornerSW;
    public TileBase wallSingle;

    [Header("Options")]
    public bool generateOnStart = true;
    public bool autoClearTilesBeforePaint = true;

    // Spawning configuration
    [Header("Spawning")]
    // Chest spawn prefab (a single chest prefab). When a chest is spawned, the chest itself can be a prefab
    // that has visuals and logic to show contents. Here we optionally instantiate different chest "content"
    // prefabs directly in place of the chest or as children; for simplicity we spawn one chestPrefab and
    // optionally spawn a content prefab on top based on the chance roll below.
    public GameObject chestPrefab;
    public GameObject chestContentNormalPrefab;      // fallback
    public GameObject chestContentSpecialPrefab;     // <=30
    public GameObject chestContentSuperSpecialPrefab;// 40-50
    public GameObject chestContentMegaSpecialPrefab; // 80-85
    public GameObject chestContentUltimatePrefab;    // ==90

    // Monster spawn prefabs (the "enemyPrefab" base plus special variants if you want)
    public GameObject monsterPrefab;                 // common monster
    public GameObject specialEnemyPrefab;            // <=30
    public GameObject SuperspecialEnemyPrefab;       // 40-50
    public GameObject megaspecialEnemyPrefab;        // 80-85
    public GameObject ultimatespecialEnemyPrefab;    // ==90

    // Other important points
    public GameObject startPrefab;   // player start marker
    public GameObject finishPrefab;  // finish marker

    [Header("Spawn rules")]
    public int spawnPerRoom = 1;              // monsters per room (random positions inside room)
    public int chestChanceAttempts = 1;       // how many chest attempts per room (0 disables)
    public float spawnYOffset = 0f;
    public Transform spawnParent;            // optional parent to keep spawned objects organized

    // Internal
    private bool[,] grid;
    private List<RectInt> rooms = new List<RectInt>();

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (width <= 0 || height <= 0) return;
        if (randomSeed) seed = Environment.TickCount;
        UnityEngine.Random.InitState(seed);

        grid = new bool[width, height];
        rooms.Clear();

        // Place rooms
        for (int i = 0; i < roomAttempts; i++)
        {
            int rw = UnityEngine.Random.Range(minRoomWidth, maxRoomWidth + 1);
            int rh = UnityEngine.Random.Range(minRoomHeight, maxRoomHeight + 1);
            int rx = UnityEngine.Random.Range(1, Mathf.Max(2, width - rw - 1));
            int ry = UnityEngine.Random.Range(1, Mathf.Max(2, height - rh - 1));
            RectInt r = new RectInt(rx, ry, rw, rh);

            bool overlaps = false;
            foreach (var other in rooms)
            {
                if (allowRoomOverlap)
                {
                    if (r.Overlaps(other)) overlaps = true;
                }
                else
                {
                    RectInt otherExp = new RectInt(other.xMin - 1, other.yMin - 1, other.width + 2, other.height + 2);
                    if (r.Overlaps(otherExp)) { overlaps = true; break; }
                }
            }

            if (!overlaps)
            {
                rooms.Add(r);
                CarveRoomIntoGrid(r);
                if (rooms.Count >= maxRooms) break;
            }
        }

        // Connect rooms with 1-tile corridors
        if (rooms.Count > 1)
        {
            rooms.Sort((a, b) => GetRectCenterX(a).CompareTo(GetRectCenterX(b)));
            for (int i = 1; i < rooms.Count; i++)
            {
                Vector2Int cA = GetRectCenter(rooms[i - 1]);
                Vector2Int cB = GetRectCenter(rooms[i]);
                CarveCorridor(cA, cB);
            }
        }

        // Paint tiles
        PaintToTilemap();

        // Spawn objects (deterministic relative to seed)
        SpawnAllObjects();

        Debug.Log($"Dungeon generated: {rooms.Count} rooms, seed {seed}");
    }

    private void CarveRoomIntoGrid(RectInt r)
    {
        for (int x = r.xMin; x < r.xMax; x++)
            for (int y = r.yMin; y < r.yMax; y++)
                if (InBounds(x, y)) grid[x, y] = true;
    }

    private void CarveCorridor(Vector2Int a, Vector2Int b)
    {
        if (UnityEngine.Random.value < 0.5f)
        {
            CarveHorizontal(a.x, b.x, a.y);
            CarveVertical(a.y, b.y, b.x);
        }
        else
        {
            CarveVertical(a.y, b.y, a.x);
            CarveHorizontal(a.x, b.x, b.y);
        }
    }

    private void CarveHorizontal(int x1, int x2, int y)
    {
        int start = Mathf.Min(x1, x2);
        int end = Mathf.Max(x1, x2);
        for (int x = start; x <= end; x++) if (InBounds(x, y)) grid[x, y] = true;
    }

    private void CarveVertical(int y1, int y2, int x)
    {
        int start = Mathf.Min(y1, y2);
        int end = Mathf.Max(y1, y2);
        for (int y = start; y <= end; y++) if (InBounds(x, y)) grid[x, y] = true;
    }

    private bool InBounds(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;

    private void PaintToTilemap()
    {
        if (tilemap == null) return;
        if (autoClearTilesBeforePaint) tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x - width / 2, y - height / 2, 0);
                if (grid[x, y])
                {
                    if (floorTile != null) tilemap.SetTile(pos, floorTile);
                    else tilemap.SetTile(pos, null);
                }
                else
                {
                    TileBase chosen = ChooseDirectionalWallTile(x, y);
                    tilemap.SetTile(pos, chosen);
                }
            }
    }

    private TileBase ChooseDirectionalWallTile(int x, int y)
    {
        bool north = (y + 1 < height) && grid[x, y + 1];
        bool south = (y - 1 >= 0) && grid[x, y - 1];
        bool west = (x - 1 >= 0) && grid[x - 1, y];
        bool east = (x + 1 < width) && grid[x + 1, y];

        // corners
        if (north && east && wallCornerNE != null) return wallCornerNE;
        if (north && west && wallCornerNW != null) return wallCornerNW;
        if (south && east && wallCornerSE != null) return wallCornerSE;
        if (south && west && wallCornerSW != null) return wallCornerSW;

        if (north && wallHorizontalNorth != null) return wallHorizontalNorth;
        if (south && wallHorizontalSouth != null) return wallHorizontalSouth;
        if (east && wallVerticalEast != null) return wallVerticalEast;
        if (west && wallVerticalWest != null) return wallVerticalWest;

        return wallSingle;
    }

    // --- Spawning logic ---

    // Called after tiles are painted
    private void SpawnAllObjects()
    {
        // deterministic RNG based on seed
        System.Random rng = new System.Random(seed);

        // Clear previous spawned children if spawnParent assigned (optional cleanup)
        if (spawnParent != null)
        {
            for (int i = spawnParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(spawnParent.GetChild(i).gameObject);
            }
        }

        if (rooms.Count == 0) return;

        // Place start at first room center and finish at last room center (deterministic order)
        Vector2Int startCell = GetRectCenter(rooms[0]);
        Vector2Int finishCell = GetRectCenter(rooms[rooms.Count - 1]);

        SpawnOneAtCell(startPrefab, startCell, "StartPoint", true);
        SpawnOneAtCell(finishPrefab, finishCell, "FinishPoint", true);

        // For each room spawn monsters and maybe chests
        for (int i = 0; i < rooms.Count; i++)
        {
            RectInt r = rooms[i];

            // Monsters per room (spawnPerRoom)
            for (int m = 0; m < spawnPerRoom; m++)
            {
                // choose random tile inside room
                int rx = rng.Next(r.xMin, r.xMax);
                int ry = rng.Next(r.yMin, r.yMax);
                if (!InBounds(rx, ry)) continue;
                if (!grid[rx, ry]) { m--; continue; } // ensure on floor

                // perform chance roll for special monster variants using same ranges you provided
                int randomNumber = rng.Next(1, 551); // 1..550 inclusive (Matches Random.Range(1,551))
                GameObject prefabToSpawn = monsterPrefab;

                if (randomNumber <= 30)
                {
                    prefabToSpawn = specialEnemyPrefab;
                }
                else if (randomNumber >= 40 && randomNumber <= 50)
                {
                    prefabToSpawn = SuperspecialEnemyPrefab;
                }
                else if (randomNumber >= 80 && randomNumber <= 85)
                {
                    prefabToSpawn = megaspecialEnemyPrefab;
                }
                else if (randomNumber == 90)
                {
                    prefabToSpawn = ultimatespecialEnemyPrefab;
                }
                // else prefabToSpawn stays monsterPrefab

                SpawnOneAtCell(prefabToSpawn, new Vector2Int(rx, ry), $"Monster_room{i}_{m}", false);
            }

            // Chests attempts per room
            for (int c = 0; c < chestChanceAttempts; c++)
            {
                int cx = rng.Next(r.xMin, r.xMax);
                int cy = rng.Next(r.yMin, r.yMax);
                if (!InBounds(cx, cy)) continue;
                if (!grid[cx, cy]) { c--; continue; }

                // roll for chest content using same logic and ranges
                int randomNumber = rng.Next(1, 551);
                // spawn the chest itself
                GameObject chest = SpawnOneAtCell(chestPrefab, new Vector2Int(cx, cy), $"Chest_room{i}_{c}", false);

                // attach content prefab according to roll as a child of chest or spawn at same position
                if (chest != null)
                {
                    GameObject contentPrefab = chestContentNormalPrefab;
                    if (randomNumber <= 30) contentPrefab = chestContentSpecialPrefab;
                    else if (randomNumber >= 40 && randomNumber <= 50) contentPrefab = chestContentSuperSpecialPrefab;
                    else if (randomNumber >= 80 && randomNumber <= 85) contentPrefab = chestContentMegaSpecialPrefab;
                    else if (randomNumber == 90) contentPrefab = chestContentUltimatePrefab;
                    // if contentPrefab null, nothing extra spawns

                    if (contentPrefab != null)
                    {
                        Vector3 worldPos = GridToWorld(cx, cy);
                        GameObject cont = Instantiate(contentPrefab, worldPos, Quaternion.identity);
                        cont.transform.SetParent(chest.transform, true);
                        if (spawnParent != null) cont.transform.SetParent(chest.transform, true);
                    }
                }
            }
        }
    }

    // Instantiate a prefab at grid cell (x,y). Returns created GameObject or null.
    private GameObject SpawnOneAtCell(GameObject prefab, Vector2Int cell, string name, bool isMarker)
    {
        if (prefab == null) return null;
        Vector3 pos = GridToWorld(cell.x, cell.y);
        GameObject inst = Instantiate(prefab, pos, Quaternion.identity);
        inst.name = prefab.name + "_" + name;
        inst.transform.position = pos;
        if (spawnParent != null) inst.transform.SetParent(spawnParent, true);

        // Optional tweak: keep marker objects (start/finish) visible in editor by tagging or layer - left to user
        return inst;
    }

    // Convert grid coordinates to world position consistent with Tilemap placement
    private Vector3 GridToWorld(int x, int y)
    {
        Vector3Int cellPos = new Vector3Int(x - width / 2, y - height / 2, 0);
        if (tilemap != null)
        {
            // CellToWorld returns bottom-left of cell; add half cell to get center
            Vector3 world = tilemap.CellToWorld(cellPos) + (Vector3)tilemap.cellSize * 0.5f;
            world.y += spawnYOffset;
            return world;
        }
        else
        {
            Vector3 world = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f + spawnYOffset, 0f);
            return world;
        }
    }

    // Helpers for rect centers
    private Vector2Int GetRectCenter(RectInt r)
    {
        int cx = r.xMin + r.width / 2;
        int cy = r.yMin + r.height / 2;
        return new Vector2Int(cx, cy);
    }

    private int GetRectCenterX(RectInt r) => r.xMin + r.width / 2;

    private void OnValidate()
    {
        width = Mathf.Max(8, width);
        height = Mathf.Max(8, height);
        maxRooms = Mathf.Max(1, maxRooms);
        roomAttempts = Mathf.Max(1, roomAttempts);
        minRoomWidth = Mathf.Max(1, minRoomWidth);
        minRoomHeight = Mathf.Max(1, minRoomHeight);
        if (maxRoomWidth < minRoomWidth) maxRoomWidth = minRoomWidth;
        if (maxRoomHeight < minRoomHeight) maxRoomHeight = minRoomHeight;
    }

    private void Start()
    {
        if (generateOnStart && Application.isPlaying) Generate();
    }

#if UNITY_EDITOR
    private void Update() { }
#endif
}