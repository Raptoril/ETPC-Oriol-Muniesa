using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float acceleration = 10f;
    public float jumpForce = 12f;
    public float friction = 1f;

    public float rayLength = 1f;
    public LayerMask rayMask;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity = Vector2.zero;
    private float _input;
    private bool _grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _input = UnityEngine.Input.GetAxisRaw("Horizontal");

        _grounded = Physics2D.Raycast(transform.position, Vector2.down, rayLength, rayMask);

        if (UnityEngine.Input.GetButtonDown("Jump") && _grounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _velocity = _rigidbody.linearVelocity;

        if(_input != 0)
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, _input * speed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, friction * Time.fixedDeltaTime);
        }

        _rigidbody.linearVelocity = _velocity;
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.relativeVelocity.y > -5f)
    //    {
    //        _rigidbody.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
    //    }
    //}
}
