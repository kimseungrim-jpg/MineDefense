using UnityEngine;

public class ShotAttack : MonoBehaviour
{
    public float Speed = 5f;

    private Enemy target;
    private float damage;

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
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
