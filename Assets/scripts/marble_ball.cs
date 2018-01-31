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
	public float self_ball_attack; //根據不同子彈改變數值，子彈互撞沒消失也要改變
	private float emeny_ball_attack;
	private float compare_attack;
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

		Debug.Log (object_);
		if (object_.tag == "Player") { 
			Max_hp = object_.GetComponent<body> ().Max_hp;
			damage = self_ball_attack / 10 / Max_hp;

			explosion ();
			object_.GetComponent<body> ().HP -= damage;
			//if(不等於絕招 && player_body_state.SP == 1.0f){
			object_.GetComponent<body> ().SP += damage;

			UI_script.change_emeny_value (object_.GetComponent<body> ().HP , object_.GetComponent<body> ().SP );

			player_body_state.SP += 0.05f;

			Debug.Log (player_body_state.SP);
			//}

			Destroy (gameObject);
		} else if (object_.tag == "ball") { 
			emeny_ball_attack = object_.GetComponent<marble_ball> ().self_ball_attack;
			compare_attack = self_ball_attack - emeny_ball_attack;
			if (Mathf.Abs (compare_attack) <= 50) { 
				explosion ();
				Destroy (gameObject);
			} else {
				if (self_ball_attack < emeny_ball_attack) {
					explosion ();
					Destroy (gameObject);
				} else {
					self_ball_attack = compare_attack;
				}
			}
		} else {
			explosion ();
			Destroy (gameObject);
		}
	}

	/*void OnCollisionEnter(Collision  object_)
	{
		if (object_.gameObject.tag == "Player") { 
			Max_hp = object_.gameObject.GetComponent<body> ().Max_hp;
			damage = self_ball_attack / 10 / Max_hp;

			explosion ();
			object_.gameObject.GetComponent<body> ().HP -= damage;
			//if(不等於絕招 && player_body_state.SP == 1.0f){
			object_.gameObject.GetComponent<body> ().SP += damage;

			UI_script.change_emeny_value (object_.gameObject.GetComponent<body> ().HP , object_.gameObject.GetComponent<body> ().SP );

			player_body_state.SP += 0.05f;

			//Debug.Log (player_body_state.SP);
			//}

			Destroy (gameObject);
		} else if (object_.gameObject.tag == "ball") { 
			emeny_ball_attack = object_.gameObject.GetComponent<marble_ball> ().self_ball_attack;
			compare_attack = self_ball_attack - emeny_ball_attack;
			if (Mathf.Abs (compare_attack) <= 50) { 
				explosion ();
				Destroy (gameObject);
			} else {
				if (self_ball_attack < emeny_ball_attack) {
					explosion ();
					Destroy (gameObject);
				} else {
					self_ball_attack = compare_attack;
				}
			}
		} else {
			explosion ();
			Destroy (gameObject);
		}
	}*/

	public void explosion()
	{
		Fire_smoke_ins = Instantiate(hit_small, transform.position, transform.rotation)as GameObject;
	}
}
