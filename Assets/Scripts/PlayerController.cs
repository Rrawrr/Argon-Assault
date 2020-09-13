using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    [Header("General")]
    [Tooltip("In meters per second")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("From the center")] [SerializeField] float xRange = 4f;
    [Tooltip("From the center")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;
    [SerializeField] GameObject explosion;

    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -5f; //Коэффициент наклона
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    public bool isActive = true;

    float yThrow, xThrow;
    float zThrow; //Используется для только второго джойстика (для режима пике)
    float zRollFactor = 20f;
    
    CameraMover cameraMover;
    AudioSource audioSource;

    void Start()
    {
        cameraMover = FindObjectOfType<CameraMover>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isActive)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
            ProcessNosedown(); //Метод пике корабля
        }
    }

   private void OnPlayerDeath() //Вызывается по имени через ссылку SendMessage
    {
        isActive = false;
        GameObject fx = Instantiate(explosion, transform.position, Quaternion.identity);//Cоздаем взрыв как объект
        gameObject.SetActive(false);//Выключаем игрока
    }

   private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;// Наклон для того чтобы не смотреть всегда только в центр
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
         xThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //Отклонение джойстика по оси х
         yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime; //Отступ игрока при движении джойстика по х = отклонение джойстика * скорость * абсолютное время
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset; //Новая текущая позиция x...
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); //...c ограничением чтобы не вылетать за экран
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); //Задаем позицию игрока как новую переменную типа Vector3

        if (rawYPos > yRange)
          MoveCamera("up"); 
        if (rawYPos < -yRange)
          MoveCamera("down");
        if (rawXPos > xRange)
          MoveCamera("left");
        if (rawXPos < -xRange)
         MoveCamera("right");
    }

   private void ProcessFiring() //Метод стрельбы
    {
        if (CrossPlatformInputManager.GetAxis("Fire") < 0 || CrossPlatformInputManager.GetButton("Fire")) // Если положение курка на правом джойстике отличное от 0 , или нажата кнопка стрельбы
        {
            SetGunsActive(true);//..включить стрельбу
            if (!audioSource.isPlaying)// Если звук стрельбы еще не проигрывается..
        }
        else
        {
            SetGunsActive(false);
            if (audioSource.isPlaying)
                audioSource.Stop();
        } 
    }

    public void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns) //Перебираем все имеющиеся в массиве орудия...
        {
            var emission = gun.GetComponent<ParticleSystem>().emission;//Обращаемся к их компоненту
            emission.enabled = isActive;//...и переключаем его в необходимое состояние
        }
    }

    void MoveCamera(string direction) //Метод движения камеры (рамки обзора)
    {
        cameraMover.TurnCamera(direction);
    }

    void ProcessNosedown() //Метод пике (когда вместе с повротом игрока повороачивается камера)
    {
        zThrow = CrossPlatformInputManager.GetAxis("Horizontal_1"); //Берем ось х второго джойстика на контроллере (это число в пределах -1:1)
        float playerRollWithCamera = zThrow * zRollFactor;// Умножаем это число на максимально возможный угол поворота (при 1 будет максимальный наклон)
        transform.Rotate(0f, 0f, playerRollWithCamera);// Поворачиваем игрока на соответствующий угол
        cameraMover.RotateCamera(zThrow); //Запускаем метод поворота камеры из другого скрипта
    }
}
