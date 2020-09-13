using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// используется для переключения сцен

public class LevelManager : MonoBehaviour
{

    public float autoLoadNextLevelAfter;

    void Start()
    {

        if (autoLoadNextLevelAfter <= 0)
        {
            Debug.Log("Auto Level loading disabeled, use positive number of seconds");
        }
        else
        {
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);//Запустить метод LoadNextLevel через время autoLoadNextLevelAfter  
        }

    }

    public void LoadLevel (string name)
    {
        	
        Debug.Log("Level load requested for: "+name);
        SceneManager.LoadScene(name);//Загружает сцену по имени
        
    }

    public void Quit() //добавил публичную функцию в класс LevelManager
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Загружает слудующий по списку уровень 
    }


}
