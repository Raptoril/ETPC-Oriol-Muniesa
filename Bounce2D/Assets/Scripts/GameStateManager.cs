using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public enum GameState
    {
        WIN,
        OVER,
        OVERMAIN,
        MAIN,
        PAUSE,
        GAMEPLAY
    }

    public GameState currentState;
    private void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.PAUSE)
            {
                ChangeGameState(GameState.GAMEPLAY);
            }
            else
            {
                ChangeGameState(GameState.PAUSE);
            }
        }
    }

    public void ChangeGameState(GameState state)
    {
        Debug.Log("CHANGE STATE" + state);

        switch (state)
        {
            case GameState.MAIN:
                {
                    UIController.instance.ActivateGamePlay(false);
                    UIController.instance.ActivateGameOver(false);
                    UIController.instance.ActivateGameWin(false);
                    UIController.instance.ActivateGamePause(false);
                    UIController.instance.ActivateMain(true);
                    UIController.instance.ActivateOverMain(false);

                    SceneManager.LoadScene("spmap_mainmenu");
                    Time.timeScale = 1f;

                    currentState = GameState.MAIN;
                }
                break;
            case GameState.OVER:
                {
                    UIController.instance.ActivateGamePlay(false);
                    UIController.instance.ActivateGameOver(true);
                    UIController.instance.ActivateGameWin(false);
                    UIController.instance.ActivateGamePause(false);
                    UIController.instance.ActivateMain(false);
                    UIController.instance.ActivateOverMain(false);
                    StartCoroutine(Respawn());
                    Time.timeScale = 0f;

                    currentState = GameState.OVER;
                    Debug.Log("HE MUERTO");
                }
                break;
            case GameState.WIN:
                {
                    UIController.instance.ActivateGamePlay(false);
                    UIController.instance.ActivateGameOver(false);
                    UIController.instance.ActivateGameWin(true);
                    UIController.instance.ActivateGamePause(false);
                    UIController.instance.ActivateMain(false);
                    UIController.instance.ActivateOverMain(false);
                    Time.timeScale = 0f;
                    StartCoroutine(RestartGame());

                    currentState = GameState.WIN;
                }
                break;
            case GameState.PAUSE:
                {
                    UIController.instance.ActivateGamePlay(false);
                    UIController.instance.ActivateGameOver(false);
                    UIController.instance.ActivateGameWin(false);
                    UIController.instance.ActivateGamePause(true);
                    UIController.instance.ActivateMain(false);
                    UIController.instance.ActivateOverMain(false);
                    Time.timeScale = 0f;

                    currentState = GameState.PAUSE;
                }
                break;
            case GameState.GAMEPLAY:
                {
                    UIController.instance.ActivateGamePlay(true);
                    UIController.instance.ActivateGameOver(false);
                    UIController.instance.ActivateGameWin(false);
                    UIController.instance.ActivateGamePause(false);
                    UIController.instance.ActivateMain(false);
                    UIController.instance.ActivateOverMain(false);
                    Time.timeScale = 1f;

                    currentState = GameState.GAMEPLAY;
                }
                break;
            case GameState.OVERMAIN:
                {
                    UIController.instance.ActivateGamePlay(false);
                    UIController.instance.ActivateGameOver(false);
                    UIController.instance.ActivateGameWin(false);
                    UIController.instance.ActivateGamePause(false);
                    UIController.instance.ActivateMain(false);
                    UIController.instance.ActivateOverMain(true);
                    Time.timeScale = 0f;

                    StartCoroutine(RestartGame());
                    currentState = GameState.OVERMAIN;
                }
                break;
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(2f);

        Debug.Log("RESPAWNING");

        PlayerController pctr = FindAnyObjectByType<PlayerController>();
        pctr.Respawn();

        ChangeGameState(GameState.GAMEPLAY);
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);

        ChangeGameState(GameState.MAIN);
    }
}
