using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_controller : MonoBehaviour {

	public GameObject HP_self;
	public GameObject SP_self;
	public GameObject Icon_self;
	public GameObject Bullet;
	public GameObject HP_emeny;
	public GameObject SP_emeny;
	public GameObject Icon_emeny;
	public GameObject Time;


	private GameObject player;
	private body player_body_state;
	private int bullet_max = 0;
	private int bullet_now = 0;
	private float now_time = 0;
	private string sec = "00";
	private string min = "00";
	private string hour = "00";
	// Use this for initialization
	void Start () {
		if(Goble_Player.playerName == null)
			Goble_Player.playerName = this.name;

		player = GameObject.Find(Goble_Player.playerName);
		player_body_state = player.GetComponent<body> ();
		bullet_max = player_body_state.Bullets_able_num;
		bullet_now = bullet_max;
		restart ();
	}
	
	// Update is called once per frame
	void Update () {

		bullet_now = player_body_state.Bullets_able_num;
		Bullet.GetComponent<UILabel> ().text = bullet_now.ToString() + " / " + bullet_max.ToString();
	}

	private void restart()
	{
		HP_self.GetComponent<UITexture> ().fillAmount = 1.0f;
		SP_self.GetComponent<UITexture> ().fillAmount = 0f;
		HP_emeny.GetComponent<UITexture> ().fillAmount = 1.0f;
		SP_emeny.GetComponent<UITexture> ().fillAmount = 0f;
		sec = "00";
		min = "00";
		hour = "00";
		Time.GetComponent<UILabel>().text = hour+":"+min+":"+sec;
		Bullet.GetComponent<UILabel> ().text = bullet_now.ToString() + " / " + bullet_max.ToString();
		player_body_state.Bullets_able_num = bullet_max;
	}

	public void change_self_value(float hp, float sp, string icon)
	{
		HP_self.GetComponent<UITexture> ().fillAmount = 0.5f;
	}
}
