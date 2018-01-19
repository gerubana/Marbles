using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marble_ball : MonoBehaviour {

	private float speed;
	private GameObject player;
	private body player_body_state;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		player_body_state = player.GetComponent<body> ();
		speed = player_body_state.shoot_speed;
		Destroy (this.gameObject, 2.0f);
	}

	void Update () {
		this.transform.Translate (0, 0, speed * Time.fixedDeltaTime);
	}

}
