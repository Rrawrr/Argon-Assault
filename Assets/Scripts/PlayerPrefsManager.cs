using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{


    const string MASTER_VOLUME_KEY = "master_volume";   // Этот файл сохраняет настройки игрока, такие как например уровень громкости или сложности
    const string DIFFICULTY_KEY = "difficulty";         //Все переменные заданы статичными, чтобы к ним можно было иметь доступ отовсюду
    const string LEVEL_KEY = "level_unlocked_";         //В классе PlayerPrefs доступны переменные только трех типов int, float, string (нет bool и прочих)
    const string FREE_FLIGHT_KEY = "free_flight_mode";
   


    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)   //если значение громкости задано между 0 и 1
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);    //то выставляем предпочтительное значение
        }
        else
        {
            Debug.LogError("Master volume out of range 0-1");   //иначе выдаем ошибку, вне диапазона
        }
    }


    public static float GetMasterVolume()//Метод получения уровня громкости
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);//Возвращает текущий уровень громкости
    }


    public static void UnlockLevel(int level)//Метод разблокировки уровня, задает конкретному уровню значение 1, чтобы разблокировать
    {
        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);    //Используем 1 для значения true
        }

        else
        {
            Debug.LogError("Trying to unlock level not in build order");
        }
    }

    public static bool IsLevelUnlocked(int level)   //Метод проверки разблокирован ли уровень
    {
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());  //Переменная которая обращается к настройкам PlayerPrefs и берет значение для true/false
        bool isLevelUnlocked = (levelValue == 1);

        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            return isLevelUnlocked;
        }
        else
        {
            Debug.LogError("Level index not in build order");
            return false;
        }
        
    }

    public static void SetDifficuty(float difficulty)   //Метод выставления сложности
    {
        if (difficulty >= 1 && difficulty <= 3) //если значение difficulty задано между 0 и 1, то
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);  //...выставляем значение сложности
        else
            Debug.LogError("Difficulty out of range 0-1");
    }

    public static float GetDifficulty() //Метод возвращает значение сложности
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }

    

    public static void SetFreeFlightMode(int freeValue) // Метод Вкл/выкл режим свободного полета
    {
        PlayerPrefs.SetInt(FREE_FLIGHT_KEY, freeValue); //Устанавливаем значение ключа FREE_FLIGHT_KEY на то которое ему послали (int)
    }

    public static bool GetFreeFlightMode() //Метод возвращает true или false исходя из того, хранит FREE_FLIGHT_KEY 1 или 0
    {
        int freeValue = PlayerPrefs.GetInt(FREE_FLIGHT_KEY); //Получаем значение FREE_FLIGHT_KEY
        return freeValue == 1; //возвращаем соответ. bool
    }
}
        
 
