using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_battle_result : MonoBehaviour {

	public GameObject Win;
	public GameObject Lose;
	public GameObject Draw;
	public GameObject count;

	private string countWord;
	private string time;
	private time_script time_;

	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		time_ = GameObject.Find("Time").GetComponent<time_script>();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void showText()
	{
		time = time_.now_time.ToString();

		countWord = "        花費時間 ： "+ time +" \n\n        獲得金錢 ：\n\n        累計金錢 ：";
	}
}
