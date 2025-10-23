using UnityEngine;

public class EndLevel : MonoBehaviour
{
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
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0.0f;
            Debug.Log("El nivel se ha terminado");

            GameStateManager.instance.ChangeGameState(GameStateManager.GameState.WIN);

            // Load the next level
        }
    }
}
