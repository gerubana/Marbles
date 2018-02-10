using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour {
	//基本數值
	public float shoot_speed = 10f; 
	public float moveSpeed = 10f; 
	public float fillSpeed = 0.3f; 
	public float HP = 1.0f;
	public float SP = 0; //max 100
	public float attack = 200f; //base 20  
	public float Max_hp = 500f; //base 500 
	public int Bullets_able_num = 15; 
	//技能 1.單體大絕 *2.5 (SP-50) 2.單體連發 10*0.2 (SP-40) 3.同時擊發 10*1 (SP-35/40)
	/*
	 攻擊力設定調整 --> 原先 20(up0.5 MAX120) 改為 200(up5 MAX999)  約160等
	HP設定調整 	--> 原先 500(up5 MAX1000) 改為 500(up10 MAX2000) 約150等
	算上彈數和移動速度平均分攤190等 (合計500等)
	最大攻擊力(必殺技1)/最大血量傷害值為24.975%
	最大攻擊力(必殺技1)/最小血量傷害值為99.9%
	最大攻擊力/最小血量傷害值為19.98%
	最小攻擊力(必殺技1)/最大血量傷害值為5%
	最小攻擊力/最大血量傷害值為1%
	技能倍率調整 1.單發最大攻擊(5%，SP50%)  2.單條快速連發(0.5*10，SP30%)  3.齊發(2*10，SP40%)
	 */

	//彈珠及發射器
	public GameObject marble_ball;
	public GameObject marble_FireBall;
	public GameObject Fire;
	public GameObject Fire_smoke;
	public GameObject wind_from_asset;
	//音效
	public AudioClip[] this_audio_clip = new AudioClip[3];
	public AudioSource this_audio;

	//複製彈珠
	private GameObject marble_ball_ins;
	private GameObject Fire_smoke_ins;
	private GameObject wind_ins;
	//音效控制
	private bool audioIsPlaying = false;
	private bool audioIsChange = false;
	//最大彈數
	private int Bullets_Max; 
	private bool canshoot = true;
	private bool isFilling = false;
	//UI
	private UI_controller UI_script;
	//集氣
	private bool ball_strong = false;
	private bool start_count = false;
	private float buttonTime;
	private bool use_skill = true;
	//AI
	public bool AI = true;
	private int AI_Level; //AI強度
	private int ran_shoot_num; //隨機射出子彈數量
	private int ran_fill_num; //隨機在子彈剩幾發時補充
	private float ran_shoot_time; //隨機間格時間發射子彈
	private bool AI_canshoot = false; 


	// Use this for initialization
	void Start () {
		//Debug.Log (Goble_Player.playerName);
		if(Goble_Player.playerName == null)
			Goble_Player.playerName = this.name;
		//Debug.Log (Goble_Player.playerName);
		if (Goble_Player.playerName == this.name || Goble_Player.playerName == this.name)
			AI = false;

		UI_script = GameObject.Find("UI Root").GetComponent<UI_controller>();
		this_audio = GetComponent<AudioSource> ();
		this_audio.clip = this_audio_clip[0];
		this_audio.Play ();
		this_audio.loop = true;
		audioIsPlaying = true;
		Bullets_Max = Bullets_able_num;

		if (AI) 
		{
			StartCoroutine(AI_shoot());
			//隨機在子彈剩幾發時補充
			ran_fill_num = (int)Mathf.Floor (Random.Range (0, (float)Bullets_Max)); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SP >= 1)
			SP = 1;

		if (this.name == Goble_Player.playerName) {
			UI_script.change_self_value (HP, SP);
		} else {
			UI_script.change_emeny_value (HP, SP);
		}

		if(HP <= 0)
		{
			this.gameObject.SetActive (false);
		}

	}

	void FixedUpdate () 
	{
		if (!Goble_Player.gameover) {
			if (Input.GetButtonDown ("Fire1")) {
				if (this.name == Goble_Player.playerName) {
					start_count = true;
					use_skill = false;
				}
			}

			if (Input.GetButtonUp ("Fire1")) {
				if (this.name == Goble_Player.playerName) {
					shoot ("normal");
					start_count = false;
					buttonTime = 0f;
					SP += 0.05f;
					use_skill = true;
				}
			}

			if (Input.GetKeyUp (KeyCode.X) && use_skill && !isFilling) {
				if (this.name == Goble_Player.playerName)
					StartCoroutine (Filling ());
			}

			if (Input.GetKeyUp (KeyCode.F) && use_skill) {
				if (this.name == Goble_Player.playerName && SP >= 0.5f) {
					SP -= 0.5f;
					shoot ("Fire");
				}
			}
			if (Input.GetKeyUp (KeyCode.V) && use_skill) {
				if (this.name == Goble_Player.playerName && SP >= 0.4f) {
					SP -= 0.4f;
					shoot ("10_V");
				}
			}
			if (Input.GetKeyUp (KeyCode.B) && use_skill) {
				if (this.name == Goble_Player.playerName && SP >= 0.45f) {
					SP -= 0.45f;
					shoot ("10_H");
				}
			}

			if (start_count && buttonTime < 1 && canshoot && Bullets_able_num != 0) {
				buttonTime += Time.deltaTime;
				if (buttonTime >= 1 && canshoot && ball_strong == false && Bullets_able_num != 0) {
					ball_strong = true;
					start_count = false;
					//buttonTime = 0f;
					wind_ins = Instantiate (wind_from_asset, Fire.transform.position, transform.rotation)as GameObject;
				} else {
					ball_strong = false;
				}
			}

			if (ball_strong && wind_ins != null) {
				wind_ins.transform.position = Fire.transform.position;
			}

			if (AI && canshoot) {
				AI_filling ();
			}
		} else {
			audioStop ();
		}
	}
	//射擊相關
	private void shoot(string skill_name)
	{

		if (skill_name == "normal") 
		{
			if (Bullets_able_num != 0 && canshoot) 
			{
				marble_ball_ins = Instantiate (marble_ball, Fire.transform.position, transform.rotation)as GameObject;
				Fire_smoke_ins = Instantiate (Fire_smoke, Fire.transform.position, transform.rotation)as GameObject;
				marble_ball_ins.GetComponent<marble_ball> ().self_ball_attack = attack;//決定子彈威力
				marble_ball_ins.GetComponent<marble_ball> ().Skill = false;//是否為必殺彈

				if(wind_ins)
				{
					ball_strong = false;
					marble_ball_ins.GetComponent<marble_ball> ().self_ball_attack = attack*1.5f;//決定子彈威力
					wind_ins.transform.parent = marble_ball_ins.transform;
					Destroy (wind_ins,1.5f);
				}

				marble_ball_ins.transform.Translate (0, 0, shoot_speed * Time.fixedDeltaTime);
				Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), marble_ball_ins.GetComponent<Collider>());

				Bullets_able_num--;
			} 
			else 
			{
				canshoot = false;
			}
		}
		else if (skill_name == "Fire") 
		{
			marble_ball_ins = Instantiate (marble_FireBall, Fire.transform.position, transform.rotation)as GameObject;
			Fire_smoke_ins = Instantiate (Fire_smoke, Fire.transform.position, transform.rotation)as GameObject;
			marble_ball_ins.GetComponent<marble_ball> ().self_ball_attack = attack*5;//決定子彈威力
			marble_ball_ins.GetComponent<marble_ball> ().Skill = true;//是否為必殺彈
			marble_ball_ins.transform.Translate (0, 0, shoot_speed * Time.fixedDeltaTime);
			Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), marble_ball_ins.GetComponent<Collider>());
			if(wind_ins)
			{
				Destroy (wind_ins);
			}
		} 
		else if (skill_name == "10_V") 
		{
			for (int tmpNum = 0; tmpNum < 10; tmpNum++)
			{
				Vector3 tmpV3 = new Vector3 (0, 0, tmpNum * 0.21f);
				marble_ball_ins = Instantiate (marble_ball, Fire.transform.position-tmpV3, transform.rotation)as GameObject;
				marble_ball_ins.GetComponent<marble_ball> ().self_ball_attack = attack*0.5f;//決定子彈威力
				marble_ball_ins.GetComponent<marble_ball> ().Skill = false;//是否為必殺彈
				Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), marble_ball_ins.GetComponent<Collider>());
			}
			Fire_smoke_ins = Instantiate (Fire_smoke, Fire.transform.position, transform.rotation)as GameObject;
			if(wind_ins)
			{
				Destroy (wind_ins);
			}
		} 
		else if (skill_name == "10_H") 
		{
			for (int tmpNum = 0; tmpNum < 10; tmpNum++)
			{
				Vector3 tmpV3 = new Vector3 (-2.25f + tmpNum * 0.5f, Fire.transform.position.y , this.transform.position.z);
				marble_ball_ins = Instantiate (marble_ball, tmpV3, transform.rotation)as GameObject;
				marble_ball_ins.GetComponent<marble_ball> ().self_ball_attack = attack*2.0f;//決定子彈威力
				marble_ball_ins.GetComponent<marble_ball> ().Skill = false;//是否為必殺彈
				Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), marble_ball_ins.GetComponent<Collider>());
			}
			Fire_smoke_ins = Instantiate (Fire_smoke, Fire.transform.position, transform.rotation)as GameObject;
			if(wind_ins)
			{
				Destroy (wind_ins);
			}
		}
	}
	//裝填
	public IEnumerator Filling()
	{
		if (!Goble_Player.gameover)
		{
			if (Bullets_Max - Bullets_able_num > 0)
			{
				Bullets_able_num++;
				canshoot = false;
				isFilling = true;
				yield return new WaitForSeconds (0.3f);
				StartCoroutine (Filling ());
			} 
			else 
			{
				canshoot = true;
				isFilling = false;
			}
		}
	}

	//移動相關
	public void moveAudio()
	{
		if(this_audio.clip != this_audio_clip[1])
			audioIsChange = true;

		audioPlay (this_audio_clip[1]);
	}

	public void moveAudio_stop()
	{
		if(this_audio.clip != this_audio_clip[0])
			audioIsChange = true;
		
		audioPlay (this_audio_clip[0]);
	}

	//聲音相關
	private void audioPlay(AudioClip playclip)
	{
		this_audio.clip = playclip;

		if (audioIsChange) {
			this_audio.Play ();
			this_audio.loop = true;
			audioIsPlaying = true;
			audioIsChange = false;
		}
	}

	private void audioStop()
	{
		if (audioIsPlaying) {
			this_audio.Stop ();
			this_audio.loop = false;
			audioIsPlaying = false;
		}
	}

	//AI自動射擊
	private IEnumerator AI_shoot ()
	{

		if (!Goble_Player.gameover) 
		{
			if (AI_canshoot) 
			{
				//隨機射出子彈數量，若數量大於目前殘彈，則發射殘彈數
				ran_shoot_num = (int)((Mathf.Floor (Random.Range (1.0f, 4.0f)) < Bullets_able_num) ? Bullets_able_num : Mathf.Floor (Random.Range (1.0f, 4.0f)));

				shoot ("normal");
				AI_canshoot = false;
				StartCoroutine (AI_shoot ());
			} 
			else 
			{
				//隨機間格時間發射子彈
				ran_shoot_time = Random.Range (0.5f, 3.0f); 
				Debug.Log ("ran_shoot_time=" + ran_shoot_time);
				yield return new WaitForSeconds (ran_shoot_time);
				AI_canshoot = true;
				StartCoroutine (AI_shoot ());
			}
		}
		//shoot("normal");
	}
	//AI隨機彈數自動填彈
	private void AI_filling()
	{

		if (Bullets_able_num <= ran_fill_num) 
		{
			StartCoroutine(Filling());
			//隨機在子彈剩幾發時補充
			ran_fill_num = (int)Mathf.Floor (Random.Range (0, (float)Bullets_Max)); 
		}
	}
}
