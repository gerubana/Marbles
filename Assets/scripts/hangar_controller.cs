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
    public GameObject money_show;
    public GameObject pay_money_total;

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

    //各項數值花費金額暫存
    private int attack_pay;
    private int HP_pay;
    private int moveSpeed_pay;
    private int shootSpeed_pay;
    private int fillingSpeed_pay;
    private int max_marble_ball_pay;
    private int S1_pay;
    private int S2_pay;
    private int S3_pay;
    private int aim_pay;
    private int[] ball_pay;

    //暫存
    private int now_page;
    private int tmp_monay;
    private int pay_money=0;

    SQL_script SQL;

	// Use this for initialization
    void Start () 
    {
        SQL = GameObject.Find ("SQL").GetComponent<SQL_script> ();
        if (Goble_Player.member_no == null || Goble_Player.member_no == "")
        {
            Debug.Log("未登入，請回首頁");
        }
        else
        {
            Debug.Log("member : " + Goble_Player.member_no);
        }
        moneyDataGet();
	}
	
	// Update is called once per frame
	void Update () 
    {
        moneyDataShow();
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

    #region 改變$$
    private void moneyDataGet()
    {
        //若是抓顯示的金額格式，需要以下轉成普通字串

        string tmp_string = "";
        /*string[] Split_money = total_money.Split(',');

        foreach (string i in Split_money)
            tmp_string += i;
        */

        //資料庫抓會員的錢
        tmp_string = SQL.get_and_set_money("get", "000001", null);
        //無的話直接轉數字即可
        tmp_monay = int.Parse(tmp_string);

        Debug.Log(tmp_string);
    }

    /* note
        type 1為攻擊、HP、速度，比例採用 技能升級 1~10,11~20,21~30 ….> 100,200,300….. 每一項總需共55,000 
        type 2為彈數、填彈速度、射擊速度，升一級3000 每一項總需共30,000
        type 3為技能解鎖，每一項需 50,000
        type 4為瞄準器解鎖，需 10,000
        type 5為特殊彈，每一項需 100,000
    */
    private int moneyCount(int type, int now_rank, int add_rank)
    {
        int tmp_money = 0;
        if (type == 1)
        {
            int goal_rank = add_rank + now_rank;
            int tmp_now_rank = now_rank;
            int tmp_add_rank = add_rank;

            if (now_rank <= 0)
                now_rank = 1;
            
            int tmp_min = Mathf.CeilToInt(now_rank / 10.0f); //*這裡不用浮點數的話，會預設double會算不出值，只會是0
            int tmp_max = Mathf.CeilToInt(goal_rank / 10.0f);
           
            if (tmp_min != tmp_max)
            {
                for(int i = tmp_min; i <= tmp_max; i++){
                    int tmp_num = i;
                    int pay = tmp_num * 100;
                    int rank_range = i * 10;
                    int rank_gap = rank_range - tmp_now_rank;

                    if (tmp_add_rank > rank_gap)
                    {
                        tmp_money += (rank_gap * pay);
                        tmp_add_rank -= rank_gap;
                        tmp_now_rank = rank_range;
                    }
                    else
                    {
                        tmp_money += (tmp_add_rank * pay);
                    }
                }
            }
            else
            {
                int tmp_num = tmp_min;
                int pay = tmp_num * 100;
                tmp_money += (add_rank * pay);
            }
        }
        else if(type == 2)
        {
            tmp_money += (add_rank * 3000);
        }
        else
        {
            switch (type)
            {
                case 3:
                    tmp_money += 50000;
                    break;
                case 4:
                    tmp_money += 10000;
                    break;
                case 5:
                    tmp_money += 100000;
                    break;
            }
        }

        Debug.Log(tmp_money);
        return tmp_money;
    }

    private void moneyTotalCount()
    {
        pay_money = attack_pay + HP_pay + moveSpeed_pay + shootSpeed_pay + fillingSpeed_pay + max_marble_ball_pay + S1_pay + S2_pay + S3_pay + aim_pay;
        if(ball_pay != null){
            foreach (int i in ball_pay)
                pay_money += i;
        }
        Debug.Log(pay_money);
    }

    private void moneyDataShow()
    {
        money_show.GetComponent<UILabel>().text = tmp_monay.ToString("C0").Replace("$","");
        if (pay_money > 0)
        {
            pay_money_total.SetActive(true);
            pay_money_total.GetComponent<UILabel>().text = "(-" + pay_money.ToString("C0").Replace("$","") + ")";
        }
        else
        {
            pay_money_total.SetActive(false);
        }
    }
    #endregion

    #region 提升技能區 
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
                    break;
                case "HP_add":
                    HP_add_change("input");
                    break;
                case "moveSpeed_add":
                    MS_add_change("input");
                    break;
                case "shootSpeed_add":
                    SS_add_change("input");
                    break;
                case "fillingSpeed_add":
                    FS_add_change("input");
                    break;
                case "max_marble_ball_add":
                    MMB_add_change("input");
                    break;
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

        //計算金額，因為目前無論點按加或減，都會影響數值，只要數值變動會再跑一次這隻，所以決定計算金額只抓數值變動，也就是action == "input"，防止重複計算
        if (action == "input")
        {
            int result;

            tmp_val = int.Parse(val.GetComponent<UIInput>().value);
            //int tmp_rank = rank + tmp_val;
            //Debug.Log("max_rank:"+max_rank+" rank:"+tmp_rank+" tmp_val:"+tmp_val);
            if (max_rank == 100)
            {
                result = moneyCount(1, rank, tmp_val);
            }
            else
            {
                result = moneyCount(2, rank, tmp_val);
            }

            switch (val.name)
            {
                case "attack_add":
                    attack_pay = result;
                    break;
                case "HP_add":
                    HP_pay = result;
                    break;
                case "moveSpeed_add":
                    moveSpeed_pay = result;
                    break;
                case "shootSpeed_add":
                    shootSpeed_pay = result;
                    break;
                case "fillingSpeed_add":
                    fillingSpeed_pay = result;
                    break;
                case "max_marble_ball_add":
                    max_marble_ball_pay = result;
                    break;
            }
            moneyTotalCount();
        }
    }
    #endregion

    public void data_reset()
    {
        pay_money = 0;
    }
}
