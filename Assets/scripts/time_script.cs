using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time_script : MonoBehaviour {

	public bool time_stop = false;
	public bool time_recorded = false;
	public bool gameStart = false;
	public string time_final = "";

	private GameObject Player_obj;
	private float tmp_time = 0;
	private float now_time = 0;
	private string mil_sec = "00";
	private string sec = "00";
	private string min = "00";
	private string hour = "00";

	private float Ready_time = 0;
	private string num = "3";

	// Use this for initialization
	void Start () {
		this.GetComponent<UILabel>().text = "00:10:00:00";
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStart && !time_recorded) {
			if (!time_stop) {
				start_timing ();
			} else {
				stop_timing ();
			}
		}

		Ready_time += Time.deltaTime;
		if (this.name == "Ready") {
			if (Ready_time <= 1) {
				num = "3";
			} else if (Ready_time <= 2) {
				num = "2";
			} else {
				num = "1";
			}

			Debug.Log (Ready_time);
			Debug.Log (num);
			this.GetComponent<UILabel> ().text = num;
		}
	}

	public void start_timing(){
		tmp_time += Time.deltaTime;
		now_time = 600-tmp_time;

		mil_sec = ((int)((now_time - (int)now_time) * 100) < 10) ? "0" + ((int)((now_time - (int)now_time) * 100)).ToString () :((int)((now_time - (int)now_time) * 100)).ToString ();
		sec = ((int)(now_time%60)<10)?"0"+((int)(now_time%60)).ToString():((int)(now_time%60)).ToString();
		min = ((int)((now_time/60)%60)<10)?"0"+((int)((now_time/60)%60)).ToString():((int)((now_time/60)%60)).ToString();
		hour = ((int)((int)(now_time/60)/60)<10)?"0"+((int)((int)(now_time/60)/60)).ToString():((int)((int)(now_time/60)/60)).ToString();

		this.GetComponent<UILabel>().text = hour+":"+min+":"+sec+":"+mil_sec;
	}

	public void stop_timing(){
		time_final = hour+"小時"+min+"分"+sec+"秒"+mil_sec;
	}

	public void reset_timing(){
		time_stop = false;
		gameStart = false;
		now_time = 0;
		this.GetComponent<UILabel>().text = "00:00:00:00";
	}

}
