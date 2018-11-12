using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviour : MonoBehaviour {

    void Awake () {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //changeColor();
	}

    private void changeColor () {
        Color col = new Color();
        col.r = Random.Range(0.0F, 1.0F);
        col.g = Random.Range(0.0F, 1.0F);
        col.b = Random.Range(0.0F, 1.0F);
        _cam.backgroundColor = col;
    }

    // Основная камера фон которой мы будем менять.
    public Color _col = new Color();
    public Camera _cam = null;
    public System.Single _changeSpeed = 1.0F;

}
