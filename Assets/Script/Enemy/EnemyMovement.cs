using UnityEngine;

/// <summary>
/// 적이 전달받은 웨이포인트를 순서대로 따라 이동하도록 관리
/// 마지막 지점에 도착하면 광부에게 피해를 주고 적을 웨이브에서 제거
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    public float Speed = 1f; //속도
    public float currentSpeed;
    public Transform[] wayPoint; //목표지점 배열로 생성

    private int index = 0; //지점 번호
    public float dmg = 25;

    /// <summary>
    /// 적이 활성화 된 뒤 현재 웨이브의 속도 배율을 기본 이동 속도에 적용
    /// WaveManager가 없는 테스트 환경에서는 기본 속도를 그대로 사용
    /// </summary>
    private void Start()
    {
        if (WaveManager.instance != null)
        {
            currentSpeed = Speed * WaveManager.instance.enemySpeedUp;
        }
        else
        {
            currentSpeed = Speed;
        }
    }

    /// <summary>
    /// 매 프레임 현재 웨이포인트 방향으로 이동
    /// 도착하면 다음 지점으로 전환
    /// 마지막 웨이포인트까지 통과하면 목적지 도착 처리를 실행
    /// </summary>
    void Update()
    {
        if (wayPoint.Length == 0) return; //웨이포인트가 없을때를 대비하기위한 예외처리

        Transform target = wayPoint[index]; //현재 목표 위치 저장

        Vector3 dir = (target.position - transform.position).normalized; //현재 위치에서 목표 지점으로 향하는 방향벡터를 정규화해서 저장
        transform.position += dir * currentSpeed * Time.deltaTime; //웨이포인터까지 이동

        if (Vector3.Distance(transform.position, target.position) < 0.1f) //현재 둘의 거리가 0.1보다 작다면
        {
            index++; //인덱스를 증가시켜 다음지점으로 웨이포인트 변경

            //만약 웨이포인트가 마지막이라면 도착판정
            if (index >= wayPoint.Length) 
            {
                ReachGoal();
            }
        }
    }

    /// <summary>
    /// 이 적이 이동할 웨이포인트 배열을 저장
    /// EnemySpawner가 적 프리팹을 생성한 직후 호출
    /// </summary>
    public void SetWayPoints(Transform[] points)
    {
        wayPoint = points;
    }

    /// <summary>
    /// 적이 마지막 웨이포인트에 도착했을 때 광부에게 피해를 적용하고 적을 제거하는 함수
    /// </summary>
    void ReachGoal()
    {
        Miner miner = FindObjectOfType<Miner>();

        if (miner != null)
        {
            miner.MineTakeDamage(dmg);
        }

        Enemy enemy = GetComponent<Enemy>();

        if(enemy != null)
    {
            enemy.Die();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
