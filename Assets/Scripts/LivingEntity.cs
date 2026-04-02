using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public float startingHealth = 100f;
    public float Health {  get; private set; }
    public bool isDead { get; private set; }


    protected  virtual void OnEnable()
    {
        isDead = false;
        Health = startingHealth;

    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {

        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public 

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
