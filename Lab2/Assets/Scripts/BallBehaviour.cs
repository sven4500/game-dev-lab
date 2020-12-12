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
        _pushInProcess = false;
    }

    // Метод вызывается всякий раз когда шарик натыкается на препядствие.
    public void Push (Vector2 normal, System.Single factor = 0.0F) {
        // Абсолютно необходимая переменная. Метод Push может быть вызван
        // несколько раз за кадр если шарик попадает на границу нескольких
        // кирпичиков. В этом случае обрабатываем только первый зпрос.
        // В следующем кадре это значение будет сброшено.
        if (_pushInProcess == true)
            return;
        _pushInProcess = true;

        // Применяем алгоритм Unity для вычисления угла отражения.
		_dir = Vector2.Reflect(_dir, normal);

        // Вычисляем пару произвольных чисел которые добавим к
        // компонентам вектора направления движения.
        System.Single fx = Random.Range(0.0F, 1.0F);
        System.Single fy = Random.Range(0.0F, 1.0F);

        // Ограничиваем фактор-параметр на отрезке [0.0, 1.0]
        factor = Mathf.Max(factor, 0.0F);
        factor = Mathf.Min(factor, 1.0F);
        
        // Компоненты вектора всегда меняем только в направлении движения.
        // Иначе может возникнуть такая ситуация когда шарик вроде должен отскочить
        // а он продолжит двигаться в прежнем направлении. В итоге получаем
        // fx, fy это те значения которые мы хотим добавить к компонентам вектора, а
        // factor это то насколько сильно будет заметен их вклад.
        _dir.x += fx * factor * Mathf.Sign(_dir.x);
        _dir.y += fy * factor * Mathf.Sign(_dir.y);

        if (Mathf.Abs(_dir.x) < 0.3) {
            //Debug.Log("Angle too narrow");
            _dir.x = Mathf.Sign(_dir.x) * 0.3F;
        }

        if (Mathf.Abs(_dir.y) < 0.3) {
            //Debug.Log("Angle too narrow");
            _dir.y = Mathf.Sign(_dir.y) * 0.3F;
        }

        // После преобразований нормализуем вектор направления
        // таким образом сохраняем скорость постоянной без
        // особых потерь по производительности =P. Конечно понятно
        // что сейчас это не критично но думаю в старые добрые это
        // было гораздо более существенно.
        _dir.Normalize();
    }

    public System.Single _speed = 1.0F;

    private Vector2 _dir;
    private Rigidbody2D _rb;
    private System.Boolean _pushInProcess = false;

}
