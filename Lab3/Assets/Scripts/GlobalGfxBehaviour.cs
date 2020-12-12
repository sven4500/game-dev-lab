using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGfxBehaviour: MonoBehaviour {

	void LateUpdate() {
        if (_camera != null) {
            // Используем линейную интерполяцию цвета фона. Метод PingPong грубо говоря
            // заключает значение внутри отрезка [0; changeSpeed] поэтому нормализуем это значение
            // так как на вход Lerp нужно значение на отрезке [0; 1].
            Color color = Color.Lerp(_startColor, _endColor, Mathf.PingPong(Time.time, _changeSpeed) / _changeSpeed);
            _camera.backgroundColor = color;
        }
	}

    // Камера фон которой мы будем менять.
    public Camera _camera = null;

    public Color _startColor;
    public Color _endColor;
    public System.Single _changeSpeed = 1.0F;

}
