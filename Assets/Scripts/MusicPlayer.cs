using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Используется для управления сценами

public class MusicPlayer : MonoBehaviour
{


    private void Awake()
    {
        int numOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; //Находим длинну массива всех MusicPlayer

        if (numOfMusicPlayers > 1) //...если в массиве больше одного...
        {
            Destroy(gameObject);//...уничтожаем этот MusicPlayer
        }
        else
        {
            Object.DontDestroyOnLoad(gameObject); //Не удалять при загрузке новой сцены
        }


    }
}
