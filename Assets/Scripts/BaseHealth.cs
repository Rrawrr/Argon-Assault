using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour {

	[SerializeField] int baseMaxHealth = 200;
	[SerializeField] Slider baseHealthSlider;
    [SerializeField] Text finalScoreText;

    public bool isAlive = true;

    private int currentBaseHealth;
	private CameraMover cameraMover;
	private ScoreBoard scoreBoard;
	private PlayerController player;

	void Start ()
	{
        player = FindObjectOfType<PlayerController>();
		scoreBoard = FindObjectOfType<ScoreBoard>();
		cameraMover = FindObjectOfType<CameraMover>();
		baseHealthSlider.maxValue = baseMaxHealth;
		currentBaseHealth = baseMaxHealth;
	}
	

	public void UpdateHealth()
	{
        if (isAlive) //Если база жива
        {
            currentBaseHealth--;
            baseHealthSlider.value = currentBaseHealth;

            if (currentBaseHealth <= 0) //Если прочность базы меньше 0
            {
                isAlive = false;
                finalScoreText.text = "Your base has been destroyed!";
                scoreBoard.ShowFinalScore(); //Вывести таблицу очков
                cameraMover.OnPlayerDeath(); //Остановить движение камеры
                player.isActive = false;
                player.SetGunsActive(false);
                player.GetComponent<AudioSource>().Stop();
            }
        }
	}
}
