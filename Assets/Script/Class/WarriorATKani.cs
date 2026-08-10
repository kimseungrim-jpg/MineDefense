using UnityEngine;

public class WarriorATKani : MonoBehaviour
{
    WarriorAttack attack;
    void Awake()
    {
        attack = GetComponentInParent<WarriorAttack>();
    }

    // Update is called once per frame
    public void Attack()
    {
        //Debug.Log("나는 기본공격");
        attack.Dealing();
    }
}
