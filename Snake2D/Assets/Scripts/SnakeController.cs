using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    public GameObject body;
    public float speed = 5;
    public Vector2 direction = Vector2.up;

    private float _timer = 0;
    public float delayTime = 0.2f;

    public List<GameObject> bodyParts = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame && direction != Vector2.down) direction = Vector2.up;
        if (Keyboard.current.sKey.wasPressedThisFrame && direction != Vector2.up) direction = Vector2.down;
        if (Keyboard.current.dKey.wasPressedThisFrame && direction != Vector2.left) direction = Vector2.right;
        if (Keyboard.current.aKey.wasPressedThisFrame && direction != Vector2.right) direction = Vector2.left;

        _timer += Time.deltaTime;

        if (_timer > delayTime )
        {
            _timer = 0;
            Move();
        }
    }

    public void Grow()
    {
        GameObject bodyInstance = Instantiate(body);

        if (bodyParts.Count > 0)
        {
            bodyInstance.transform.position = bodyParts[bodyParts.Count - 1].transform.position;
        }
        else
        {
            bodyInstance.transform.position = transform.position;
        }

        // Get the collider from the body, and disable it for a few frames
        {
            BoxCollider2D box = bodyInstance.GetComponent<BoxCollider2D>();
            box.enabled = false;
            StartCoroutine(EnableCollider(box));
        }

        bodyParts.Add(bodyInstance);

        UIController.Instance.DisplayScore(bodyParts.Count);
    }

    IEnumerator EnableCollider(BoxCollider2D box)
    {
        yield return new WaitForSeconds(0.5f);
        box.enabled = true;
    }

    private void Move()
    {
        Vector3 prev = transform.position;
        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0.0f
            );

        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 part = bodyParts[i].transform.position;
            bodyParts[i].transform.position = prev;
            prev = part;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("SnakeWall") || col.collider.CompareTag("SnakeBody"))
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;

            UIController.Instance.DisplayGameOver();
        }
    }
}