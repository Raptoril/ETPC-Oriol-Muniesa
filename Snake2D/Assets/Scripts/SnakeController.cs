using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    public float speed = 5;
    public Vector2 direction = Vector2.up;

    private float _timer = 0;
    public float delayTime = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame) direction = Vector2.up;
        if (Keyboard.current.sKey.wasPressedThisFrame) direction = Vector2.down;
        if (Keyboard.current.dKey.wasPressedThisFrame) direction = Vector2.right;
        if (Keyboard.current.aKey.wasPressedThisFrame) direction = Vector2.left;

        _timer += Time.deltaTime;

        if (_timer > delayTime )
        {
            _timer = 0;
            Move();
        }
    }

    private void Move()
    {
        transform.position = new Vector3(
            transform.position.x + direction.x,
            transform.position.y + direction.y,
            0.0f
            );
    }
}

