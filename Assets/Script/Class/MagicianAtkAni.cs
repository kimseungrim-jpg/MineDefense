using UnityEngine;

/// <summary>
/// 마법사 모델의 공격 애니메이션 이벤트를 MagicainAttack에 전달
/// 자식 Animator에서 발생한 발사 시점과 부모 오브젝트의 투사체 생성 로직을 연결
/// </summary>
public class MagicianAtkAni : MonoBehaviour
{
    MagicainAttack magic;

    private void Awake()
    {
        magic = GetComponentInParent<MagicainAttack>();
    }

    /// <summary>
    /// 마법이 발사되는 공격 애니메이션 프레임에서 호출
    /// 실제 투사체 생성은 MagicainAttack.Dealing에서 수행
    /// </summary>
    public void Attack()
    {
        magic.Dealing();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.SfxType.MagicianAttack);
        }
    }
}
