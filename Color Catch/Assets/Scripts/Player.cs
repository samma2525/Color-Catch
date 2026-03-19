using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Animator anim;

    public dropletManager dM;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float x;
    private float y;
    private bool moving;
    private Vector2 inputPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputPlayer * moveSpeed;
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        inputPlayer = new Vector2(x, y).normalized;
    }

    private void Animate()
    {
        moving = inputPlayer.magnitude > 0.1f;

        if (moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }

        anim.SetBool("Moving", moving);
    }

    public void ApplyColor()
    {
        sr.color = PlayerColorManager.Instance.CurrentColor;
    }
}