using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;

    public int goldReward = 10;
    private float hitFlashTime = 0.3f;

    private bool isDead = false;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    public void HpUp(float Up)
    {
        maxHp = maxHp * Up;
    }

    public void Damage(float damge)
    {
        if (isDead) return;

        currentHp -= damge;
        //Debug.Log(currentHp);

        StopAllCoroutines();
        StartCoroutine(HitFlash());

        if (currentHp <= 0)
        {
            isDead = true;
            GoldManager.instance.AddGold(goldReward);
            Die();
        }
    }

    IEnumerator HitFlash()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(hitFlashTime);
        sprite.color = Color.white;
    }

    public void Die()
    {
        WaveManager.instance.OnEnemyDead();
        Destroy(gameObject);
    }
}
