using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    public Transform respawnPoint;
    public GameObject player;

    private bool isDead;
    public GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0 || player.transform.position.y < -5 && !isDead) // Check if player falls below a certain height (e.g., -10)
        {
            isDead = true;
            gameManager.gameOver();
            Debug.Log("Dead");
        }

    }

    void RespawnPlayer()
    {
        player.transform.position = respawnPoint.position;
        health = maxHealth;
    }
}