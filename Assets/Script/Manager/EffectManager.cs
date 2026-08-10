using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public GameObject levelupPrefab;
    public GameObject hpUpPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerLevelupEffect(Vector3 position)
    {
        if (levelupPrefab != null)
        {
            GameObject effect = Instantiate(levelupPrefab, position, Quaternion.identity);

            Destroy(effect, 2f);
        }
    }

    public void PlayerHpUpEffect(Vector3 position)
    {
        if (levelupPrefab != null)
        {
            GameObject effect2 = Instantiate(hpUpPrefab, position, Quaternion.identity);

            Destroy(effect2, 2f);
        }
    }
}
