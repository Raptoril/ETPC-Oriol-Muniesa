using UnityEngine;

public class BirdController : MonoBehaviour
{
    private bool _isActive = false;
    [HideInInspector] public Rigidbody2D Rbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_isActive)
        {
            if(Rbody.linearVelocity.magnitude < 0.1f)
            {
                Debug.Log("El pajaro esta quieto");
                _isActive = false;

                SlingshotController.instance.Reload();
            }
        }
    }
    public void SetBirdActive(bool activate)
    {
        _isActive = activate;
    }
}
