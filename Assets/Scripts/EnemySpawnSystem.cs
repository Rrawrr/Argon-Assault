using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour {

    [SerializeField] GameObject[] spawnPoints; //Массив всех объектов для респауна
    public float spawnTimer;

    private float currentTimer;
    private GameObject currentSpawnPoint;//Выбранный объект (волна врагов) для респауна



    private void Awake()
    {
        if (PlayerPrefsManager.GetFreeFlightMode()) //Если выставлена галочка свободного полета, отключаем этот объект
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
       
        spawnTimer = 19f;
        Invoke("SpawnEnemyWave", 5f); //Вызывает первую волну врагов
        currentTimer = spawnTimer;
        
    }

    private void Update()
    {
        ProcessSpawning();
    }

    private void ProcessSpawning() //Метод отсчета до респауна новой волны
    {
        currentTimer -= Time.deltaTime;
        print("current spawn timer is: " + currentTimer);
        if (currentTimer <= 0)
        {
            SpawnEnemyWave();
            currentTimer = spawnTimer;
        }
    }
    private void SpawnEnemyWave()//Метод респауна
    {
        currentSpawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length-1)];//Выбираем случайный объект из массива
        Instantiate(currentSpawnPoint, currentSpawnPoint.transform.position, currentSpawnPoint.transform.rotation);//Респауним его с позицией и вращением его префаба
    }

}
