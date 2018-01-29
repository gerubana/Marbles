using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour {
	//基本數值
	public float shoot_speed; 
	public float moveSpeed; 
	public int Bullets_able_num = 15; 
	//彈珠及發射器
	public GameObject marble_ball;
	public GameObject Fire;
	public GameObject Fire_smoke;
	//音效
	public AudioClip[] this_audio_clip = new AudioClip[5];
	public AudioSource this_audio;

	//複製彈珠
	private GameObject marble_ball_ins;
	private GameObject Fire_smoke_ins;
	//音效控制
	private bool audioIsPlaying = false;
	private bool audioIsChange = false;
	//最大彈數
	private int Bullets_Max; 
	private bool canshoot = true;

	// Use this for initialization
	void Start () {
		//Debug.Log (Goble_Player.playerName);
		if(Goble_Player.playerName == null)
			Goble_Player.playerName = this.name;
		//Debug.Log (Goble_Player.playerName);

		this_audio = GetComponent<AudioSource> ();
		this_audio.clip = this_audio_clip[0];
		this_audio.Play ();
		this_audio.loop = true;
		audioIsPlaying = true;
		Bullets_Max = Bullets_able_num;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider object_)
	{
		if (object_.tag == "ball") {
			object_.GetComponent<marble_ball> ().explosion();
			Destroy (object_.gameObject);
		}
	}

	void FixedUpdate () 
	{
		if(Input.GetButtonDown("Fire1"))
		{
			if(this.name == Goble_Player.playerName)
				shoot ();
		}

		if (Input.GetKeyUp(KeyCode.X)) 
		{
			//InvokeRepeating("Filling",0.2f, 0.2f );
			StartCoroutine(Filling());
		}
	}
	//射擊相關
	private void shoot()
	{
		if (Bullets_able_num != 0 && canshoot) {
			marble_ball_ins = Instantiate (marble_ball, Fire.transform.position, transform.rotation)as GameObject;
			Fire_smoke_ins = Instantiate (Fire_smoke, Fire.transform.position, transform.rotation)as GameObject;
			marble_ball_ins.transform.Translate (0, 0, shoot_speed * Time.fixedDeltaTime);
			Bullets_able_num--;
		} else {
			canshoot = false;
		}
	}
	//裝填
	public IEnumerator Filling()
	{
		Debug.Log ("@@");
		if (Bullets_Max - Bullets_able_num > 0 )
		{
			Bullets_able_num++;
			canshoot = false;
		} else {
			canshoot = true;
		}
		yield return new WaitForSeconds (0.2f);
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

}
