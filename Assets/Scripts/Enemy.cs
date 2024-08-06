using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float moveSpeed = 5f; // Speed at which the enemy moves towards the player

    private Rigidbody rb;

    //[SerializeField]
    //private float _health = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Ensure Rigidbody is not affected by rotation
                rb.freezeRotation = true;
            }
            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Touched Bullet");
            Destroy(gameObject);
        }
    }
}
