using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marble_ball : MonoBehaviour {
	//不同爆炸模式
	public GameObject hit_small;
	public GameObject hit_big;
	public GameObject hit_over;
	public GameObject hit_break;
	//子彈類型
	public bool Skill = false;
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

	private ThirdPersonCamera camera_script;

	//UI
	private UI_controller UI_script;
	// Use this for initialization
	void Start () {
		UI_script = GameObject.Find("UI Root").GetComponent<UI_controller>();
		player = GameObject.Find(Goble_Player.playerName);
		player_body_state = player.GetComponent<body> ();
		camera_script = GameObject.Find("Main Camera").GetComponent<ThirdPersonCamera>();
		speed = player_body_state.shoot_speed;
		Destroy (this.gameObject, 2.0f);
	}

	void Update () {
		this.transform.Translate (0, 0, speed * Time.fixedDeltaTime);
	}

	void OnTriggerEnter(Collider object_)
	{

		Debug.Log (object_);
		if (object_.tag == "Player" )
		{ 
			Max_hp = object_.GetComponent<body> ().Max_hp;
			damage = self_ball_attack / 10 / Max_hp;


			object_.GetComponent<body> ().HP -= damage;
			object_.GetComponent<body> ().SP += damage;

			if (!Skill) {
				explosion ("normal");
			} else {
				explosion ("skill");
			}

			Destroy (gameObject);

			if(object_.name == Goble_Player.playerName)
				camera_script.shakeCamera (0.5f);
		} 
		else if (object_.tag == "ball") 
		{ 
			emeny_ball_attack = object_.GetComponent<marble_ball> ().self_ball_attack;
			compare_attack = self_ball_attack - emeny_ball_attack;
			if (Mathf.Abs (compare_attack) <= 50) { 
				explosion ("break");
				Destroy (gameObject);
			} else {
				if (self_ball_attack < emeny_ball_attack) {
					explosion ("break");
					Destroy (gameObject);
				} else {
					self_ball_attack = compare_attack;
				}
			}
		} 
		else 
		{
			if (!Skill) {
				explosion ("normal");
				Destroy (gameObject);
			}
		}
	}

	public void explosion(string power)
	{
		switch(power)
		{
		case "normal":
			Fire_smoke_ins = Instantiate (hit_small, transform.position, transform.rotation)as GameObject;
			return;
		case "skill":
			Fire_smoke_ins = Instantiate(hit_big, transform.position, transform.rotation)as GameObject;
			return;
		case "gameover":
			Fire_smoke_ins = Instantiate(hit_over, transform.position, transform.rotation)as GameObject;
			return;
		case "break":
			Fire_smoke_ins = Instantiate(hit_break, transform.position, transform.rotation)as GameObject;
			return;
		default :
			Fire_smoke_ins = Instantiate(hit_small, transform.position, transform.rotation)as GameObject;
			return;
		}

	}
}
