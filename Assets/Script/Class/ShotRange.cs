using UnityEngine;

public class ShotRange : MonoBehaviour
{
    public float Speed = 5f;

    private Enemy target;
    private float damage;

    [SerializeField]
    float explosionRange = 1f;

    public void SetTarget(Enemy enemy, float dmg)
    {
        target = enemy;
        damage = dmg;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Explosion();

    }

    void Explosion()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;

            enemy.Damage(damage);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
