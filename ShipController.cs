using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController: MonoBehaviour {

	public GameObject Bullet;
	public Rigidbody rbb;
	public float radianAngle;
	public Bullet B;
    GameObject[] CannonL;
	GameObject[] CannonR;
	public GameObject Explosion;
	float LeftCannonTimer;
	float RightCannonTimer;
	public Slider Health;


	// Use this for initialization
	void Start () {

//		timer.SetActive (false);

		LeftCannonTimer = 0f;
		RightCannonTimer = 0f;

		CannonL = GameObject.FindGameObjectsWithTag ("CannonL");
		CannonR = GameObject.FindGameObjectsWithTag ("CannonR");

		B = Bullet.GetComponent<Bullet> ();

		rbb = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void Update () {

		if(Health.value == 0){
			gameObject.transform.GetChild (0).parent = null; 
			Destroy (gameObject);
		}

		if(LeftCannonTimer > 0f){
			LeftCannonTimer -= Time.deltaTime;
		}


		if(RightCannonTimer > 0f){
			RightCannonTimer -= Time.deltaTime;
		} 

		radianAngle = 2 * Mathf.PI / 360;

		rbb.AddForce (Vector3.forward * Mathf.Cos (transform.rotation.eulerAngles.y * radianAngle) * 20f, ForceMode.Impulse);
		rbb.AddForce (Vector3.right * Mathf.Sin (transform.rotation.eulerAngles.y * radianAngle) * 20f, ForceMode.Impulse);


		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			rbb.angularVelocity = new Vector3 (0, -0.5f, 0);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			rbb.angularVelocity = new Vector3 (0, 0.5f, 0);
		}
			
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			if(touchDeltaPosition.y > 0){
				rbb.velocity = new Vector3 (rbb.velocity.x + 0.5f * Mathf.Sin (transform.rotation.eulerAngles.y * radianAngle),0,rbb.velocity.z + 0.5f * Mathf.Cos (transform.rotation.eulerAngles.y * radianAngle));
			}
			if(touchDeltaPosition.y < 0){
				rbb.velocity = new Vector3 (rbb.velocity.x - 0.5f * Mathf.Sin (transform.rotation.eulerAngles.y * radianAngle),0,rbb.velocity.z - 0.5f * Mathf.Cos (transform.rotation.eulerAngles.y * radianAngle));
			}
		}

		if (Input.acceleration.x != 0) {
			if (Input.acceleration.x > 0) {
				rbb.angularVelocity = new Vector3 (0, 1f * Input.acceleration.x, 0);
			}
			if (Input.acceleration.x < 0) {
				rbb.angularVelocity = new Vector3 (0, 1f * Input.acceleration.x, 0);
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "EBullet"){
			Health.value -= 10f;
		}
	}

	public void FireLeftB(){
		B.speed = -10f;

		if(LeftCannonTimer <= 0f){
		foreach (GameObject go in CannonL) {
				Instantiate (Bullet,go.transform.GetChild(0).transform.position, new Quaternion (0, 0, 0, 0));
			//Instantiate (Explosion, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
			}
			LeftCannonTimer = 2f;
		}
	
	}
		
	public void FireRightB(){
		B.speed = 10f;
		if(RightCannonTimer <= 0f){
		foreach (GameObject go in CannonR) {
			Instantiate (Bullet, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
			//Instantiate (Explosion, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
			}
			RightCannonTimer = 2f;
		}
}

}