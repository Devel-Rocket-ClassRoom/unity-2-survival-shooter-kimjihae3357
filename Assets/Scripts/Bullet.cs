using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public Transform fireTransform;

    private Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public  void Shoot (Vector3 direction)
    {
        rb.linearVelocity = direction * bulletSpeed;

        Destroy(gameObject, 1f);
        
    }
}
