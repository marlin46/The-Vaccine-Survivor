using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;
    public TextMeshProUGUI scoreText;
    int score = 0;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
