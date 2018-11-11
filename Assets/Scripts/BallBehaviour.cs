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
    public void Push (Vector2 normal, System.Single factor = 0.0F) {
        // На всякий случай сохраняем текущий вектор направления.
        // Если нахи преобразования дадут плохой результат
        // (углы отскока будут слишком малыми), то мы повторим этот
        // метод ещё один раз со старым вектором.
        Vector2 temp = _dir;

        // Сперва нормализуем вектор направления. Он может стать
        // не нормальным после столкновения с поатформой, когда мы
        // искуственно добавляем искаредие отражения ради интереса
        // и просто ради нелинейности нашей игры.
        _dir.Normalize();

        // Применяем алгоритм Unity для вычисления угла отражения.
		_dir = Vector2.Reflect(_dir, normal);

        // Добавляем некоторую степень дискретности порога вхждения.
        System.Single fx = Random.Range(0.0F, 1.0F);
        System.Single fy = Random.Range(0.0F, 1.0F);

        // Ограничиваем фактор-параметр на отрезке [0.0, 1.0]
        factor = Mathf.Max(factor, 0.0F);
        factor = Mathf.Min(factor, 1.0F);

        // Компоненту x вектора направления меняем в любом направлении,
        // а вот компоненту y только в направлении движения. Иначе может
        // возникнуть такая ситуация когда шарик вроде должен отскочить
        // а он продолжит двигаться в прежнем направлении.
        _dir.x += fx * factor;
        _dir.y += fy * factor * Mathf.Sign(_dir.y);

        // Здесь важно вернуть вектору прежнее направление иначе после
        // повторного вхождения в функцию шарик может продолжить движение
        // по старой траектории минуя препядствие.
        System.Single alpha = Vector2.Angle(_dir, Vector2.right);
        if (alpha < 15.0F || alpha > 165.0F) {
            //Debug.Log("Horz angle too narrow");
            _dir = temp;
            Push(normal, 0.5F);
        }

        System.Single beta = Vector2.Angle(_dir, Vector2.up);
        if (beta < 15.0F || beta > 165.0F) {
            //Debug.Log("Vert angle too narrow");
            _dir = temp;
            Push(normal, 0.5F);
        }
    }

    public System.Single _speed = 1.0F;

    private Vector2 _dir;
    private Rigidbody2D _rb;

}
