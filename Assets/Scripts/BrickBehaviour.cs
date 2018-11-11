using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour {

    static readonly private Color[] _col = new Color[] {
        new Color(0.639F, 0.286F, 0.643F, 1.000F),
        new Color(1.000F, 0.498F, 0.153F, 1.000F),
        new Color(0.710F, 0.902F, 0.114F, 1.000F),
        new Color(0.929F, 0.110F, 0.141F, 1.000F)
    };

    void Awake () {
		// Получаем (кэшируем) кокпонент отрисовки спрайта.
		// Будем использовать его для изменения цвета кирпичика
		// в записимрсти от количества попаданий в него.
        _spr = GetComponent<SpriteRenderer>();
		Health = _health;
    }
	
	private void OnCollisionEnter2D (Collision2D other) {
		// Случилось столкновение поетому уменьшаем жизни на единицу.
		--Health;
	}

	public System.UInt32 Health {
        set {
            _health = value;
            
            if (value >= 1 && value <= _col.Length)
                _spr.color = _col[_health - 1];

            // Если жизней больше не осталось, то самоубиваемся
            // (странно, но что поделать ¯\_(ツ)_/¯ ).
            if (Health == 0)
                Object.Destroy(this.gameObject);
        }
        get {
            return _health;
        }
    }

    protected System.UInt32 _health = 1;
    protected SpriteRenderer _spr;
}
