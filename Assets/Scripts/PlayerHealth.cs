using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public UIManager uiManager;

    private float maxHealth = 100f;

    private PlayerMovement playerMovement;
    private Animator playerAnimator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();

        if (uiManager != null)
        {
            uiManager.UpdateHealthSlider(Health, startingHealth);
        }
        else
        {
            Debug.Log("uimanager가 연결안됨");
        }

        playerMovement.enabled = true;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (isDead)
            return;

        base.OnDamage(damage, hitPoint, hitNormal);

        Debug.Log("현재 체력: " + Health);

        if (uiManager != null)
        {
            uiManager.UpdateHealthSlider(Health, startingHealth);
        }

        if (isDead)
        {
            Die();
        }

    }

    private void Die()
    {
        Debug.Log("플레이어 사망");


        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Die");
        }
    }
}
