using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {


	public Slider volumeSlider; //Задаем общественные переменные, чтобы можно было привязать слайдер к скрипту в unity
	public Slider difficultySlider;
    public Toggle freeFlightToggle;
	public LevelManager levelManager; // переменная для привязки LevelManager

	private MusicManager musicManager;
    


    void Start () {
		musicManager = Object.FindObjectOfType<MusicManager>(); //При запуске находим MusicManager, так как изначально он находится только в сцене SplashScreen
		Debug.Log(musicManager);

		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();  //Слайдер выставляется на текущее заданное значение
		//difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        freeFlightToggle.isOn = PlayerPrefsManager.GetFreeFlightMode(); //Значение выставляется в зависимости от того хранит FREE_FLIGHT_KEY 0 или 1
        
        
	}
	
	
	void Update () {
		musicManager.ChangeVolume(volumeSlider.value);  //Каждый тик выставляем значение громкости равное уровню слайдера


	}


	public void SaveAndExit(){  //Метод сохранения настроек
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value); //Обращаемся к скрипту PlayerPrefsManager, запускаем метод SetMasterVolume со
		// значением volumeSlider.value равном значению слайдера
		//PlayerPrefsManager.SetDifficuty(difficultySlider.value); // задаем значение сложности, равном уровню слайдера
        PlayerPrefsManager.SetFreeFlightMode(freeFlightToggle.isOn ? 1:0);// Посылаем в скрипт PlayerPrefs значение 1 либо 0
        levelManager.LoadLevel("Menu"); // загружаем уровень Menu
	}

	public void SetDefaults() //Метод выставления настроек по умолчанию
	{
		volumeSlider.value = 0.8f;
		//difficultySlider.value = 2f;
        freeFlightToggle.isOn = false;

    }
}
