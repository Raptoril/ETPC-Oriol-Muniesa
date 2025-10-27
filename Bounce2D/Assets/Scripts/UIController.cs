using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;    

    public GameObject gameover;
    public GameObject gamewin;
    public GameObject gamepause;
    public GameObject gamemain;
    public GameObject gameplay;
    public GameObject gameovermain;

    public TextMeshProUGUI healths;
    public TextMeshProUGUI checkpoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ActivateGameOver(bool state)
    {
        gameover.SetActive(state);
    }

    public void ActivateGameWin(bool state)
    {
        gamewin.SetActive(state);
    }

    public void ActivateGamePause(bool state)
    {
        gamepause.SetActive(state);
    }

    public void ActivateGamePlay(bool state)
    {
        gameplay.SetActive(state);
    }

    public void ActivateMain(bool state)
    {
        gamemain.SetActive(state);
    }

    public void ActivateOverMain(bool state)
    {
        gameovermain.SetActive(state);
    }

    public void SetHealths(int healthCount)
    {
        healths.text = "X" + healthCount.ToString();
    }
    public void SetCheckpoints(int checkpointCount)
    {
        checkpoints.text = "X" + checkpointCount.ToString();
    }

    public void ContinueGame()
    {
        GameStateManager.instance.ChangeGameState(GameStateManager.GameState.GAMEPLAY);
    }

    public void PlayGame()
    {
        GameStateManager.instance.ChangeGameState(GameStateManager.GameState.GAMEPLAY);

        SceneManager.LoadScene("spmap_level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitLevel()
    {
        GameStateManager.instance.ChangeGameState(GameStateManager.GameState.MAIN);
    }

}
