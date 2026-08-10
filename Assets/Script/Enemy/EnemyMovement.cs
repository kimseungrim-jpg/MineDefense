using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 1f; //속도
    public float currentSpeed;
    public Transform[] wayPoint; //목표지점 배열로 생성

    private int index = 0; //지점 번호
    public float dmg = 25;

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

    void Update()
    {
        if (wayPoint.Length == 0) return; //웨이포인트가 없을때를 대비하기위한 예외처리

        Transform target = wayPoint[index]; //현재 목표 위치 저장

        Vector3 dir = (target.position - transform.position).normalized; //현재 위치에서 목표 지점으로 향하는 방향벡터를 정규화해서 저장
        transform.position += dir * currentSpeed * Time.deltaTime; //웨이포인터까지 이동

        if (Vector3.Distance(transform.position, target.position) < 0.1f) //현재 둘의 거리가 0.1보다 작다면
        {
            index++; //인덱스를 증가시켜 다음지점으로 웨이포인트 변경

            if (index >= wayPoint.Length) //만약 웨이포인트가 마지막이라면 도착판정
            {
                ReachGoal();
            }
        }
    }

    public void SetWayPoints(Transform[] points)
    {
        wayPoint = points;
    }

    void ReachGoal() //도착해서 광부체력을 깎고 현재 몬스터를 제거하기 위한 함수
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
