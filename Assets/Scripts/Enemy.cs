using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] int scoresPerHit = 12;
    [SerializeField] int hits = 5;
    [SerializeField] GameObject[] guns;

    ScoreBoard scoreBoard;
    Transform parent;


    private void Start()
    {
        ProccessShoot(false);
        AddNonTriggerBoxCollider();// метод создает коллайдер у этого объекта
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider nonTriggerBoxCollider = gameObject.AddComponent<BoxCollider>();
        nonTriggerBoxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Player"))
        ProcessHit(); 
    }

    public void ProcessHit()
    {
        scoreBoard.ScoreHit(scoresPerHit);
        hits--;
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);//создаем взрыв как объект
        fx.transform.parent = parent; //делаем его дочерним для объект parent, чтобы все взрывы сохранялись в одном месте в инспекторе
        Destroy(gameObject);
    }

    public void ProccessShoot(bool isActive) //Метод стрельбы
    {
      foreach (GameObject gun in guns) //Перебираем все имеющиеся в массиве орудия...
      {
        var emission = gun.GetComponent<ParticleSystem>().emission;//Обращаемся к их компоненту
            emission.enabled = isActive;//...и переключаем его в необходимое состояние 
      }

    }

    public void ShootTheBase()
    {
        ProccessShoot(true);
    }

    public void StopShooting()
    {

        ProccessShoot(false);
    }

}
