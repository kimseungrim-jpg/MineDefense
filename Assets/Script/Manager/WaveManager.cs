using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public EnemySpawner spawner;

    public bool isAutoMode = false;
    private bool isWaitingForStart = false;

    public int currentWave = 1; //현재 웨이브
    public int enemiesPerWave = 5; //웨이브동안 나올 적의 수
    public float spawnInterval = 1f; //적이 스폰되는 시간
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

    public void OnStartBtnClick()
    {
        TryStartWaitingWave();
    }

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

    public void IncreaseLevel()
    {
        hpLevelUp += currentWave * 0.2f;
    }

    private void Start()
    {
        isWaitingForStart = true;
    }

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

    public void OnEnemyDead()
    {
        if (!isWaveAlive) return;

        aliveEnemise--;
        //Debug.Log(aliveEnemise);

        if (aliveEnemise <= 0)
        {
            aliveEnemise = 0;
            isWaveAlive = false;
            if (waveRoutine != null) StopCoroutine(waveRoutine);
            waveRoutine = StartCoroutine(NextWave());
        }
    }

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

    void StartNextWave()
    {
        currentWave++;
        waveRoutine = StartCoroutine(StartWave());
    }
}

