using UnityEngine;

/// <summary>
/// 궁수의 공격 애니메이션 이벤트를 AcherAttack에 전달
/// Animator가 자식 오브젝트에 있고 공격 로직이 부모에 있을 때 두 컴포넌트를 연결하는 역할
/// </summary>
public class AcherATKAni : MonoBehaviour
{
    AcherAttack achAtkAni;

    /// <summary>
    /// 오브젝트가 생성될 때 부모 계층에서 궁수 공격 컴포넌트를 찾아 저장
    /// 이후 애니메이션 이벤트가 발생했을 때 실제 공격 로직을 호출하기 위해 사용
    /// </summary>
    void Awake()
    {
        achAtkAni = GetComponentInParent<AcherAttack>();
    }

    /// <summary>
    /// 화살이 발사되어야 하는 공격 애니메이션 프레임에서 호출
    /// 실제 화살 생성과 피해량 설정은 AcherAttack.Dealing에서 처리
    /// </summary>
    public void Attack()
    {
        achAtkAni.Dealing();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.SfxType.ArcherAttack);
        }
    }
}
