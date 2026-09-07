using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 웨이브의 시작과 종료, 적 생성 수, 생성 간격 및 웨이브별 적 능력치 증가를 관리
/// 수동 시작과 자동 진행이 동일한 웨이브 시작 흐름을 사용하도록 제어
/// </summary>
public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public EnemySpawner spawner;

    public bool isAutoMode = false;
    private bool isWaitingForStart = false;

    public int currentWave = 1;
    public int enemiesPerWave = 5;
    public float spawnInterval = 1f;
    public float enemySpeedUp = 1f;

    public float hpLevelUp = 1f;

    int aliveEnemise = 0;
    int fWavecnt = 0;

    public bool isWaveAlive = false;

    public Button autoBtn;
    public Color autoOn = Color.gray;
    public Color autoOff = Color.white;

    private Coroutine waveRoutine;
    

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 수동 시작 버튼 입력을 공통 웨이브 시작 처리로 전달
    /// Game 씬의 웨이브 시작 버튼이 클릭되었을 때 호출
    /// </summary>
    public void OnStartBtnClick()
    {
        TryStartWaitingWave();
    }

    /// <summary>
    /// 자동 진행 모드로 전환하고 버튼 색상을 현재 상태에 맞게 갱신
    /// 자동 모드를 켠 시점에 시작 대기 중인 웨이브가 있다면 즉시 시작을 요청
    /// </summary>
    public void AutoMode()
    {
        isAutoMode = !isAutoMode;

        Image btnImage = autoBtn.GetComponent<Image>();

        if (btnImage != null)
        {
            btnImage.color = isAutoMode? autoOn : autoOff;
        }

        if (isAutoMode)
        {
            TryStartWaitingWave();
        }
    }

    /// <summary>
    /// 웨이브 시작 대기 여부를 확인한 뒤 공통 시작 처리를 수행
    /// 수동과 AUTO가 서로 다른 초기화 순서를 타지 않도록 한곳에서만 실행
    /// </summary>
    private void TryStartWaitingWave()
    {
        if (!isWaitingForStart)
        {
            return;
        }

        isWaitingForStart = false;

        // 버튼 선택 상태가 다음 월드 클릭 판정에 남지 않도록 해제합니다.
        UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);

        if (waveRoutine == null)
        {
            waveRoutine = StartCoroutine(StartWave());
            return;
        }

        StartNextWave();
    }

    /// <summary>
    /// 현재 웨이브 번호를 기준으로 적 최대 체력 배율을 누적 증가시키는 함수
    /// 각 웨이브가 시작될 때 첫 적을 생성하기 전에 StartWave에서 호출
    /// </summary>
    public void IncreaseLevel()
    {
        hpLevelUp += currentWave * 0.2f;
    }

    /// <summary>
    /// 첫 웨이브는 수동 또는 자동 시작 입력을 기다리는 상태가 되도록 초기화
    /// </summary>
    private void Start()
    {
        isWaitingForStart = true;
    }

    /// <summary>
    /// 현재 웨이브의 기본 적 수와 생성 간격을 계산하고 5웨이브마다 강화 규칙을 적용
    /// StartWave가 실제 적 생성을 시작하기 전에 호출
    /// </summary>
    void PrepareWave()
    {
        if (currentWave > 5 && currentWave % 5 == 1)
        {
            enemiesPerWave -= 10 * fWavecnt;
            enemySpeedUp = 1f;
        }

        spawnInterval = 1f;
        enemiesPerWave = 5 + (currentWave - 1) * 2;

        if (currentWave % 5 == 0)
        {
            fWavecnt++;
            spawnInterval *= 0.6f;
            enemiesPerWave += 10 * fWavecnt;
            enemySpeedUp += 0.5f;
        }
    }

    /// <summary>
    /// 현재 웨이브 설정을 준비한 뒤 지정된 수의 적을 생성 간격에 맞춰 순차적으로 생성
    /// 수동 시작 또는 자동 진행이 확정되었을 때 코루틴이 실행됨
    /// </summary>
    IEnumerator StartWave()
    {
        isWaveAlive = true;
        PrepareWave();
        IncreaseLevel();

        aliveEnemise = enemiesPerWave;

        //Debug.Log($"Wave {currentWave} Start");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            spawner.SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /// <summary>
    /// 웨이브에 속한 적이 제거될 때 생존 적 수를 줄이고 웨이브 종료 여부를 판정
    /// Enemy.Die에서 호출되며, 마지막 적이 제거되면 다음 웨이브 대기 처리를 시작
    /// </summary>
    public void OnEnemyDead()
    {
        if (!isWaveAlive) return;

        aliveEnemise--;

        if (aliveEnemise <= 0)
        {
            aliveEnemise = 0;
            isWaveAlive = false;
            if (waveRoutine != null) StopCoroutine(waveRoutine);
            waveRoutine = StartCoroutine(NextWave());
        }
    }

    /// <summary>
    /// 웨이브 종료 후 2초 동안 대기하고 자동 모드 여부에 따라 다음 웨이브를 시작하거나 입력을 기다림
    /// 마지막 적이 제거되었을 때 OnEnemyDead에서 코루틴으로 실행
    /// </summary>
    IEnumerator NextWave()
    {
        //Debug.Log($"Wave {currentWave} Clear");
        yield return new WaitForSeconds(2f);
        
        if (isAutoMode)
        {
            StartNextWave();
        }
        else
        {
            isWaitingForStart = true;
        }
    }

    /// <summary>
    /// 웨이브 번호를 증가시키고 새로운 웨이브 생성 코루틴을 시작
    /// 자동 진행 또는 다음 수동 시작 입력이 확정되었을 때 호출
    /// </summary>
    void StartNextWave()
    {
        currentWave++;
        waveRoutine = StartCoroutine(StartWave());
    }
}

