using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public float damage = 25f;
    public int startAmmoRemain = 80;
    public int magCapacity = 25;

    public float timeBetFrie = 0.12f;
    public float reloadTime = 1.8f;
}
