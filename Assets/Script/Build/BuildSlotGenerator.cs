using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 빌드 가능한 타일맵의 각 타일 위치에 BuildSlot을 생성하는 스크립트
/// 씬이 시작되면 지정된 타일맵을 순회하고 실제 타일이 존재하는 위치에만 슬롯을 배치
/// </summary>
public class BuildSlotGenerator : MonoBehaviour
{
    [SerializeField] Tilemap buildTilemap;
    [SerializeField] GameObject buildSlotPrefab;
    [SerializeField] Transform slotParent;
    
    /// <summary>
    /// 타일맵 구성이 완료된 상태에서 빌드 슬롯 생성 시작
    /// </summary>
    void Start()
    {
        GenerateSlots();
    }

    /// <summary>
    /// buildTilemap의 전체 셀 범위를 검사
    /// 실제 타일이 배치된 셀마다 빌드 슬롯을 하나씩 생성
    /// Start에서 한 번 호충
    /// </summary>
    void GenerateSlots()
    {
        foreach (Vector3Int cellPos in buildTilemap.cellBounds.allPositionsWithin)
        {
            // cellBounds에는 빈 셀도 포함해서 실제 타일이 없는 위치는 제외
            if (!buildTilemap.HasTile(cellPos)) 
                continue;

            Vector3 worldPos = buildTilemap.CellToWorld(cellPos) + buildTilemap.cellSize / 2;

            Instantiate(buildSlotPrefab, worldPos, Quaternion.identity, slotParent);
        }
    }
}
