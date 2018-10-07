using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    //public float _delta = 0.1f;
    //public float _scaling = 1.5f;

    private bool _isSelected = false;
    //private LineRenderer _lr;
    private SpriteRenderer _sr;

    public bool IsSelected
    {
        get { return _isSelected; }
        set {
            if (value == true)
                _sr.color = Color.red;
            else
                _sr.color = Color.white;
            _isSelected = value;
        }
    }

    void Awake()
    {
        //_lr = new LineRenderer();
        //lr.setColor
        _sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
