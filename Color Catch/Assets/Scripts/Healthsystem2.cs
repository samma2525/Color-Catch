using UnityEngine;

public class Healthsystem2 : MonoBehaviour
{
    public int health;
    public int maxHealth =5;

    private Player2Dplatformer playerScript;
    private SpriteRenderer playerSr;
    void Start()
    {
        health = maxHealth;
        playerSr = GetComponent<SpriteRenderer>();
        playerScript = GetComponent<Player2Dplatformer>();
    }
    
    public void TakeDamage(int amount)
    {
        health -=amount;
        if (health <= 0){
            playerSr.enabled = false;
            playerScript.enabled = false;
        }
    }
}
