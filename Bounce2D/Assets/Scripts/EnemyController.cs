using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;

    public float tiempo = 5f;
    private Transform _currentWaypoint;
    private float _speedProp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentWaypoint = waypoint1;

        float dist = Vector3.Distance(waypoint1.position, waypoint2.position);
        _speedProp = dist / tiempo;

        // Velocidad = distance / tiempo

        Debug.Log(_speedProp, this.gameObject);
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

        transform.position = Vector2.MoveTowards(transform.position, _currentWaypoint.position, _speedProp * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El player ha muerto");
            Time.timeScale = 0f;

            PlayerController ctr = collision.gameObject.GetComponent<PlayerController>();
            ctr.Kill();
        }
    }
}
