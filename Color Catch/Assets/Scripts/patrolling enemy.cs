using UnityEngine;

public class patrollingenemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    public int speed = 5;

    public bool vertical = false;
    public bool horizontal = true;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
    }

    void Update()
    {
        if (vertical) MovingOnY();
        if(horizontal) MovingOnX();
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void MovingOnX()
    {
        vertical = false;
        Vector2 point= currentPoint.position - transform.position;
        if ( currentPoint == pointB.transform) 
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else 
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    private void MovingOnY()
    {
        horizontal = false;
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(0, speed);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, -speed);
        }

        if(Vector2.Distance(transform.position, currentPoint.position)< 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
}
