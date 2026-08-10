using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance; //전역 접근용 인스턴스 싱글톤

    public int gold = 0; //플레이어 보유 골드

    private void Awake()
    {
        if (instance == null) //아직 goldmanager가 없다면 이 객체를 대표 인스턴스로 설정
            instance = this;
        else
            Destroy(gameObject); //이미 있다면 중복 생성이므로 제거
    }

    public void AddGold(int amount) // 골드추가 함수
    {
        gold += amount; //받아온 값을 골드에 추가
        //Debug.Log("Gold: " + gold); //정상적으로 오르는지 체크하기 위한 디버그로그
    }
}
