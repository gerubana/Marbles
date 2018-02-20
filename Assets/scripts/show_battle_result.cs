using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_battle_result : MonoBehaviour {

	public GameObject Win;
	public GameObject Lose;
	public GameObject Draw;
	public GameObject count;
	public string battleResult;

	private string countWord;
	private int gold;
	private int gold_tmp=0;
	private string gold_text;
	private float time;
	private string time_text;
	private time_script time_;
	private string mil_sec = "00";
	private string sec = "00";
	private string min = "00";
	private string hour = "00";

	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		time_ = GameObject.Find("Time").GetComponent<time_script>();

		gold = 1000;
		if (battleResult == "Win") {
			if (time_.now_time <= 60.0f) {
				gold = (int)(gold * 3f);
			} else if (time_.now_time <= 180.0f) {
				gold = (int)(gold * 2f);
			} else if (time_.now_time <= 300.0f) {
				gold = (int)(gold * 1.5f);
			}
		} else if (battleResult == "Lose") {
			gold = (int)(gold/2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		showText ();
	}

	void showText()
	{
		if (time < time_.tmp_time) {
			time += Time.deltaTime*5;
			if (time >= time_.tmp_time)
				time = time_.tmp_time;
		}

		mil_sec = ((int)((time - (int)time) * 100) < 10) ? "0" + ((int)((time - (int)time) * 100)).ToString () :((int)((time - (int)time) * 100)).ToString ();
		sec = ((int)(time%60)<10)?"0"+((int)(time%60)).ToString():((int)(time%60)).ToString();
		min = ((int)((time/60)%60)<10)?"0"+((int)((time/60)%60)).ToString():((int)((time/60)%60)).ToString();
		hour = ((int)((int)(time/60)/60)<10)?"0"+((int)((int)(time/60)/60)).ToString():((int)((int)(time/60)/60)).ToString();

		time_text = hour+":"+min+":"+sec+":"+mil_sec;


		if (gold_tmp < gold) {
			gold_tmp += 10;
			if (gold_tmp >= gold) {
				gold_tmp = gold;
			}
		}

		gold_text = gold_tmp.ToString ();

		countWord = "        花費時間 ： "+ time_text +" \n\n        獲得金錢 ： "+ gold_text +" \n\n        累計金錢 ：";
		count.gameObject.GetComponent<UILabel> ().text = countWord;
	}
}
