using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bulletPrefab;
    public ParticleSystem muzzleEffect;
    public Transform fireTransform;
    public GunData gunData;
    public float fireDistance = 50f;

    private LineRenderer bulletLineEffect;
    private Coroutine _coShot;


    public enum Stats
    {
        Ready,
        Empty,
        Reloading
    }

    public Stats State {  get; private set; }

    private void Awake()
    {
        bulletLineEffect = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void  Fire()
    {
        if (muzzleEffect != null)
        {
            muzzleEffect.Play();
        }

        
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            endPoint = hit.point;
            Debug.Log("맞은 오브젝트: " + hit.collider.name);

            LivingEntity livingEntity = hit.collider.GetComponentInParent<LivingEntity>();

            if (livingEntity != null)
            {
                Debug.Log("LivingEntity 찾음");
                livingEntity.OnDamage(10f, hit.point, hit.normal);
            }
        }
        else
        {
            
            endPoint = fireTransform.position + fireTransform.forward * fireDistance;
        }

        _coShot = StartCoroutine(ShotEffect(endPoint));


    }

    private  IEnumerator ShotEffect(Vector3 hitPosition)
    {
        bulletLineEffect.SetPosition(0, fireTransform.position);
        bulletLineEffect.SetPosition(1, hitPosition);
        bulletLineEffect.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineEffect.enabled = false;
    }

}
