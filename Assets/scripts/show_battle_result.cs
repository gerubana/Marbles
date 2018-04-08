using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int money;
    private string money_text;
	private time_script time_;
	private string mil_sec = "00";
	private string sec = "00";
	private string min = "00";
	private string hour = "00";

    private string[] error_member = {"999999", "000000"};

    private SQL_script SQL;
    private bool hasMember = false;
	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		time_ = GameObject.Find("Time").GetComponent<time_script>();
        SQL = GameObject.Find("SQL").GetComponent<SQL_script>();

        gold = int.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["total_rank"].ToString())*100;
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

        if (error_member.Contains(Goble_Player.ds_play1.Tables[0].Rows[0]["member_id"].ToString()) == false)
        {
            moneyDataGet();
            int new_money = money + gold;
            sendMoneyToDatabase(new_money.ToString());
            hasMember = true;
        }
        else
            hasMember = false;

	}

    #region $$相關
    private void moneyDataGet()
    {
        //若是抓顯示的金額格式，需要以下轉成普通字串

        string tmp_string = "";
        /*string[] Split_money = total_money.Split(',');

        foreach (string i in Split_money)
            tmp_string += i;
        */

        //資料庫抓會員的錢
        tmp_string = SQL.get_and_set_money("get", Goble_Player.ds_play1.Tables[0].Rows[0]["member_id"].ToString(), null);
        //無的話直接轉數字即可
        money = int.Parse(tmp_string);

        //Debug.Log(tmp_string);
    }

    private void sendMoneyToDatabase(string new_money)
    {
        string tmp_string = "";
        tmp_string = SQL.get_and_set_money("set", Goble_Player.ds_play1.Tables[0].Rows[0]["member_id"].ToString(), new_money);
        //Debug.Log(tmp_string);
    }
    #endregion

	
	// Update is called once per frame
	void Update () {
		showText ();
	}

	void showText()
	{
		if (time < time_.tmp_time) {
			time += Time.deltaTime*50;
			if (time >= time_.tmp_time)
				time = time_.tmp_time;
		}

		mil_sec = ((int)((time - (int)time) * 100) < 10) ? "0" + ((int)((time - (int)time) * 100)).ToString () :((int)((time - (int)time) * 100)).ToString ();
		sec = ((int)(time%60)<10)?"0"+((int)(time%60)).ToString():((int)(time%60)).ToString();
		min = ((int)((time/60)%60)<10)?"0"+((int)((time/60)%60)).ToString():((int)((time/60)%60)).ToString();
		hour = ((int)((int)(time/60)/60)<10)?"0"+((int)((int)(time/60)/60)).ToString():((int)((int)(time/60)/60)).ToString();

		time_text = hour+":"+min+":"+sec+":"+mil_sec;


		if (gold_tmp < gold) {
			gold_tmp += 100;
			if (gold_tmp >= gold) {
				gold_tmp = gold;
			}
		}

        if (hasMember)
        {
            gold_text = gold_tmp.ToString();
            money_text = (money + gold_tmp).ToString();
        }
        else
        {
            gold_text = "此帳號無法獲得金錢";
            money_text = "此帳號無法獲得金錢";
        }


        countWord = "        花費時間 ： "+ time_text +" \n\n        獲得金錢 ： "+ gold_text +" \n\n        累計金錢 ： " + money_text;
		count.gameObject.GetComponent<UILabel> ().text = countWord;
	}
}
