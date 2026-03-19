using UnityEngine;
using System.Collections.Generic;

public class UIHealthController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthObject;

    private float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = PlayerHealthController.health;

        for (int i = 0; i < health; i++)
        {
            Instantiate(healthObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TakeDamage()
    {
        if (health > 0)
        {
            Destroy(gameObject);
        }
    }
}