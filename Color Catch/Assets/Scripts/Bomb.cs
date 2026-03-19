using UnityEngine;

public class Bomb : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool collected = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = colorManager.Instance.CurrentColor;
    }

    public void SetColor (Color c)
    {
        if ( sr == null) sr = GetComponent<SpriteRenderer>();
        sr.color = c;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;
        dropletManager.Instance.CollectBomb();

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null) playerHealth.TakeDamage(1);

        if (dropletManager.Instance.colorChaning)
        {
            other.GetComponent<Player>().ApplyColor();
        }
        AudioManager.Instance.PlayBombSFX();
        Destroy(gameObject);
    }
}
