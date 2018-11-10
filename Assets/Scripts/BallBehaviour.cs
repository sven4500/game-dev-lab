using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    private void Awake () {
		// Выбираем произвольное направление вектора движения с
		// ограничением в нижней полуплоскости.
		System.Single val = Random.Range(0.0F, Mathf.PI);
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

    /*private Vector2 rotate (Vector2 vect, System.Single deg) {
        System.Single rad = deg * Mathf.Deg2Rad;
        System.Single a = Mathf.Cos(rad);
        System.Single b = Mathf.Sin(rad);
        return new Vector2(
            a * vect.x - b * vect.y,
            a * vect.y + b * vect.x
        );
    }*/

    // Метод вызывается всякий раз когда шарик натыкается на препядствие.
    public void Push (Vector2 normal, System.Single factor = 0.0F) {
        _dir.Normalize();
		_dir = Vector2.Reflect(_dir, normal);

        // Отрицательный вклад по оси Y давать нельзя.
        System.Single dx = Random.Range(-1.0F, 1.0F);
        System.Single dy = Random.Range(0.0F, 1.0F);

        factor = Mathf.Max(factor, 0.0F);
        factor = Mathf.Min(factor, 1.0F);

        _dir.x += dx * factor;
        _dir.y += dy * factor;
    }

    public System.Single _speed = 1.0F;

    private Vector2 _dir;
    private Rigidbody2D _rb;

}
