using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 1f;
    //[SerializeField] GameObject deathFX; //Звук и партиклы при столкновении
    // Тк кроме particle system есть еще звук, то для активации всего вместе используем SetActive а не просто функцию .Play (так было по курсу)

    ScoreBoard scoreBoard;
    BaseHealth baseHealth;

    private void Start()
    {
        baseHealth = FindObjectOfType<BaseHealth>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }


    private void OnTriggerEnter(Collider other)// При столкновении
    { 
        if (other.gameObject.GetComponent<Enemy>()) //Если у другого объекта найден скрипт Enemy
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.KillEnemy();//Запускаем в нем метод KillEnemy
            scoreBoard.ScoreHit(50);//...и прибавляем 50 очков за таран врага
        }
        else
            StartDeathSequence(); //Иначе, запускается метод умирания
    }

    private void OnParticleCollision(GameObject other)
    {
      StartDeathSequence();
      print("Player got some shots");
    }


    private void StartDeathSequence()
    {
        gameObject.SendMessage("OnPlayerDeath");// С помощью SendMessage этот метод запускается во всех скриптах этого объекта
        gameObject.SendMessageUpwards("OnPlayerDeath");// Посылает сообщение родительскому объекту - камере
        Invoke("ShowFinalScore", levelLoadDelay);
        baseHealth.isAlive = false; //Отключаем базу, для избежания багов
        //Invoke("LoadFirstLevel", levelLoadDelay
    }


    void ShowFinalScore()
    {
        scoreBoard.ShowFinalScore();
    }

    //void LoadFirstLevel()
    //{
    //    int currentScene = SceneManager.GetActiveScene().buildIndex;
    //    SceneManager.LoadScene(currentScene);
    //}

}
