using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] wayPoints;

    public void SpawnEnemy()
    {
        GameObject obj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
        Enemy enemyState = obj.GetComponent<Enemy>();

        //Debug.Log(WaveManager.instance.hpLevelUp);
        enemyState.HpUp(WaveManager.instance.hpLevelUp);
        enemy.SetWayPoints(wayPoints);
    }
}
