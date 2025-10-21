using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;

    public float speed = 5f;
    private Transform _currentWaypoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentWaypoint = waypoint1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (_currentWaypoint.position - transform.position);
        float dist = dir.magnitude;

        if(dist < 0.01f)
        {
            _currentWaypoint = _currentWaypoint == waypoint1 ? waypoint2 : waypoint1;
        }

        transform.position = Vector2.MoveTowards(transform.position, _currentWaypoint.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El player ha muerto");
            Time.timeScale = 0f;
        }
    }
}
