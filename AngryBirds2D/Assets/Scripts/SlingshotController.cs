using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public static SlingshotController instance;

    [SerializeField] private BirdController _currentBird;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _force = 350f;
    [SerializeField] private float _maxDistance = 3f;

    private Camera _camera;
    private bool _isDragging;
    private Vector2 _startOrigin;
    private Vector2 _direction;
    private float _distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        _startOrigin = new Vector2(_startPosition.position.x, _startPosition.position.y);

        instance = this;
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

            Shot();
        }

        OnDrag();
    }

    public void OnDrag()
    {
        if (_isDragging)
        {
            Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            _direction = position - _startOrigin;

            if (_direction.magnitude > _maxDistance)
            {
                position = _startOrigin + _direction.normalized * _maxDistance;
                _distance = Mathf.Clamp(Vector2.Distance(position, _startOrigin),0, _maxDistance);
            }
            
            _currentBird.transform.position = position;
        }
    }

    public void Shot()
    {
        _currentBird.Rbody.bodyType = RigidbodyType2D.Dynamic;

        float forceImpulse = _distance / _maxDistance;
        Vector2 direction = _startPosition.position - _currentBird.transform.position;
        _currentBird.Rbody.AddForce(direction.normalized * _force * forceImpulse);

        Invoke(nameof(ActivateBird), 0.1f);
    }
    public void Reload()
    {
        // We have to reload the new bird from the ammocontroller
        _currentBird = AmmoController.instance.Reload();

        if (_currentBird != null)
        {
            _currentBird.transform.position = _startPosition.position;

            // We need to move the camera to its original position
            CameraController.instance.ResetCamera();
        }
        else
        {
            // The game is over.
        }
    }

    public void ActivateBird()
    {
        BirdController bird = _currentBird.GetComponent<BirdController>();
        bird.SetBirdActive(true);
    }

    public Transform GetCurrentBird()
    {
        return _currentBird.transform;
    }
}
