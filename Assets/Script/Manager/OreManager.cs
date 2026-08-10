using UnityEngine;

public class OreManager : MonoBehaviour
{
    public static OreManager instance; //전역 접근용 인스턴스 싱글톤

    public int ore = 0; //플레이어 보유 광석

    private void Awake()
    {
        if (instance == null) //아직 OreManager가 없다면 이 객체를 대표 인스턴스로 설정
            instance = this;
        else
            Destroy(gameObject); //이미 있다면 중복 생성이므로 제거
    }

    public void AddOre(int amount) // 광석추가 함수
    {
        ore += amount; //받아온 값을 광석에 추가
        //Debug.Log("Ore: " + ore); //정상적으로 오르는지 체크하기 위한 디버그로그
    }
}
