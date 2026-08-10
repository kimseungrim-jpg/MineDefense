using System.Collections;
using UnityEngine;

public class WarriorSpin : MonoBehaviour
{
    [SerializeField] float duration = 1.5f;
    [SerializeField] float damage = 5f;
    [SerializeField] float range = 1.5f;
    [SerializeField] float interval = 0.2f;
    [SerializeField] Animator animator;

    public bool isSpinning;

    public void Active()
    {
        if (isSpinning) return;
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        isSpinning = true;
        animator.SetBool("Spin", true);

        float time = 0f;

        while (time < duration)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

            foreach (var hit in hits)
            {
                if (!hit.CompareTag("Enemy")) continue;
                hit.GetComponent<Enemy>().Damage(damage);
                Debug.Log("½ºÇÉµ¹¾Æ¿ê");
            }

            time += Time.deltaTime;
            yield return new WaitForSeconds(interval);
        }

        animator.SetBool("Spin", false);
        isSpinning = false;
    }
}
