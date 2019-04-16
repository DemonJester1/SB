using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public Rigidbody rb;
	public EnemyShip ES;
	public float speed;

	// Use this for initialization
	void Awake () {

		rb = GetComponent<Rigidbody> ();
		ES = GameObject.Find ("EnemyShip").GetComponent<EnemyShip>();

	}
	void Start(){

		rb.velocity = new Vector3 (speed * Mathf.Cos (ES.transform.rotation.eulerAngles.y * ES.radianAngle),3f,-speed * Mathf.Sin (ES.transform.rotation.eulerAngles.y * ES.radianAngle));
	
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.y < -1) {
			Destroy (gameObject);
		}

	}

}