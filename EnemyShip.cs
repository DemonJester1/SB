using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyShip : MonoBehaviour {

	public float range;
	public float speed;
	public NavMeshAgent nav;
	float timer = 0;
	float attackSpeed = 2f;
	Vector3 targetPosition;
	public ShipController SC;
	Quaternion targetRotation;
	EnemyBullet EB;
	float LeftCannonTimer;
	float RightCannonTimer;
	public float radianAngle;
	public GameObject BulletEnemy;
	GameObject target;
	public Slider HealthEnemy;
	GameObject[] CannonL;
	GameObject[] CannonR;


	void Start () {

		CannonL = GameObject.FindGameObjectsWithTag ("ECannonL");
		CannonR = GameObject.FindGameObjectsWithTag ("ECannonR");

		EB = BulletEnemy.GetComponent<EnemyBullet> ();

		SC = GameObject.Find ("Ship").GetComponent<ShipController>();

		timer = attackSpeed;

		nav.speed = Random.Range (1f, 2f);
		GameObject[] EnemyShips = GameObject.FindGameObjectsWithTag("Ship");
		float minDist = Mathf.Infinity;
		foreach (GameObject EnemyShip in EnemyShips) {
			float dist = Vector3.Distance (this.transform.position, EnemyShip.transform.position);
			if (dist < minDist){
				target = EnemyShip;
				minDist = dist;
			}

		}
	}
		
	void Update () {

		if(HealthEnemy.value == 0){
			Destroy (gameObject);
			HealthEnemy.gameObject.SetActive (false);
		}


		radianAngle = 2 * Mathf.PI / 360;

		if(LeftCannonTimer > 0f){
			LeftCannonTimer -= Time.deltaTime;
		}
		if(RightCannonTimer > 0f){
			RightCannonTimer -= Time.deltaTime;
		}

		if (target != null) {
			timer -= Time.deltaTime;
			if (Vector3.Distance (this.transform.position, target.transform.position) > range) {
				nav.destination = target.transform.position;
			} else {
				if(Vector3.Distance (this.transform.position, target.transform.position) < 12f){
				this.transform.rotation = Quaternion.Euler (transform.rotation.x, target.transform.eulerAngles.y+180, transform.rotation.z);
				}
				nav.destination = target.transform.position;

				if (Vector3.Distance (this.transform.position, target.transform.position) < 6f) {
					nav.transform.RotateAround (target.transform.position, Vector3.up, 20f * Time.deltaTime);
				}
					
				float sinval = Mathf.Sin (transform.rotation.eulerAngles.y * radianAngle);
				float cosval = Mathf.Cos (transform.rotation.eulerAngles.y * radianAngle);

				if(Physics.Raycast(transform.position,new Vector3(-cosval,0,sinval),10f) == true && LeftCannonTimer <= 0f){
					AttackLeft ();
				}

				if(Physics.Raycast(transform.position,new Vector3(cosval,0,-sinval),10f) == true && RightCannonTimer <= 0f){
					AttackRight ();
				}			
				}
			}
		}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Bullet"){
			HealthEnemy.value -= 10f;
		}
	}

	void AttackLeft(){
		EB.speed = -10f;
		if(LeftCannonTimer <= 0f){
			foreach (GameObject go in CannonL) {
				Instantiate (BulletEnemy, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
				//Instantiate (Explosion, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
			}
			LeftCannonTimer = 2f;
		}
	}

	void AttackRight(){
		EB.speed = 10f;
		if (RightCannonTimer <= 0f) {
			foreach (GameObject go in CannonR) {
				Instantiate (BulletEnemy, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
				//Instantiate (Explosion, go.transform.GetChild (0).transform.position, new Quaternion (0, 0, 0, 0));
			}
			RightCannonTimer = 2f;
		}
	}
}