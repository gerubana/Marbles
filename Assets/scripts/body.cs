using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour {
	//基本數值
	public float shoot_speed; 
	public float moveSpeed; 
	//彈珠及發射器
	public GameObject marble_ball;
	public GameObject Fire;
	//音效
	public AudioClip[] this_audio_clip = new AudioClip[5];
	public AudioSource this_audio;
	//複製彈珠
	private GameObject marble_ball_ins;

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

	void FixedUpdate () 
	{
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

	public void moveAudio()
	{
		this_audio.clip = this_audio_clip[1];
		this_audio.Play ();
		this_audio.loop = true;
	}

	public void moveAudio_stop()
	{
		this_audio.clip = this_audio_clip[0];
		this_audio.Play ();
		this_audio.loop = true;
	}
}
