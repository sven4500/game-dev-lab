using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour: MonoBehaviour {

    void Awake() {
        // Получаем первый материал который добавлен к объекту
        // (в нашем случае он всего один).
        _material = GetComponent<MeshRenderer>().material;
        IsSelected = false;
    }

    public System.Boolean IsSelected {
        get {
            return _isSelected;
        }

        set {
            _material.color = (value == true) ? Color.red : Color.white;
            _isSelected = value;
        }
    }

    private System.Boolean _isSelected;
    private Material _material;

}
