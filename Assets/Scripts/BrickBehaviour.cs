using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour {

    /*public System.UInt32 Health {
        set {
            _health = value;
        }
        get {
            return _health;
        }
    }*/

    static readonly private Color[] _col = new Color[] {
        new Color(0.639F, 0.286F, 0.643F, 1.000F),
        new Color(1.000F, 0.498F, 0.153F, 1.000F),
        new Color(0.710F, 0.902F, 0.114F, 1.000F),
        new Color(0.929F, 0.110F, 0.141F, 1.000F)
    };

    void Awake () {
        _spr = GetComponent<SpriteRenderer>();
        /*Debug.Log(_spr.color);
        Debug.Log(_col[_health]);
        Debug.Log(_health);*/
        _spr.color = _col[_health];
        //Debug.Log(_spr.color);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public System.UInt32 _health = 1;
    protected SpriteRenderer _spr;
}
