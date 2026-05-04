using UnityEngine;

public class Player2Dplatformer : MonoBehaviour
{
    public float moveSpeed  =4f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    
    public Sprite jumpSprite;
    public Sprite fallSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x , jumpForce);
        }
        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if(Mathf.Abs(moveInput) < 0.01f)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                anim.Play("idle");
            }
            }
            else
            {
                if (moveInput > 0)
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("rightWalk"))
            {
                anim.Play("rightWalk");
            }
                }
                if(moveInput < 0)
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("leftWalk"))
            {
                anim.Play("leftWalk");
            }
                }
            }
        }
    }
}
