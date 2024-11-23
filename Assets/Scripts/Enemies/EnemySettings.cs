using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemySettings", menuName = "Enemy/Settings")]
public class EnemySettings : ScriptableObject
{
    public int Health;
    public float DamageResistance;
    public float WalkSpeed;
    public float AttackRange;
    public int Difficulty;
    public int Damage;
}

