using UnityEngine;

public interface IAttack
{
    void Execute(Enemy target, float rnage, float level);
    float GetDamage(float level);
}