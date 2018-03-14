using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hangar_controller : MonoBehaviour {

    public GameObject[] machine; 
    public GameObject attack_add_val;
    public GameObject HP_add_val;
    public GameObject moveSpeed_add_val;
    public GameObject shootSpeed_add_val;
    public GameObject fillingSpeed_add_val;
    public GameObject max_marble_ball_add_val;

    //計算數值資料
    private float attack_val;
    private float HP_val;
    private float moveSpeed_val;
    private float shootSpeed_val;
    private float fillingSpeed_val;
    private int max_marble_ball_val;
    //計算等級資料
    private int attack_rank;
    private int HP_rank;
    private int moveSpeed_rank;
    private int shootSpeed_rank;
    private int fillingSpeed_rank;
    private int max_marble_ball_rank;

    //顯示資料
    private string attack_state;
    private string HP_state;
    private string moveSpeed_state;
    private string shootSpeed_state;
    private string fillingSpeed_state;
    private string max_marble_ball_state;
    private string total_rank; //Mathf.Floor((attack_rank + HP_rank + moveSpeed_rank)/3).ToString();


    private int now_page;

	// Use this for initialization
	void Start () {
        if (Goble_Player.member_no == null || Goble_Player.member_no == "")
        {
            Debug.Log("未登入，請回首頁");
        }
        else
        {
            Debug.Log("member : " + Goble_Player.member_no);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void machine_move(Vector3 start_pos, Vector3 goal_pos, float move_speed)
    {

    }

    /*private void click_button(string dir)
    {
        if(dir == "Right")
            //
        else
            //
         
        
    }*/

    public void attack_add_change(string action)
    {
        add_input_change(action, attack_add_val, attack_rank, 100);
    }

    public void HP_add_change(string action)
    {
        add_input_change(action, HP_add_val, HP_rank, 100);
    }

    public void MS_add_change(string action)
    {
        add_input_change(action, moveSpeed_add_val, moveSpeed_rank, 100);
    }

    public void SS_add_change(string action)
    {
        add_input_change(action, shootSpeed_add_val, shootSpeed_rank, 10);
    }

    public void FS_add_change(string action)
    {
        add_input_change(action, fillingSpeed_add_val, fillingSpeed_rank, 10);
    }

    public void MMB_add_change(string action)
    {
        add_input_change(action, max_marble_ball_add_val, max_marble_ball_rank, 10);
    }

    public void input_change(GameObject val)
    {
        bool tmp;

        try
        {
            int.Parse(val.GetComponent<UIInput>().value);
            tmp = true;

            if(val.GetComponent<UIInput>().value == "")
                tmp = false;
        }
        catch
        {
            tmp = false;
        }

        if (tmp)
        {
            switch (val.name)
            {
                case "attack_add":
                    attack_add_change("input");
                    return;
                case "HP_add":
                    HP_add_change("input");
                    return;
                case "moveSpeed_add":
                    MS_add_change("input");
                    return;
                case "shootSpeed_add":
                    SS_add_change("input");
                    return;
                case "fillingSpeed_add":
                    FS_add_change("input");
                    return;
                case "max_marble_ball_add":
                    MMB_add_change("input");
                    return;
            }
        }
    }

    private void add_input_change(string action, GameObject val, int rank ,int max_rank)
    {
        int tmp_val = int.Parse(val.GetComponent<UIInput>().value);
        int max_val = max_rank - rank;

        if (action == "add" && tmp_val < max_val)
        {
            tmp_val++;
            val.GetComponent<UIInput>().value = tmp_val.ToString();
        }
        else if (action == "minus" && tmp_val > 0)
        {
            tmp_val--;
            val.GetComponent<UIInput>().value = tmp_val.ToString();
        }
        else if (action == "input" && tmp_val >= max_val)
        {
            val.GetComponent<UIInput>().value = max_val.ToString();
        }
        else if (action == "input" && tmp_val <= 0)
        {
            val.GetComponent<UIInput>().value = "0";
        }
    }
}
