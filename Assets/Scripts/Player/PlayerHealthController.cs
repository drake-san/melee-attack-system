using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static float health;

    private float maxHealth = 3;
    private PlayerController playerController;
    private Animator playerAnimator;
    // public UIHealthController uIHealthController;

    [SerializeField]
    private bool isInvincible = false;

    [SerializeField] private float invincibleTime = 5.0f;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth;

        playerController = GetComponent<PlayerController>();
        player = playerController.gameObject;
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isInvincible)
            invincibleTime -= Time.deltaTime;

        if (invincibleTime <= 0)
        {
            isInvincible = false;
            invincibleTime = 5.0f;
            player.GetComponent<CapsuleCollider2D>().isTrigger = false;
            playerAnimator.SetBool("IsInvincible", false);
        }
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0f, maxHealth);
        //uIHealthController.TakeDamage();

        playerAnimator.SetBool("IsInvincible", true);

        if (health <= 0)
            Die();
        else
        {
            isInvincible = true;
            player.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

    public void Die()
    {
        playerController.enabled = false;
        player.GetComponent<CapsuleCollider2D>().isTrigger = true;
        playerAnimator.SetTrigger("Die");
    }
}