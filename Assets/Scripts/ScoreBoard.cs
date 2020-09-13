using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    [SerializeField] int changeDifficulty1 = 500;
    [SerializeField] int changeDifficulty2 = 1500;
    [SerializeField] int changeDifficulty3 = 2500;
    [SerializeField] Text finalScoreText;
    [SerializeField] GameObject finalImage;

    int score;
    Text scoreText;
    EnemySpawnSystem spawnSystem;


    

    void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
        spawnSystem = FindObjectOfType<EnemySpawnSystem>();
    }


    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
        scoreText.text = score.ToString();
        if (score >= changeDifficulty1) 
        {
            IncreaseDifficulty(15f);
        }

        if (score >= changeDifficulty2)
        {
            IncreaseDifficulty(11f);
        }
        if (score >= changeDifficulty3)
        {
            IncreaseDifficulty(9f);
        }

    }

    private void IncreaseDifficulty(float spawnTimer)
    {
        print("difficulty shoud be increased");
        spawnSystem.spawnTimer = spawnTimer;// враги спаунятся быстрее
    }

    public void ShowFinalScore()
    {
        finalImage.SetActive(true);
        finalScoreText.text = score.ToString();
        spawnSystem.gameObject.SetActive(false);
    }
}
