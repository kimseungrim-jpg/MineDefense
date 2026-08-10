using System.Collections;
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField]
    float maxHp = 100f;
    public float currentHp;

    
    public float miningSpeed = 3f;
    public int upgradeCost = 50;

    [SerializeField]
    int orePerCycle = 1;
    

    void Start()
    {
        currentHp = maxHp;

        StartCoroutine(ProduceOre()); //코루틴 시작
    }

    public void MineTakeDamage(float dmg)
    {
        currentHp -= dmg;
        //Debug.Log(currentHp);

        MinerUpgradeUI.Instance.UpdateUI();

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public void UpgradeMiner()
    {
        if (GoldManager.instance.gold >= upgradeCost)
        {
            if (miningSpeed > 0.3f)
            {
                GoldManager.instance.gold -= upgradeCost;

                currentHp = maxHp;

                miningSpeed -= 0.3f;

                if (miningSpeed > 0.3f)
                {
                    upgradeCost += 50;
                }
                else if(miningSpeed <= 0.3f)
                {
                    miningSpeed = 0.3f;

                    upgradeCost = 50;
                }

                EffectManager.instance.PlayerLevelupEffect(transform.position);
            }
            else
            {
                GoldManager.instance.gold -= upgradeCost;

                currentHp = maxHp;

                EffectManager.instance.PlayerHpUpEffect(transform.position);
            }

            //선택완료후 ui 제거 로직 자리
            //MinerUpgradeUI.Instance.Hide();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        TowerInfoUI.instance?.Hide();

        //선택시 ui 표시 로직
        MinerUpgradeUI.Instance.Show(this);
    }

    IEnumerator ProduceOre()
    {
        while (true)
        {
            if (WaveManager.instance != null && WaveManager.instance.isWaveAlive)
            {

                yield return new WaitForSeconds(miningSpeed);
                OreManager.instance.AddOre(orePerCycle);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }

        }
    }

    void Die()
    {
        //게임 매니저 호출
        GameManager.instance.GameOver();

        StopAllCoroutines(); //코루틴 정지
    }

}
