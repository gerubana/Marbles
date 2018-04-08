using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class game_controller : MonoBehaviour {

    public GameObject[] player_list;
    public GameObject[] player_pos;

    private SQL_script SQL;
    private GameObject player_ins;

    private int player_machine_no;
    private string[] error_member = {"999999", "000000"};

	// Use this for initialization
    void Awake () {
        SQL = GameObject.Find("SQL").GetComponent<SQL_script>();

        //Player1
        player_machine_no = int.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["machine_type"].ToString());
        player_ins = Instantiate(player_list[player_machine_no], player_pos[0].transform.position, player_pos[0].transform.rotation)as GameObject;
        player_ins.GetComponent<body>().AI = false;
        player_ins.name = Goble_Player.ds_play1.Tables[1].Rows[0]["nickname"].ToString();
        //player_ins.GetComponent<body>().attack = 2500.0f;
        /*Debug.Log("member = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["member_id"].ToString());
        Debug.Log("attack = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["attack"].ToString());
        Debug.Log("HP = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["HP"].ToString());
        Debug.Log("move_speed = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["move_speed"].ToString());
        Debug.Log("shoot_speed = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["shoot_speed"].ToString());
        Debug.Log("filling_speed = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["filling_speed"].ToString());
        Debug.Log("max_marble_ball = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["max_marble_ball"].ToString());
        Debug.Log("skill1 = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["skill1"].ToString());
        Debug.Log("skill2 = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["skill2"].ToString());
        Debug.Log("skill3 = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["skill3"].ToString());
        Debug.Log("aim = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["aim"].ToString());
        Debug.Log("move_range = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["move_range"].ToString());
        Debug.Log("AI = "+ Goble_Player.ds_play1.Tables[0].Rows[0]["AI"].ToString());*/

        player_ins.GetComponent<body>().attack = float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["attack"].ToString());
        player_ins.GetComponent<body>().Max_hp = float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["HP"].ToString());
        player_ins.GetComponent<body>().moveSpeed = float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["move_speed"].ToString());
        player_ins.GetComponent<body>().shootSpeed = float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["shoot_speed"].ToString());
        player_ins.GetComponent<body>().fillSpeed = float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["filling_speed"].ToString());
        player_ins.GetComponent<body>().Bullets_able_num = (int)float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["max_marble_ball"].ToString());
        player_ins.GetComponent<body>().skill_list[0] = (Goble_Player.ds_play1.Tables[0].Rows[0]["skill1"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().skill_list[1] = (Goble_Player.ds_play1.Tables[0].Rows[0]["skill2"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().skill_list[2] = (Goble_Player.ds_play1.Tables[0].Rows[0]["skill3"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().aim = (Goble_Player.ds_play1.Tables[0].Rows[0]["aim"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().moveRange = float.Parse(Goble_Player.ds_play1.Tables[0].Rows[0]["move_range"].ToString());

        GameObject.Find("play1_name").GetComponent<UILabel>().text = player_ins.name;



        //Player2
        player_machine_no = int.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["machine_type"].ToString());
        player_ins = Instantiate(player_list[player_machine_no], player_pos[1].transform.position, player_pos[1].transform.rotation)as GameObject;
        if (Goble_Player.ds_play2.Tables[0].Rows[0]["AI"].ToString() == "Y")
        {
            player_ins.GetComponent<body>().AI = true;
            player_ins.name = "AI(AI)";
        }
        else
        {
            player_ins.GetComponent<body>().AI = false;
            player_ins.name = Goble_Player.ds_play2.Tables[0].Rows[0]["nickname"].ToString();
        }

        player_ins.GetComponent<body>().attack = float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["attack"].ToString());
        player_ins.GetComponent<body>().Max_hp = float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["HP"].ToString());
        player_ins.GetComponent<body>().moveSpeed = float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["move_speed"].ToString());
        player_ins.GetComponent<body>().shootSpeed = float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["shoot_speed"].ToString());
        player_ins.GetComponent<body>().fillSpeed = float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["filling_speed"].ToString());
        player_ins.GetComponent<body>().Bullets_able_num = (int)float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["max_marble_ball"].ToString());
        player_ins.GetComponent<body>().skill_list[0] = (Goble_Player.ds_play2.Tables[0].Rows[0]["skill1"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().skill_list[1] = (Goble_Player.ds_play2.Tables[0].Rows[0]["skill2"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().skill_list[2] = (Goble_Player.ds_play2.Tables[0].Rows[0]["skill3"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().aim = (Goble_Player.ds_play2.Tables[0].Rows[0]["aim"].ToString() == "Y") ? true : false;
        player_ins.GetComponent<body>().moveRange = float.Parse(Goble_Player.ds_play2.Tables[0].Rows[0]["move_range"].ToString());

        GameObject.Find("play2_name").GetComponent<UILabel>().text = player_ins.name;
	}
	
    public void BackToHome()
    {
        Globe.loadName = "Main";
        Application.LoadLevel ("Loading");

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
    }
}
