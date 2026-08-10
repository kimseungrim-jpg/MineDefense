using UnityEngine;

public class AssasinAtkAni : MonoBehaviour
{
    AssasinAttack assasin;

    private void Awake()
    {
        assasin = GetComponentInParent<AssasinAttack>();
    }

    public void Attack()
    {
        assasin.Dealing();
    }

}
