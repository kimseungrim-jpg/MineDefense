using UnityEngine;

public class AcherATKAni : MonoBehaviour
{
    AcherAttack achAtkAni;
    void Awake()
    {
        achAtkAni = GetComponentInParent<AcherAttack>();
    }

    public void Attack()
    {
        achAtkAni.Dealing();
    }
}
