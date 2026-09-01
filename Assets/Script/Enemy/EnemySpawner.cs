using UnityEngine;

/// <summary>
/// 적 프리팹을 지정한 위치에 생성하고 웨이브 배율과 이동 경로를 초기화
/// 실제 생성 시점과 반복 횟수는 외부의 WaveManager가 결정
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] wayPoints;

    /// <summary>
    /// 적 한 개체를 생성한 뒤 현재 웨이브의 체력 배율과 웨이포인트를 전달
    /// WaveManager가 웨이브 진행에 맞춰 적을 출현시킬 때 호출
    /// </summary>
    public void SpawnEnemy()
    {
        GameObject obj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
        Enemy enemyState = obj.GetComponent<Enemy>();

        enemyState.HpUp(WaveManager.instance.hpLevelUp);
        enemy.SetWayPoints(wayPoints);
    }
}
