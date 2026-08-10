using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //전역 접근용 인스턴스 싱글톤

    private bool isGameOver = false; //게임이 끝났는지 판별하는 변수

    private void Awake()
    {
        if (instance == null) //아직 GameManager가 없다면 이 객체를 대표 인스턴스로 설정
            instance = this;
        else
            Destroy(gameObject); //이미 있다면 중복 생성이므로 제거
    }

    public void GameOver()
    {
        if (isGameOver) return; //이미 게임오버일시 예외처리

        isGameOver = true; //현재상태를 게임오버로 변경
        UiManager.instance.ShowGameOver();

        Time.timeScale = 0; //게임 정지
    }
}
