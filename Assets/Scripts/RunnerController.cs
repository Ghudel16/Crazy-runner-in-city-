using UnityEngine;

public class RunnerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 8f;
    public float maxSpeed = 24f;
    public float acceleration = 0.35f;
    public float steerSpeed = 7f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = rb.linearVelocity;
        velocity.z = Mathf.MoveTowards(velocity.z, maxSpeed, acceleration * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x += horizontal * steerSpeed * Time.deltaTime;
        transform.position = pos;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            grounded = false;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
                transform.position += new Vector3(touch.deltaPosition.x * 0.015f, 0, 0);
        }

        velocity.z = Mathf.Clamp(Mathf.Max(velocity.z, forwardSpeed), 0, maxSpeed);
        rb.linearVelocity = velocity;
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (var c in collision.contacts)
            if (c.normal.y > 0.5f) grounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
