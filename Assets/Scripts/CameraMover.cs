using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraMover : MonoBehaviour {

    [SerializeField] float movementSpeedFactor = 1f;
    [SerializeField] float rotationSpeedFactor = 1f;
    [SerializeField] float noseDownSpeedFactor = 2f;

    private bool isActive = true;

    
    void Update () {

        if (isActive)
        {
            ProcessTranslation();
        }

    }

    private void ProcessTranslation()
    {
        transform.Translate(Vector3.forward * movementSpeedFactor, Space.Self); //Прямолинейное движение камеры вместе с игроком
    }

    public void TurnCamera(string direction) //Метод движения камеры (рамки обзора)
    {

        switch (direction)
        {
            case "up":
                transform.Rotate(new Vector3(-rotationSpeedFactor, 0f, 0f), Space.Self);
                break;
            case "down":
                transform.Rotate(new Vector3(rotationSpeedFactor, 0f, 0f), Space.Self);
                break;
            case "left":
                transform.Rotate(new Vector3(0f, rotationSpeedFactor, 0f), Space.Self);
                break;
            case "right":
                transform.Rotate(new Vector3(0f, -rotationSpeedFactor, 0f), Space.Self);
                break;
        }

    }

    public void RotateCamera(float zThrow) //Метод поворота камеры в пике
    {
        transform.Rotate(new Vector3(0f, 0f, zThrow * noseDownSpeedFactor), Space.Self);
    }

    public void OnPlayerDeath() // При взрыве игрока
    {
        isActive = false;
    }

    

}
