using System;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    protected bool isActive = false;
    public float trailDelay = 0.3f;
    public Transform trailSprite;

    [HideInInspector] public Rigidbody2D Rbody;

    private float _timeSpan = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Initialize()
    {
        Rbody = GetComponent<Rigidbody2D>();
    }

    protected void DetectAlive()
    {
        if (Rbody.linearVelocity.magnitude < 0.4f)
        {
            isActive = false;

            SlingshotController.instance.Reload();
        }
    }

    public void SetBirdActive(bool activate)
    {
        isActive = activate;
    }

    public void DrawTrace()
    {
        if (isActive)
        {
            _timeSpan = _timeSpan + Time.deltaTime;

            if(_timeSpan > 0.03)
            {
                Transform trail = Instantiate(trailSprite, transform.position, Quaternion.identity);
                trail.localScale = UnityEngine.Random.Range(0.5f, 1.2f) * Vector3.one;
                _timeSpan = 0f;
            }
        }
    }
}
