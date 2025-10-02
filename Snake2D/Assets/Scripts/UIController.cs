using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public TextMeshProUGUI points;
    public TextMeshProUGUI gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        gameOver.enabled = false;
    }

    public void DisplayGameOver()
    {
        gameOver.enabled = true;
    }

    public void DisplayScore(int score)
    {
        points.text = score.ToString();
    }
}
