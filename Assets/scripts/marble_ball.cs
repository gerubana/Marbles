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
	private float damage;
	private float Max_hp;
	private float tmp_hp;
	private float tmp_attack;
	//UI
	private UI_controller UI_script;
	// Use this for initialization
	void Start () {
		UI_script = GameObject.Find("UI Root").GetComponent<UI_controller>();
		player = GameObject.Find(Goble_Player.playerName);
		player_body_state = player.GetComponent<body> ();
		speed = player_body_state.shoot_speed;
		Destroy (this.gameObject, 2.0f);
	}

	void Update () {
		this.transform.Translate (0, 0, speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider object_)
	{
		if (object_.tag == "Player") { 
			tmp_attack = object_.GetComponent<body> ().attack;
			Max_hp = object_.GetComponent<body> ().Max_hp;
			damage = tmp_attack / Max_hp;

			explosion ();
			object_.GetComponent<body> ().HP -= damage;
			//if(不等於絕招 && player_body_state.SP == 1.0f){
			object_.GetComponent<body> ().SP += damage;

			UI_script.change_emeny_value (object_.GetComponent<body> ().HP , object_.GetComponent<body> ().SP );

			player_body_state.SP += 0.05f;

			Debug.Log (player_body_state.SP);
			//}

			Destroy (gameObject);
		} else {
			explosion ();
			Destroy (gameObject);
		}
	}

	public void explosion()
	{
		Fire_smoke_ins = Instantiate(hit_small, transform.position, transform.rotation)as GameObject;
	}
}
