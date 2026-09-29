using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 타이틀 씬과 게임 씬 사이의 전환 및 종료 요청을 담당
/// </summary>
public class LoadScene : MonoBehaviour
{
    /// <summary>
    /// 현재 씬에서 Game 씬으로 전환
    /// 타이틀 화면의 게임 시작 버튼이 클릭되었을 때 호출
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 게임 시간을 정상 속도로 복구한 뒤 Title 씬으로 전환
    /// </summary>
    public void ReturnTitle()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// 실행 중인 애플리케이션의 종료를 요청
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("[LoadScene]: 종료 버튼 클릭");

        Application.Quit();
    }
}
