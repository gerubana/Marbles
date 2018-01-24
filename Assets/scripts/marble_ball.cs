using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marble_ball : MonoBehaviour {
	//不同爆炸模式
	public GameObject hit_small;
	public GameObject hit_big;
	public GameObject hit_over;
	//產生爆炸效果
	private GameObject Fire_smoke_ins;

	private float speed;
	private GameObject player;
	private body player_body_state;
	// Use this for initialization
	void Start () {
		player = GameObject.Find(Goble_Player.playerName);
		player_body_state = player.GetComponent<body> ();
		speed = player_body_state.shoot_speed;
		Destroy (this.gameObject, 2.0f);
	}

	void Update () {
		this.transform.Translate (0, 0, speed * Time.fixedDeltaTime);
	}

	public void explosion()
	{
		
		Fire_smoke_ins = Instantiate(hit_small, transform.position, transform.rotation)as GameObject;
	}
}
