using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;

    private Vector3 dashTarget;
    private float dashTime;
    private bool isDashing = false;

    [SerializeField]
    public float _health = 100;

    // For Coin Collection
    public int totalCoinsCollected = 0;

    // PowerUps Variables:
    // Radial Bullets
    [SerializeField]
    private int radialBulletCost = 2;
    

    [SerializeField]
    private GameObject _bulletPrefab;
    public int numberOfBullets = 8; // Number of bullets in the radial pattern
    public float bulletSpeed = 5f;

    // Radial bullet activation
    private bool isRadialBulletActive = false;
    private float radialBulletTimer = 0f;

    // Magnet Powerup
    [SerializeField]
    private int magnetCost = 3;
    [SerializeField]
    private GameObject _magnetPrefab;
    private bool isMagnetActive = false;
    private float magnetDuration = 5f; // Magnet active duration

    // UI Elements
    [SerializeField]
    private TextMeshProUGUI powerupText;

    // Start is called before the first frame update
    void Start()
    {
        powerupText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDashMovement();

        HandlePowerupInput();
        HandleRadialBulletUsage();
        UpdateRadialBulletTimer();

        // Buy and Use PowerUp
        // Buy Radial Bullet with "Q"
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            BuyAndUseRadialBulletPowerup();
        }

        // Buy Magnet with "W"
        if (Input.GetKeyDown(KeyCode.W))
        {
            BuyAndUseMagnetPowerup();
        }

        // Use Magnet PowerUp
        if (isMagnetActive && Input.GetMouseButtonDown(1))
        {
            PlaceMagnet();
        }


    }


    private void BuyAndUseMagnetPowerup()
    {
        if (totalCoinsCollected >= magnetCost)
        {
            totalCoinsCollected -= magnetCost;
            isMagnetActive = true;
        }
        else
        {
            Debug.Log("Not enough coins to buy the Magnet powerup!");
        }
    }
    public void PlaceMagnet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(_magnetPrefab, new Vector3(hit.point.x, 0.5f, hit.point.z), Quaternion.identity);
            isMagnetActive = false; // Disable the magnet powerup after placing it
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); 
        }
    }

    // PowerUps
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            CollectCoin(other);
        }
    }

    private void CollectCoin(Collider other)
    {
        totalCoinsCollected++;
        Destroy(other.gameObject);
    }

    public void HandleDashMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    dashTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z); // Maintain player's initial y-coordinate
                    dashTime = 0f;
                    isDashing = true;
                }
            }
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dashTarget, dashTime / dashDuration);

            if (dashTime >= dashDuration)
            {
                transform.position = dashTarget;
                isDashing = false;
            }
        }
    }

    void HandlePowerupInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BuyRadialBulletPowerup();
        }
    }

    void HandleRadialBulletUsage()
    {
        if (isRadialBulletActive && Input.GetMouseButtonDown(1))
        {
            SpawnRadialBullets(transform.position);
            DeactivateRadialBulletPowerup();
        }
    }

    void UpdateRadialBulletTimer()
    {
        if (isRadialBulletActive)
        {
            radialBulletTimer += Time.deltaTime;
            BlinkPowerupText();

            if (radialBulletTimer >= 3f)
            {
                DeactivateRadialBulletPowerup();
            }
        }
    }

    public void BuyRadialBulletPowerup()
    {
        if (totalCoinsCollected >= radialBulletCost)
        {
            totalCoinsCollected -= radialBulletCost;
            ActivateRadialBulletPowerup();
        }
        else
        {
            Debug.Log("Not enough coins to buy the powerup!");
        }
    }

    public void ActivateRadialBulletPowerup()
    {
        isRadialBulletActive = true;
        radialBulletTimer = 0f;
        powerupText.enabled = true;
    }

    public void DeactivateRadialBulletPowerup()
    {
        isRadialBulletActive = false;
        powerupText.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void BuyAndUseRadialBulletPowerup()
    {
        if (totalCoinsCollected >= radialBulletCost)
        {
            totalCoinsCollected -= radialBulletCost;
            SpawnRadialBullets(transform.position);
        }
        else
        {
            Debug.Log("Not enough coins to buy the powerup!");
        }
    }

    public void SpawnRadialBullets(Vector3 spawnPosition)
    {
        float angleStep = 360f / numberOfBullets;
        float angle = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the direction for this bullet
            float bulletDirXPosition = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirZPosition = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletDirection = new Vector3(bulletDirXPosition, 0, bulletDirZPosition).normalized;

            // Instantiate the bullet
            GameObject tempBullet = Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);

            // Set the bullet's velocity
            tempBullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;

            // Increase the angle for the next bullet
            angle += angleStep;
        }
    }

    private void BlinkPowerupText()
    {
        if (isRadialBulletActive)
        {
            powerupText.text = "Radial Bullets Active!";
            powerupText.alpha = Mathf.PingPong(Time.time * 2, 1);
        }
        else if (isMagnetActive)
        {
            powerupText.text = "Magnet Active!";
            powerupText.alpha = Mathf.PingPong(Time.time * 2, 1);
        }
        else
        {
            powerupText.text = ""; // Clear text if no powerup is active
        }
    }

    IEnumerator MagnetTimer()
    {
        yield return new WaitForSeconds(magnetDuration);
        isMagnetActive = false; // Deactivate the magnet powerup after the duration
    }

}
