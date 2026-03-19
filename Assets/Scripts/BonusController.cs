using System;
using Unity.VisualScripting;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            playerController.ammoNumber += 25;
        }
    }
}