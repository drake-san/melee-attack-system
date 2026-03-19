using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 distance;

    private float speed;

    private PlayerHealthController playerHealthController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameManager = GameManager.instance;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerHealthController = player.gameObject.GetComponent<PlayerHealthController>();

        speed = gameManager.enemySpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            distance = (player.position - transform.position).normalized;

            GetComponent<SpriteRenderer>().flipX = distance.x < 0;

            Debug.DrawLine(transform.position, player.position);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = speed * distance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.gameObject.GetComponent<PlayerController>().isAttacking)
            {
                playerHealthController.TakeDamage(1);
            }
            else
                speed = 0;
        }
    }

    private void DestroySelf() => Destroy(gameObject);
}