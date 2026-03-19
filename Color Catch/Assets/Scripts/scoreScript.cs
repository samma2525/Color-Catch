using UnityEngine;
using TMPro;

public class scoreScript : MonoBehaviour
{
    public static scoreScript Instance;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    void Awake()
    {
        Instance = this;
    }
    public void AddScore (int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
