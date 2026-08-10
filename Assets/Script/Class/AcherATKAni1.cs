using UnityEngine;

public class AcherATKAni1 : MonoBehaviour
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
