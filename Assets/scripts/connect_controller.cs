using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class connect_controller : MonoBehaviour {

    private SQL_script SQL;
    private System.Data.DataTable dt;
    private string[] error_member = {"999999", "000000"};

	// Use this for initialization
	void Start () {
        SQL = GameObject.Find("SQL").GetComponent<SQL_script>();

        if (Goble_Player.member_no == null || Goble_Player.member_no == "")
            Goble_Player.member_no = "000000";

        Goble_Player.ds_play1 = SQL.get_machine_data(Goble_Player.member_no, null, null, "Y");
        Goble_Player.ds_play2 = SQL.get_machine_data("000000", Goble_Player.AI_rank.ToString(), Goble_Player.AI_machine_no, "Y");

        //Debug.Log("1="+Goble_Player.ds_play1.Tables[0].Rows[0]["machine_type"].ToString());
        //Debug.Log("2="+Goble_Player.ds_play2.Tables[0].Rows[0]["attack"].ToString());

        Debug.Log("member_no="+Goble_Player.member_no);
        Debug.Log("machine_no="+Goble_Player.machine_no);
        Debug.Log("AI_rank="+Goble_Player.AI_rank);
        Debug.Log("AI_machine_no="+Goble_Player.AI_machine_no);

        Goble_Player.playerName = Goble_Player.ds_play1.Tables[1].Rows[0]["nickname"].ToString();
        GameObject.Find("1P/name").GetComponent<UILabel>().text = Goble_Player.ds_play1.Tables[1].Rows[0]["nickname"].ToString();
        GameObject.Find("1P/Rank").GetComponent<UILabel>().text = "Rank" + Goble_Player.ds_play1.Tables[0].Rows[0]["total_rank"].ToString();

        if (Goble_Player.ds_play2.Tables[0].Rows[0]["AI"].ToString() == "Y")
        {
            Goble_Player.player2Name = null;
            GameObject.Find("2P/name").GetComponent<UILabel>().text = "AI(AI)";
            GameObject.Find("2P/Rank").GetComponent<UILabel>().text = "Rank" + Goble_Player.AI_rank.ToString();
        }
        else
        {
            Goble_Player.player2Name = Goble_Player.ds_play2.Tables[1].Rows[0]["nickname"].ToString();
            GameObject.Find("1P/name").GetComponent<UILabel>().text = Goble_Player.ds_play2.Tables[1].Rows[0]["nickname"].ToString();
            GameObject.Find("1P/Rank").GetComponent<UILabel>().text = "Rank" + Goble_Player.ds_play2.Tables[0].Rows[0]["total_rank"].ToString();
        }

	}

    public void chech_ok()
    {
        Globe.loadName = "game";
        Application.LoadLevel ("Loading");
    }

    public void backToHome()
    {
        if(error_member.Contains(Goble_Player.ds_play1.Tables[0].Rows[0]["member_id"].ToString()))
        {
            Goble_Player.member_no = null;
            Goble_Player.machine_no = "";
        }
        Goble_Player.AI_rank = 0;
        Goble_Player.machine_no = "";

        Goble_Player.ds_play1 = new System.Data.DataSet();
        Goble_Player.ds_play2 = new System.Data.DataSet();

        Goble_Player.playerName = "";
        Goble_Player.player2Name = "";

        Goble_Player.gameStart = false;
        Goble_Player.gameover = false;

        Globe.loadName = "Main";
        Application.LoadLevel ("Loading");
    }
}
