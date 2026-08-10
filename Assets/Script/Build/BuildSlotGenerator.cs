using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildSlotGenerator : MonoBehaviour
{
    [SerializeField] Tilemap buildTilemap;
    [SerializeField] GameObject buildSlotPrefab;
    [SerializeField] Transform slotParent;
    
    void Start()
    {
        GenerareSlots();
    }

    void GenerareSlots()
    {
        foreach (Vector3Int cellPos in buildTilemap.cellBounds.allPositionsWithin)
        {
            if (!buildTilemap.HasTile(cellPos)) continue;

            Vector3 worldPos = buildTilemap.CellToWorld(cellPos) + buildTilemap.cellSize / 2;

            Instantiate(buildSlotPrefab, worldPos, Quaternion.identity, slotParent);
        }
    }
}
