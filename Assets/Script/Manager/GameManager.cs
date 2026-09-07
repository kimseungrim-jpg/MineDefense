using UnityEngine;

/// <summary>
/// 게임 종료 상태를 관리하고 게임 오버 UI 표시와 전체 시간 정지를 요청
/// 광부의 체력이 모두 소진되었을 때 게임 종료 흐름의 진입점으로 사용
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameOver = false;

    /// <summary>
    /// GameManager를 전역 접근 인스턴스로 등록
    /// 이미 다른 인스턴스가 있다면 게임 종료 상태가 중복 관리되지 않도록 현재 오브젝트를 제거
    /// </summary>
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 게임을 한 번만 종료 상태로 전환하고 게임 오버 UI를 표시한 뒤 게임 시간을 정지
    /// 적이 목적지에 도달해 광부의 체력이 모두 소진되었을 때 Miner에서 호출
    /// </summary>
    public void GameOver()
    {
        // 한번에 여러 피해가 들어와도 게임 오버 처리가 중복 실행되지 않도록 차단 예외처리
        if (isGameOver) return;

        isGameOver = true;
        UiManager.instance.ShowGameOver();

        Time.timeScale = 0;
    }
}
