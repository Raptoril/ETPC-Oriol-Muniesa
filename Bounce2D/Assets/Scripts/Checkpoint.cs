using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            current = this;
            gameObject.SetActive(false);

            PlayerController ctr = collision.gameObject.GetComponent<PlayerController>();
            ctr.SetCheckpoint(this);

            // I wanna save player data
            //PlayerPrefs, is in charge of saving data (serializing)
        }
    }
}
