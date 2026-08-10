using UnityEngine;

public class MagicianAtkAni : MonoBehaviour
{
    MagicainAttack magic;

    private void Awake()
    {
        magic = GetComponentInParent<MagicainAttack>();
    }

    public void Attack()
    {
        magic.Dealing();
    }
}
