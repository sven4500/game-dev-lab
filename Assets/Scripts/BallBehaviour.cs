using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    private void Awake () {
		// Выбираем произвольное направление вектора движения с
		// ограничением в нижней полуплоскости.
		float val = Random.Range(0.0F, Mathf.PI);
        _dir.x = Mathf.Cos(val);
        _dir.y = Mathf.Sin(val);
		
        // Получаем (кэшируем) компонент твердого тела.
		// Далее будем использовать его для перемещения шарика.
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate () {
        Vector2 tr = _dir * _speed * Time.deltaTime;
        _rb.MovePosition(_rb.position + tr);
    }

    // Метод вызывается всякий раз когда шарик натыкается на препядствие.
    public void Bounce (Vector2 norm) {
        if (norm.x != 0.0F)
            _dir.x *= -1.0F;
        if (norm.y != 0.0F)
            _dir.y *= -1.0F;
    }

    public float _speed = 1.0F;

    private Vector2 _dir;
    private Rigidbody2D _rb;

}
