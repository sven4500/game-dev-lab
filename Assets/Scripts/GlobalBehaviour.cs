using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour: MonoBehaviour
{
    void Awake()
    {
        _cameraBehaviour = (_cam) ? _cam.GetComponent<CameraBehaviour>() : null;
    }

    void LateUpdate()
    {
        handleInput();
        //changeColor();
    }

    private void handleInput()
    {
        if (_cameraBehaviour == null)
            return;
        // Смотрим состояние ПКМ. Если нажата, то разрешаем камере двигаться.
        _cameraBehaviour._allowRotation = Input.GetMouseButton(1);
    }

    private void changeColor()
    {
        Color col = new Color();
        col.r = Random.Range(0.0F, 1.0F);
        col.g = Random.Range(0.0F, 1.0F);
        col.b = Random.Range(0.0F, 1.0F);
        _cam.backgroundColor = col;
    }

    // Основная камера фон которой мы будем менять.
    public Color _col = new Color();
    public Camera _cam = null;
    public System.Single _changeSpeed = 1.0F;
    private CameraBehaviour _cameraBehaviour;

}
