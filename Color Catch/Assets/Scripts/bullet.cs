using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]public float speed = 6f;
    [SerializeField]private AudioClip hitSound;

    private Vector3 target;
    private AudioSource audioSource;
public void SetTarget(Vector3 endPoint)
    {
        target = endPoint;
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(1);
            PlayAndDestroy();
        }
    }

    private void PlayAndDestroy()
    {
        if (hitSound != null & audioSource != null)
        {
            audioSource.transform.SetParent(null);
            audioSource.PlayOneShot(hitSound);
            Destroy(gameObject);
        }
    }
}
