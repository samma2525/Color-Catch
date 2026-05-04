using UnityEngine;

public class bulletShooter : MonoBehaviour
{
    [SerializeField]public GameObject bullentPrefab;
    [SerializeField]public Transform shootPoint;
    [SerializeField]public Transform endPoint;
    [SerializeField]public float shootDelay =2f;
    void Start()
    {
        InvokeRepeating("Shoot", 1f, shootDelay);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(
            bullentPrefab,
            shootPoint.position,
            Quaternion.identity
        );
        bullet.GetComponent<bullet>().SetTarget(endPoint.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(shootPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(endPoint.transform.position, 0.5f);
        Gizmos.DrawLine(shootPoint.transform.position, endPoint.transform.position);
    }
}
