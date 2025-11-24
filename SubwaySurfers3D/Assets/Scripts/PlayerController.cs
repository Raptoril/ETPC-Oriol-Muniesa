using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float verticalSpeed = 20f;
    public float laneSwapSpeed = 5f;
    public float laneDistance = 4f;

    // Lane change
    public int currentLane = 1;

    private Vector3 targetPosition;
    private CharacterController _charCtr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _charCtr = GetComponent<CharacterController>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLane(-1);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            MoveLane(1);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 forwardMove = Vector3.forward * forwardSpeed * Time.fixedDeltaTime;
        Vector3 verticalMove = Vector3.down * 0.5f;
        Vector3 horizontalMove = Vector3.MoveTowards(_charCtr.transform.position, targetPosition, laneSwapSpeed * Time.fixedDeltaTime);
        horizontalMove = new Vector3(horizontalMove.x - transform.position.x, 0, 0);
        
        Debug.Log(_charCtr.transform.position + " // " + targetPosition);
        _charCtr.Move(forwardMove + horizontalMove + verticalMove);
    }

    private void MoveLane(int direction)
    {
        int newLane = currentLane + direction;
        newLane = Mathf.Clamp(newLane, 0, 2);

        if(currentLane != newLane)
        {
            currentLane = newLane;
            float newx = (currentLane -1) * laneDistance;
            targetPosition = new Vector3(newx, transform.position.y, transform.position.z);
        }
    }

    public void Jump()
    {

    }

    public void Slide()
    {

    }
}
