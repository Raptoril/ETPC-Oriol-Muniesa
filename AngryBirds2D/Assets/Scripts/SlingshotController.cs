using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    [SerializeField] private Transform _currentBird;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _force = 350f;
    [SerializeField] private float _maxDistance = 3f;

    private Rigidbody2D _birdRb;
    private Camera _camera;
    private bool _isDragging;
    private Vector2 _startOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _birdRb = _currentBird.GetComponent<Rigidbody2D>();
        _birdRb.bodyType = RigidbodyType2D.Kinematic;

        _camera = Camera.main;
        _startOrigin = new Vector2(_startPosition.position.x, _startPosition.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = false;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(Physics2D.Raycast(ray.origin, ray.direction))
            {
                _isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;

            OnShot();
        }

        OnDrag();
    }

    public void OnDrag()
    {
        if (_isDragging)
        {
            Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = position - _startOrigin;

            if (direction.magnitude > _maxDistance)
            {
                position = _startOrigin + direction.normalized * _maxDistance;
            }
            
            _currentBird.position = position;
        }
    }

    public void OnShot()
    {
        _birdRb.bodyType = RigidbodyType2D.Dynamic;

        Vector2 direction = _startPosition.position - _currentBird.position;
        _birdRb.AddForce(direction.normalized * _force);
    }
}
