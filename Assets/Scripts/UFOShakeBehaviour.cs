using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShakeBehaviour : MonoBehaviour {

    void Awake() {
        Vector3 startingAngles = new Vector3(
            Random.Range(-10.0F, +10.0F) * _amplitudeFactor,
            Random.Range(-10.0F, +10.0F) * _amplitudeFactor,
            Random.Range(-10.0F, +10.0F) * _amplitudeFactor);

        _times = new Vector3(
            Random.Range(0.0F, +10.0F) * _timeFactor,
            Random.Range(0.0F, +10.0F) * _timeFactor,
            Random.Range(0.0F, +10.0F) * _timeFactor);

        transform.Rotate(startingAngles);
        _rigidbody = GetComponent<Rigidbody>();
    }

	void LateUpdate() {
        Rotate();
        Levitate();
	}

    void Levitate() {
        System.Single pingPong = Mathf.PingPong(Time.time, _levitationTime) / _levitationTime;
        Debug.Log(pingPong);
        System.Single amplitude = Mathf.Lerp(-_levitationAmplitude, +_levitationAmplitude, pingPong);
        Vector3 position = _rigidbody.position;
        position.y += amplitude;
        _rigidbody.MovePosition(position);
    }

    void Rotate() {
        Vector3 pingPongs = new Vector3(
            Mathf.PingPong(Time.time, _times.x) / _times.x,
            Mathf.PingPong(Time.time, _times.y) / _times.y,
            Mathf.PingPong(Time.time, _times.z) / _times.z);

        Vector3 rotations = new Vector3(
            Mathf.Lerp(-10.0F, +10.0F, pingPongs.x) * _amplitudeFactor,
            Mathf.Lerp(-10.0F, +10.0F, pingPongs.y) * _amplitudeFactor,
            0.0F/*Mathf.Lerp(-10.0F, +10.0F, pingPongs.z)*/);

        //Vector3 rotation = _object.transform.rotation;
        //rotation.x += 

        //_object.transform.Rotate(rotations);
    }

    // Объект над которым производим вращение.
    public GameObject _object = null;

    public System.Single _levitationAmplitude = 0.01F;
    public System.Single _levitationTime = 1.0F;

    // Сила с которой колеблется объект.
    public System.Single _amplitudeFactor = 1.0F;
    public System.Single _timeFactor = 1.0F;

    private Rigidbody _rigidbody;

    //private Vector3 _startingAngles;
    private Vector3 _times;

}
