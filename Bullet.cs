using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Rigidbody rb;
	public ShipController SCC;
	public float speed;

	// Use this for initialization
	void Awake () {

		rb = GetComponent<Rigidbody> ();
		SCC = GameObject.Find ("Ship").GetComponent<ShipController>();

	}
	void Start(){

		rb.velocity = new Vector3 (speed * Mathf.Cos (SCC.transform.rotation.eulerAngles.y * SCC.radianAngle),3f,-speed * Mathf.Sin (SCC.transform.rotation.eulerAngles.y * SCC.radianAngle));

	}

	// Update is called once per frame
	void Update () {

		if (transform.position.y < -1) {
			Destroy (gameObject);
		}

	}
		
}

