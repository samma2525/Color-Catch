using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 5;
    private Player playerScript;
    private SpriteRenderer playerSr;
    void Start()
    {
        health = maxHealth;
        playerSr = GetComponent<SpriteRenderer>();
        playerScript = GetComponent<Player>();
    }

    public void TakeDamage (int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
