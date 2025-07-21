using UnityEngine;

public class FirtzController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float tricycleSpeed = 2f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isTricycle = false;

    private Vector3 normalScale;
    private Vector3 tricycleScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;
        tricycleScale = new Vector3(normalScale.x, normalScale.y * 0.5f, normalScale.z); // half-height when tricycling
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = isTricycle ? tricycleSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Toggle tricycle mode
        if (Input.GetKeyDown(KeyCode.T))
        {
            isTricycle = !isTricycle;
            transform.localScale = isTricycle ? tricycleScale : normalScale;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Check if we landed on something solid
        if (col.contacts.Length > 0 && col.contacts[0].normal.y > 0.5f)
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
}
