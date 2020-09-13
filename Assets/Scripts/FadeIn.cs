using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {


    public float fadeInTime;

    private Image fadePanel;    //Переменная типа Image
    private Color currentColor = Color.black;

	// Use this for initialization
	void Start () {
        fadePanel = GetComponent<Image>();  //Выбираем тот самый необходимый компонент Image, alfa которого мы будем менять
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Time.timeSinceLevelLoad < fadeInTime)    //Выполняем цикл, пока время загрузки сцены меньше времени FadeIn
        {
            float alfaChange = Time.deltaTime / fadeInTime; //Промежуточная переменная, которая определяет скорость уменьшения фэйда
            currentColor.a -= alfaChange;   //Декремент альфа на заданную скорость
            fadePanel.color = currentColor; // ! Выставляем цвет панели как текущий цвет (будет происходить постоянно в течении цикла)

        } else{
            gameObject.SetActive(false);//После прошествия времени FadeIn отключаем Panel
        }
	}
}
