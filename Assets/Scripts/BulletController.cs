using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 bottomLeft;
    private Vector2 topRight;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        bottomLeft = Camera.main.ViewportToWorldPoint(Vector2.zero);
        topRight = Camera.main.ViewportToWorldPoint(Vector2.one);

        Vector3 mousePosition = Mouse.current.position.ReadValue();

        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = (mouseWorldPosition - transform.position).normalized;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Debug.DrawRay(transform.position, direction * 10f, Color.red);

        if (transform.position.x < bottomLeft.x || transform.position.x > topRight.x)
            Destroy(gameObject);
        if (transform.position.y < bottomLeft.y || transform.position.y > topRight.y)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}