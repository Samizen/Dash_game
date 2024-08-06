using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    Player player;
    public float attractionForce = 5f; // Force applied to enemies to attract them

    void Update()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // Calculate the direction from the enemy to the magnet
            Vector3 direction = (transform.position - enemy.transform.position).normalized;

            // Apply a force to the enemy to attract it towards the magnet
            enemy.GetComponent<Rigidbody>().AddForce(direction * attractionForce);
        }
    }

    void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(DestroyMagnetAfterDuration());
    }

    IEnumerator DestroyMagnetAfterDuration()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject); // Destroy the magnet object after the duration
    }
}
