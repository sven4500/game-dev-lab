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

    // Метод вызывается всякий раз когда шарик натыкается на препядствие.
    public void Push (Vector2 normal) {
		// На всякий случай нормализуем этот вектор, а то всякое
		// бывает по улицам сейчас ходить опасно.
		//normal.Normalize();
		
		/*System.Single alpha = Vector2.Angle(_dir, normal);
		System.Single beta = Vector2.Angle(_dir, Vector2.up);
		//System.Single gamma = ((beta + 2.0F * alpha) / 180.0F) * Mathf.PI;
		System.Single gamma = ((beta + 180.0F - 2.0F * alpha) / 180.0F) * Mathf.PI;
		
		_dir.x = Mathf.Cos(gamma);
		_dir.y = Mathf.Sin(gamma);
		
		Debug.Log(_dir);*/
		
		_dir = Vector2.Reflect(_dir, normal);
    }

    public System.Single _speed = 1.0F;

    private Vector2 _dir;
    private Rigidbody2D _rb;

}
