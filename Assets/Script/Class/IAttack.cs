using UnityEngine;

/// <summary>
/// 타워의 공격 기능이 고통으로 구현해야 하는 동작을 정의
/// 공격 방식이 근접, 투사체, 범위 공격으로 달라도 외부에서는 같은 방식으로 호출할 수 있게 함
/// </summary>
public interface IAttack
{
    /// <summary>
    /// 지정된 적을 대상으로 공격을 시작
    /// 타워의 공격 주기가 되었을 때 공격 관리 스크립트에서 호출
    /// </summary>
    /// <param name="target"><이번 공격 대상/param>
    /// <param name="range">공격 범위</param>
    /// <param name="level">공격력 계산에 사용될 타워 레벨</param>
    void Execute(Enemy target, float range, float level);

    /// <summary>
    /// 타워의 기본 공격력과 레벨 증가량을 이용해 최종 공격력을 계산
    /// 공격이 실제로 적중하는 시점에 호출
    /// </summary>
    float GetDamage(float level);
}