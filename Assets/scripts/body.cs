using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour {
	public float shoot_speed; 
	public float moveSpeed; 
	public GameObject marble_ball;
	public GameObject Fire;
	private GameObject marble_ball_ins;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider object_)
	{
		if (object_.tag == "ball") {

			Destroy (object_.gameObject);
		}
	}

	void FixedUpdate () {
		if(Input.GetButtonDown("Fire1"))
		{
			shoot ();
		}
	}

	private void shoot()
	{

		marble_ball_ins = Instantiate(marble_ball, Fire.transform.position, transform.rotation)as GameObject;
		marble_ball_ins.transform.Translate (0, 0, shoot_speed * Time.fixedDeltaTime);

	}

}
