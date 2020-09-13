using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;//массив аудиоклипов, из которых будет загружен звук для уровня

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);//Не уничтожать после смены сцены
        Debug.Log("Don't destroy on load "+ name);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();//Переменная обращается к компоненту AudioSource
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();//Выставляем заданный уровень громкости
    }

    private void OnLevelWasLoaded(int level)//При загрузке уровня номер level
    {
        AudioClip thisLevelMusic = levelMusicChangeArray[level];//переменная типа AudioClip содержит значение из массива под номером level 
        Debug.Log("Playing audio: "+levelMusicChangeArray[level]);

        if(thisLevelMusic) //Если есть музыка для этого уровня
        {
            audioSource.clip = thisLevelMusic;//Добавить эту музыку в audioSource
            audioSource.loop = true;//Включить функцию лупа
            audioSource.Play();
        }
    }

    public void ChangeVolume(float volume) //Метод изменения громкости, содержит промежуточную переменную. Теперь если мы пошлем какоето значение  этот метод,
    //он присвоит его этой переменной
    {
        audioSource.volume = volume;    // и з атем выставит соответствующий этому значению уровень громкости
    }
}
