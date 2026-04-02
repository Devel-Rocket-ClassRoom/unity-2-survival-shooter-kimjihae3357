using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public enum Status
    {
        Idle,
        Move,
        Attack,
        Die
    }

    public Transform target;
    public HitBox hitbox;
    public ParticleSystem hitEffect;
    public Collider enemyCollider;
    public LayerMask targetLayer;

    public float traceDistance = 10f;
    public float attackInterval = 0.5f;

    private Status currentStatus;
    private NavMeshAgent agent;
    private Animator enemyAnimator;
    private float lastAttackTime;
    private float damage;

    public Status CurrentStatus
    {
        get => currentStatus;

        set
        {
            var prevStatus = currentStatus;
            currentStatus = value;

            switch (currentStatus)
            {
                case Status.Idle:
                    enemyAnimator.SetBool("HasTarget", false);
                    break;

                case Status.Move:
                    enemyAnimator.SetBool("HasTarget", true);
                    break;

                case Status.Attack:
                    enemyAnimator.SetBool("HasTarget", false);
                    break;

                case Status.Die:
                    enemyAnimator.SetTrigger("Die");
                    enemyCollider.enabled = false;
                    break;



            }
        }
    }

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        agent.enabled = true;
        
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }

        enemyCollider.enabled = true;
        CurrentStatus = Status.Idle;
    }

    private void Update()
    {
        switch (currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;

            case Status.Move:
                UpdateMove();
                break;

            case Status.Attack:
                UpdateAttack();
                break;

            case Status.Die:
                UpdateDie();
                break;
        }
    }

    private  void UpdateIdle()
    {
        if (target != null && Vector3.Distance(target.position, transform.position) <  traceDistance)
        {
            CurrentStatus = Status.Move;
            return;
        }

        target = FindTarget(traceDistance);
    }

    private void UpdateMove()
    {
        if (target != null)
        {
            var find = hitbox.Colliders.Find(x => x.transform == target);
            if (find != null)
            {
                CurrentStatus = Status.Attack;
                return;
            }
        }

        if (target == null || Vector3.Distance(target.position, transform.position) > traceDistance)
        {
            target = null;
            CurrentStatus = Status.Idle;
            return;
        }

        agent.SetDestination(target.position);

    }

    private void UpdateAttack()
    {
        if (target == null)
        {
            CurrentStatus = Status.Move;
            return;
        }

        var find  = hitbox.Colliders.Find(x => x.transform == target);
        if (find == null)
        {
            CurrentStatus = Status.Move;
            return;
        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        if (Time.time > lastAttackTime + attackInterval)
        {
            lastAttackTime = Time.time;

            var LivingEntity = target.GetComponent<LivingEntity>();
            if (LivingEntity != null)
            {
                if (!LivingEntity.isDead)
                {
                    LivingEntity.OnDamage(10f, transform.position, -transform.forward);
                }
            }
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Debug.Log("Enemy OnDamage 호출됨");

        if (isDead)
            return;

        base.OnDamage(damage, hitPoint, hitNormal);

        if (hitEffect == null)
        {
            Debug.LogError("hitEffect가 연결되지 않았습니다.");
            return;
        }

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.forward = hitNormal;
        hitEffect.Play();


    }

    private void UpdateDie()
    {
        if (isDead)
            return;

        CurrentStatus = Status.Die;
        Destroy(gameObject);


    }

    private Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);

        if (colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }
}
