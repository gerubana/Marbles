using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootment : MonoBehaviour {

	public GameObject marble_ball;
	public float speed;
	private GameObject marble_ball_ins;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetButtonDown("Fire1"))
		{
			shoot ();
		}
	}

	private void shoot()
	{

		marble_ball_ins = Instantiate(marble_ball, transform.position, transform.rotation)as GameObject;
		marble_ball_ins.transform.Translate (0, 0, speed * Time.fixedDeltaTime);

	}
}
