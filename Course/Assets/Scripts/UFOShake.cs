using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShake: MonoBehaviour {

    void Awake() {
        Vector3 startingAngles = new Vector3(
            Random.Range(-10.0F, +10.0F) * _amplitudeFactor,
            Random.Range(-10.0F, +10.0F) * _amplitudeFactor,
            Random.Range(-10.0F, +10.0F) * _amplitudeFactor);
        
        transform.Rotate(startingAngles);
        _rigidbody = GetComponent<Rigidbody>();
    }

	void LateUpdate() {
        Rotate();
        Levitate();
	}

    void Levitate() {
        System.Single pingPong = Mathf.PingPong(Time.time, _levitationTime) / _levitationTime;
        System.Single amplitude = Mathf.Lerp(-_levitationAmplitude, +_levitationAmplitude, pingPong);

        Vector3 position = _rigidbody.position;
        position.y += amplitude;

        _rigidbody.MovePosition(position);
    }

    void Rotate() {
        Vector3 pingPongs = new Vector3(
            Mathf.PingPong(Time.time, _timeFactor) / _timeFactor,
            Mathf.PingPong(Time.time, _timeFactor) / _timeFactor,
            Mathf.PingPong(Time.time, _timeFactor) / _timeFactor);

        Vector3 rot = _rigidbody.rotation.eulerAngles;
        rot.x = Mathf.Lerp(-10.0F, +10.0F, pingPongs.x) * _amplitudeFactor;
        rot.y += 10.0F * _amplitudeFactor / _timeFactor;
        rot.z = Mathf.Lerp(-10.0F, +10.0F, pingPongs.y) * _amplitudeFactor;

        _rigidbody.MoveRotation(Quaternion.Euler(rot));
    }

    // Объект над которым производим вращение.
    public GameObject _object = null;

    public System.Single _levitationAmplitude = 0.01F;
    public System.Single _levitationTime = 1.0F;

    // Сила с которой колеблется объект.
    public System.Single _amplitudeFactor = 1.0F;
    public System.Single _timeFactor = 1.0F;

    private Rigidbody _rigidbody;

}
