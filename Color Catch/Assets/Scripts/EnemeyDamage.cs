using UnityEngine;

public class EnemeyDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D (Collider2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
    
}
