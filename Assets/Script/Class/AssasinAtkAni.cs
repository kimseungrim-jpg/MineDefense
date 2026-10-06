using UnityEngine;

/// <summary>
/// 암살자 모델의 공격 애니메이션 이벤트를 AssasinAttack에 전달
/// 자식 Animator에서 발생한 이벤트와 부모 오브젝트의 실제 공격 로직을 연결
/// </summary>
public class AssasinAtkAni : MonoBehaviour
{
    AssasinAttack assasin;

    private void Awake()
    {
        assasin = GetComponentInParent<AssasinAttack>();
    }

    /// <summary>
    /// 공격이 적중하는 애니메이션 프레임에서 호출
    /// 실제 피해 처리는 AssasinAttack.Dealing에서 수행
    /// </summary>
    public void Attack()
    {
        assasin.Dealing();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.SfxType.AssasinAttack);
        }
    }

}
