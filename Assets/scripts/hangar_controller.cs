using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hangar_controller : MonoBehaviour {
    //抓取顯示數值資料
    public GameObject[] machine; 
    public GameObject attack_add_val;
    public GameObject HP_add_val;
    public GameObject moveSpeed_add_val;
    public GameObject shootSpeed_add_val;
    public GameObject fillingSpeed_add_val;
    public GameObject max_marble_ball_add_val;
    public GameObject money_show;
    public GameObject pay_money_total;
    public GameObject[] skill1; 
    public GameObject[] skill2; 
    public GameObject[] skill3; 
    public GameObject[] aim;
    public GameObject[] isuse;
    public GameObject prev;
    public GameObject next;

    //抓取顯示數值資料
    /*private float attack_val;
    private float HP_val;
    private float moveSpeed_val;
    private float shootSpeed_val;
    private float fillingSpeed_val;
    private int max_marble_ball_val;*/

    #region 計算等級資料    一開始要先抓資料存至此
    private int attack_rank;
    private int HP_rank;
    private int moveSpeed_rank;
    private int shootSpeed_rank;
    private int fillingSpeed_rank;
    private int max_marble_ball_rank;
    private int total_rank; 

    private float attack_val;
    private float HP_val;
    private float moveSpeed_val;
    private float shootSpeed_val;
    private float fillingSpeed_val;
    private float max_marble_ball_val;//存回去要轉int(無條件捨去)

    //升級加成數
    private float attack_add_rank_val;
    private float HP_add_rank_val;
    private float moveSpeed_add_rank_val;
    private float shootSpeed_add_rank_val;
    private float fillingSpeed_add_rank_val;
    private float max_marble_ball_add_rank_val;
    #endregion

    #region 上傳用
    private int new_attack_rank;
    private int new_HP_rank;
    private int new_moveSpeed_rank;
    private int new_shootSpeed_rank;
    private int new_fillingSpeed_rank;
    private int new_max_marble_ball_rank;
    private int new_total_rank; 

    private float new_attack_val;
    private float new_HP_val;
    private float new_moveSpeed_val;
    private float new_shootSpeed_val;
    private float new_fillingSpeed_val;
    private float new_max_marble_ball_val;

    private bool tmp_skill1_state;
    private bool tmp_skill2_state;
    private bool tmp_skill3_state;
    private bool tmp_aim_state;
    #endregion

    //顯示資料
    private string attack_state;
    private string HP_state;
    private string moveSpeed_state;
    private string shootSpeed_state;
    private string fillingSpeed_state;
    private string max_marble_ball_state;
    private string total_rank_state;
    private bool skill1_state;
    private bool skill2_state;
    private bool skill3_state;
    private bool aim_state;

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
    private int isuse_tmp;
    private int machine_count;
    private bool is_change = false;
    private bool move_right = false;
    private float changeSpeed = 0.1f;
    private Vector3[] movePos = new Vector3[3];
    private List<int> lockMachineNum;

    SQL_script SQL;
    private System.Data.DataSet ds = new System.Data.DataSet();

	// Use this for initialization
    void Start () 
    {
        //判斷會員
        if (Goble_Player.member_no == null || Goble_Player.member_no == "")
        {
            Debug.Log("未登入，請回首頁");
            //return;
        }
        else
        {
            Debug.Log("member : " + Goble_Player.member_no);
        }

        //抓取頁面資料
        SQL = GameObject.Find ("SQL").GetComponent<SQL_script> ();
        getMachinAllData();
        moneyDataGet();

        machine_count = ds.Tables[0].Rows.Count;
        //抓取使用中機體=頁數，預設頁面先顯示出這台
        for (int i = 0; i < machine_count; i++)
        {
            if (ds.Tables[0].Rows[i]["isUse"].ToString() == "Y")
                now_page = int.Parse(ds.Tables[0].Rows[i]["machine_type"].ToString());
        }
        Debug.Log(now_page);
        isuse_tmp = now_page;
        isuse[0].SetActive(false);
        isuse[1].SetActive(true);

        machine[now_page].SetActive(true);

        //儲存機體資料，跑完會自動跑升級用計算加成數的比例，需要有頁數才能用
        setMachinDefaultData();

        movePos[0] = new Vector3(2.0f, 0.15f, -0.7f);
        movePos[1] = new Vector3(0f, 0.15f, -0.7f);
        movePos[2] = new Vector3(-2.0f, 0.15f, -0.7f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        moneyDataShow();
        total_rank = (int)Mathf.Floor((attack_rank + HP_rank + moveSpeed_rank) / 3);
        GameObject.Find("Rank").GetComponent<UILabel>().text = "Code:"+ now_page.ToString("00") +"   Rank " + total_rank.ToString();

        if (now_page == isuse_tmp)
        {
            isuse[0].SetActive(false);
            isuse[1].SetActive(true);
        }
        else
        {
            isuse[0].SetActive(true);
            isuse[1].SetActive(false);
        }

        if (now_page == 0)
            prev.SetActive(false);
        else if (now_page == machine_count-1)
            next.SetActive(false);
        else
        {
            prev.SetActive(true);
            next.SetActive(true);
        }

        //機體自轉
        machine_rotation(machine[now_page]);

        //切換機體
        if (is_change)
        {
            if (move_right)
            {
                machine_move(machine[now_page], movePos[1], changeSpeed);
                machine_move(machine[now_page - 1], movePos[2], changeSpeed);
            }
            else
            {
                machine_move(machine[now_page], movePos[1], changeSpeed);
                machine_move(machine[now_page + 1], movePos[0], changeSpeed);
            }
        }
    }

    private void machine_rotation(GameObject machine_obj)
    {
        if(!is_change)
            machine_obj.transform.Rotate(new Vector3(0f, 1.0f, 0f));
    }

    private void machine_move(GameObject machine_obj, Vector3 goal_pos, float move_speed)
    {
        if (machine_obj.transform.position != goal_pos)
        {
            machine_obj.transform.position = Vector3.Lerp(machine_obj.transform.position, goal_pos, move_speed);
        }
        else
        {
            machine_obj.transform.rotation = Quaternion.identity;
            if (machine_obj.transform.position != movePos[1])
                machine_obj.SetActive(false);

            is_change = false;
        }
    }

    public void change_machine(string dir)
    {
        if(!is_change)
        {
            if (dir == "Right")
            {
                move_right = true;
                now_page++;
                machine[now_page].SetActive(true);
                machine[now_page].transform.position = movePos[0];
                machine[now_page-1].transform.position = movePos[1];
            }   
            else if (dir == "Left")
            {
                move_right = false;
                now_page--;
                machine[now_page].SetActive(true);
                machine[now_page].transform.position = movePos[2];
                machine[now_page+1].transform.position = movePos[1];
            }

            is_change = true;
            setMachinDefaultData();//重新載入機體資料
            data_reset();//資料清除
        }
        
    }

    private void getMachinAllData()
    {
        ds = SQL.get_machine_data("000001"/*Goble_Player.member_no*/, null, null, null);

        //存入鎖住的機體清單
        lockMachineNum = new List<int>();
        for (int i = 0; i < ds.Tables [0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["isLock"].ToString() == "Y")
                lockMachineNum.Add(int.Parse(ds.Tables[0].Rows[i]["machine_type"].ToString()));
        }
        Debug.Log(lockMachineNum[0]);
    }
    #region 讀出機體資料，預設時及改變機體時使用
    private void setMachinDefaultData()
    {
        int tmp_i = 1;

        for (int i = 0; i < ds.Tables [0].Rows.Count; i++)
        {
            if ( int.Parse(ds.Tables[0].Rows[i]["machine_type"].ToString()) == now_page)
                tmp_i = i;
        }

        attack_val = float.Parse(ds.Tables [0].Rows[tmp_i]["attack"].ToString());
        attack_rank = int.Parse(ds.Tables [0].Rows[tmp_i]["attack_rank"].ToString());
        HP_val = float.Parse(ds.Tables [0].Rows[tmp_i]["HP"].ToString());
        HP_rank = int.Parse(ds.Tables [0].Rows[tmp_i]["HP_rank"].ToString());
        moveSpeed_val = float.Parse(ds.Tables [0].Rows[tmp_i]["move_speed"].ToString());
        moveSpeed_rank = int.Parse(ds.Tables [0].Rows[tmp_i]["move_speed_rank"].ToString());
        shootSpeed_val = float.Parse(ds.Tables [0].Rows[tmp_i]["shoot_speed"].ToString());
        shootSpeed_rank = int.Parse(ds.Tables [0].Rows[tmp_i]["shoot_speed_rank"].ToString());
        fillingSpeed_val = float.Parse(ds.Tables [0].Rows[tmp_i]["filling_speed"].ToString());
        fillingSpeed_rank = int.Parse(ds.Tables [0].Rows[tmp_i]["filling_speed_rank"].ToString());
        max_marble_ball_val = float.Parse(ds.Tables [0].Rows[tmp_i]["max_marble_ball"].ToString());
        max_marble_ball_rank = int.Parse(ds.Tables [0].Rows[tmp_i]["max_marble_ball_rank"].ToString());

        skill1_state = (ds.Tables[0].Rows[tmp_i]["skill1"].ToString()=="Y")? true:false;
        skill2_state = (ds.Tables[0].Rows[tmp_i]["skill2"].ToString()=="Y")? true:false;
        skill3_state = (ds.Tables[0].Rows[tmp_i]["skill3"].ToString()=="Y")? true:false;
        aim_state = (ds.Tables[0].Rows[tmp_i]["aim"].ToString()=="Y")? true:false;

        /*Debug.Log("attack:"+attack_val+","+attack_rank);
        Debug.Log("HP:"+HP_val+","+HP_rank);
        Debug.Log("moveSpeed:"+moveSpeed_val+","+moveSpeed_rank);
        Debug.Log("shootSpeed:"+shootSpeed_val+","+shootSpeed_rank);
        Debug.Log("fillingSpeed:"+fillingSpeed_val+","+fillingSpeed_rank);
        Debug.Log("max_marble_ball:"+max_marble_ball_val+","+max_marble_ball_rank);
        Debug.Log("SKILL:"+skill1_state+","+skill2_state+","+skill3_state+","+aim_state);*/
        ShowMachineState();
        setMachinLevelUpData();
    }

    private void setMachinLevelUpData()
    {
        int tmp_i = 1;

        for (int i = 0; i < ds.Tables [0].Rows.Count; i++)
        {
            if ( int.Parse(ds.Tables[1].Rows[i]["machine_type"].ToString()) == now_page)
                tmp_i = i;
        }

        attack_add_rank_val = float.Parse(ds.Tables [1].Rows[tmp_i]["attack_up_val"].ToString());
        HP_add_rank_val = float.Parse(ds.Tables [1].Rows[tmp_i]["HP_val"].ToString());
        moveSpeed_add_rank_val = float.Parse(ds.Tables [1].Rows[tmp_i]["move_speed_val"].ToString());
        shootSpeed_add_rank_val = float.Parse(ds.Tables [1].Rows[tmp_i]["shoot_speed_val"].ToString());
        fillingSpeed_add_rank_val = float.Parse(ds.Tables [1].Rows[tmp_i]["filling_speed_val"].ToString())*-1;
        max_marble_ball_add_rank_val = float.Parse(ds.Tables [1].Rows[tmp_i]["max_marble_ball_val"].ToString());

        Debug.Log("a:"+attack_add_rank_val.ToString()+" HP:" +HP_add_rank_val.ToString()+" MS:" +moveSpeed_add_rank_val.ToString()+" SS:" +shootSpeed_add_rank_val.ToString()+" FS:" +fillingSpeed_add_rank_val.ToString()+" MAX:" +max_marble_ball_add_rank_val.ToString());
    }

    private void ShowMachineState()
    {
        Show_item_change(skill1, skill1_state);
        Show_item_change(skill2, skill2_state);
        Show_item_change(skill3, skill3_state);
        Show_item_change(aim, aim_state);
        change_state_label("attack", attack_rank, 0, attack_add_rank_val, attack_val);
        change_state_label("HP", HP_rank, 0, HP_add_rank_val, HP_val);
        change_state_label("moveSpeed", moveSpeed_rank, 0, moveSpeed_add_rank_val, moveSpeed_val);
        change_state_label("shootSpeed", shootSpeed_rank, 0, shootSpeed_add_rank_val, shootSpeed_val);
        change_state_label("fillingSpeed", fillingSpeed_rank, 0, fillingSpeed_add_rank_val, fillingSpeed_val);
        change_state_label("max_marble_ball", max_marble_ball_rank, 0, max_marble_ball_add_rank_val, max_marble_ball_val);
    }
    #endregion

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
        tmp_string = SQL.get_and_set_money("get", "000001"/*Goble_Player.member_no*/, null);
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

        //Debug.Log(tmp_money);
        return tmp_money;
    }

    private void moneyTotalCount()
    {
        pay_money = attack_pay + HP_pay + moveSpeed_pay + shootSpeed_pay + fillingSpeed_pay + max_marble_ball_pay + S1_pay + S2_pay + S3_pay + aim_pay;
        if(ball_pay != null){
            foreach (int i in ball_pay)
                pay_money += i;
        }
        //Debug.Log(pay_money);
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


    #region 提升基礎能力區 
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
            int tmp_rank = rank + tmp_val;
            Debug.Log("max_rank:"+max_rank+" rank:"+tmp_rank+" tmp_val:"+tmp_val);
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
                    new_attack_val = attack_add_rank_val * tmp_val + attack_val;
                    new_attack_rank = rank + tmp_val;
                    change_state_label("attack", rank, tmp_val, new_attack_val, attack_val);
                    break;
                case "HP_add":
                    HP_pay = result;
                    new_HP_val = HP_add_rank_val * tmp_val + HP_val;
                    new_HP_rank = rank + tmp_val;
                    change_state_label("HP", rank, tmp_val, new_HP_val, HP_val);
                    break;
                case "moveSpeed_add":
                    moveSpeed_pay = result;
                    new_moveSpeed_val = moveSpeed_add_rank_val * tmp_val + moveSpeed_val;
                    new_moveSpeed_rank = rank + tmp_val;
                    change_state_label("moveSpeed", rank, tmp_val, new_moveSpeed_val, moveSpeed_val);
                    break;
                case "shootSpeed_add":
                    shootSpeed_pay = result;
                    new_shootSpeed_val = shootSpeed_add_rank_val * tmp_val + shootSpeed_val;
                    new_shootSpeed_rank = rank + tmp_val;
                    change_state_label("shootSpeed", rank, tmp_val, new_shootSpeed_val, shootSpeed_val);
                    break;
                case "fillingSpeed_add":
                    fillingSpeed_pay = result;
                    new_fillingSpeed_val = fillingSpeed_add_rank_val * tmp_val + fillingSpeed_val;
                    new_fillingSpeed_rank = rank + tmp_val;
                    change_state_label("fillingSpeed", rank, tmp_val, new_fillingSpeed_val, fillingSpeed_val);
                    break;
                case "max_marble_ball_add":
                    max_marble_ball_pay = result;
                    new_max_marble_ball_val = max_marble_ball_add_rank_val * tmp_val + max_marble_ball_val;
                    new_max_marble_ball_rank = rank + tmp_val;
                    change_state_label("max_marble_ball", rank, tmp_val, new_max_marble_ball_val, max_marble_ball_val);
                    break;
            }
            moneyTotalCount(); //加入才會重新計算金額
        }
    }

    private void change_state_label(string obj_label, int rank, int add_rank, float new_val, float origin_val)
    {
        if (add_rank > 0)
        {
            string tmp_str;
            if(obj_label == "max_marble_ball")
                tmp_str = "Lv" + (rank + add_rank).ToString() + " (" + Mathf.FloorToInt(new_val).ToString() + ")";
            else
                tmp_str = "Lv" + (rank + add_rank).ToString() + " (" + new_val.ToString() + ")";
            
            GameObject.Find(obj_label + "_state").GetComponent<UILabel>().text = tmp_str;
            GameObject.Find(obj_label + "_state").GetComponent<UILabel>().gradientTop = Color.green;
            GameObject.Find(obj_label + "_state").GetComponent<UILabel>().gradientBottom = Color.green;
        }
        else if (add_rank == 0)
        {
            string tmp_str;
            if(obj_label == "max_marble_ball")
                tmp_str = "Lv" + rank.ToString() + " (" + Mathf.FloorToInt(origin_val).ToString() + ")";
            else
                tmp_str = "Lv" + rank.ToString() + " (" + origin_val.ToString() + ")";

            GameObject.Find(obj_label + "_state").GetComponent<UILabel>().text = tmp_str;
            GameObject.Find(obj_label + "_state").GetComponent<UILabel>().gradientTop = Color.white;
            GameObject.Find(obj_label + "_state").GetComponent<UILabel>().gradientBottom = Color.white;
        }
    }
    #endregion


    #region 取得技能/解鎖/特殊彈區域
    public void item_button_change(string str, bool click, int money_type)
    {
        GameObject[] obj = new GameObject[3];
        int result = click? moneyCount(money_type, 0, 0):0;

        switch (str)
        {
            case "s1":
                obj = skill1;
                tmp_skill1_state = click;
                S1_pay = result;
                break;
            case "s2":
                obj = skill2;
                tmp_skill2_state = click;
                S2_pay = result;
                break;
            case "s3":
                obj = skill3;
                tmp_skill3_state = click;
                S3_pay = result;
                break;
            case "aim":
                obj = aim;
                tmp_aim_state = click;
                aim_pay = result;
                break;
        }

        if (click)
        {
            obj[1].SetActive(false);
            obj[2].SetActive(true);
        }
        else
        {
            obj[1].SetActive(true);
            obj[2].SetActive(false);
        }
        moneyTotalCount(); //加入才會重新計算金額

    }

    private void Show_item_change(GameObject[] obj, bool isUnlock)
    {
        if (isUnlock)
        {
            obj[0].GetComponent<UILabel>().text = "已 取 得";
            obj[1].SetActive(false);
            obj[2].SetActive(false);
        }
        else
        {
            obj[0].GetComponent<UILabel>().text = "尚 未 解 鎖";
            obj[1].SetActive(true);
            obj[2].SetActive(false);
        }
    }

    #endregion

    public void data_reset()
    {
        attack_pay = HP_pay = moveSpeed_pay = shootSpeed_pay = fillingSpeed_pay = max_marble_ball_pay = S1_pay = S2_pay = S3_pay = aim_pay = 0;
        if(ball_pay != null){
            foreach (int i in ball_pay)
                ball_pay[i] = 0;
        }
        moneyTotalCount();
        attack_add_val.GetComponent<UIInput>().value = "0";
        HP_add_val.GetComponent<UIInput>().value = "0";
        moveSpeed_add_val.GetComponent<UIInput>().value = "0";
        shootSpeed_add_val.GetComponent<UIInput>().value = "0";
        fillingSpeed_add_val.GetComponent<UIInput>().value = "0";
        max_marble_ball_add_val.GetComponent<UIInput>().value = "0";
        Show_item_change(skill1, skill1_state);
        Show_item_change(skill2, skill2_state);
        Show_item_change(skill3, skill3_state);
        Show_item_change(aim, aim_state);
    }

    public void change_appear_machine()
    {
        isuse_tmp = now_page;
        string machine_tmp = now_page.ToString("00");
        string result = SQL.set_machine_data("000001"/*Goble_Player.member_no*/,machine_tmp,"","","","","","","","","","","","","","","","","","","Y","");
        Debug.Log(result);
    }

    public void send_data_ok()
    {
        if (pay_money > 0)
        {
            if (pay_money > tmp_monay)
            {
                //跑出視窗"所持有的金額不夠喔!!"
            }
            else
            {
                //比對資料差異
                new_total_rank = (int)Mathf.Floor((new_attack_rank + new_HP_rank + new_moveSpeed_rank) / 3);
                new_attack_rank = (new_attack_val == 0) ? attack_rank : new_attack_rank;
                new_HP_rank = (new_HP_val == 0) ? HP_rank : new_HP_rank;
                new_moveSpeed_rank = (new_moveSpeed_val == 0) ? moveSpeed_rank : new_moveSpeed_rank;
                new_shootSpeed_rank = (new_shootSpeed_val == 0) ? shootSpeed_rank : new_shootSpeed_rank;
                new_fillingSpeed_rank = (new_fillingSpeed_val == 0) ? fillingSpeed_rank : new_fillingSpeed_rank;
                new_max_marble_ball_rank = (new_max_marble_ball_val == 0) ? max_marble_ball_rank : new_max_marble_ball_rank;

                new_attack_val = (new_attack_rank == attack_rank)? attack_val: new_attack_val;
                new_HP_val = (new_HP_rank == HP_rank)? HP_val: new_HP_val;
                new_moveSpeed_val = (new_moveSpeed_rank == moveSpeed_rank)? moveSpeed_val: new_moveSpeed_val;
                new_shootSpeed_val = (new_shootSpeed_rank == shootSpeed_rank)? shootSpeed_val: new_shootSpeed_val;
                new_fillingSpeed_val = (new_fillingSpeed_rank == fillingSpeed_rank)? fillingSpeed_val: new_fillingSpeed_val;
                new_max_marble_ball_val = (new_max_marble_ball_rank == max_marble_ball_rank)? max_marble_ball_val: new_max_marble_ball_val;

                tmp_skill1_state = skill1_state ? skill1_state : tmp_skill1_state;
                tmp_skill2_state = skill2_state ? skill2_state : tmp_skill2_state;
                tmp_skill3_state = skill3_state ? skill3_state : tmp_skill3_state;
                tmp_aim_state = aim_state ? skill1_state : tmp_aim_state;
               
                Debug.Log("Update - attack => " + new_attack_rank + "," + new_attack_val);
                Debug.Log("Update - HP     => " + new_HP_rank + "," + new_HP_val);
                Debug.Log("Update - MoveS  => " + new_moveSpeed_rank + "," + new_moveSpeed_val);
                Debug.Log("Update - ShootS => " + new_shootSpeed_rank + "," + new_shootSpeed_val);
                Debug.Log("Update - FillS  => " + new_fillingSpeed_rank + "," + new_fillingSpeed_val);
                Debug.Log("Update - Max    => " + new_max_marble_ball_rank + "," + new_max_marble_ball_val);
                Debug.Log("Update - Rank   => " + new_total_rank);
                Debug.Log("Update - S1     => " + tmp_skill1_state);
                Debug.Log("Update - S2     => " + tmp_skill2_state);
                Debug.Log("Update - S3     => " + tmp_skill3_state);
                Debug.Log("Update - Aim    => " + tmp_aim_state);
                //送出資料
            }
        }
    }
}
