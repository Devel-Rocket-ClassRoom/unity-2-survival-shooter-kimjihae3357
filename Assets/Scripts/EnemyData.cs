using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 100f;
    public float damage = 10f;
    public float moveSpeed = 3.5f;
}
