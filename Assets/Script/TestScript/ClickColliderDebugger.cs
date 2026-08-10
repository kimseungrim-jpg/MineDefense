using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 마우스 위치에 겹친 모든 2D 콜라이더를 출력하여
/// 월드 클릭을 가로채는 오브젝트를 확인하는 진단용 스크립트입니다.
/// </summary>
public class ClickColliderDebugger : MonoBehaviour
{
    /// <summary>
    /// 클릭이 발생했을 때 해당 월드 좌표의 모든 Collider2D를 출력합니다.
    /// 입력 차단 원인을 조사하는 동안 매 프레임 호출됩니다.
    /// </summary>
    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);

        Debug.Log(
            $"[클릭 충돌 검사] 위치: {worldPosition}, " +
            $"감지 개수: {colliders.Length}"
        );

        foreach (Collider2D collider in colliders)
        {
            Debug.Log(
                $"[감지 Collider] {collider.name} / " +
                $"Layer: {LayerMask.LayerToName(collider.gameObject.layer)} / " +
                $"Tag: {collider.tag} / " +
                $"Enabled: {collider.enabled} / " +
                $"Trigger: {collider.isTrigger}",
                collider.gameObject
            );
        }
    }
}