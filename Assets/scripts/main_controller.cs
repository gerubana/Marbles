using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_controller : MonoBehaviour {

    public GameObject[] step;
    public GameObject back_btn;
    private int now_step;

	// Use this for initialization
	void Start () {
        now_step = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (now_step == 0)
            back_btn.SetActive(false);
        else
            back_btn.SetActive(true);
	}

    public void step_back()
    {
        change_step(now_step - 1);
    }

    public void change_step(int step_num)
    {
        for (int i = 0; i < step.Length; i++)
        {
            step[i].SetActive(false);
        }
        step[step_num].SetActive(true);
        now_step = step_num;
    }

    public void select_level(int level, int type)
    {
        int rank = level*10;

        /*if(level != 100)
            rank = (int)Random.Range((level-1) * 10.0f+1.0f, level * 10.0f);
        else
            rank = level;*/

        if(type == 99) 
            type = (int)Random.Range(1.0f, 4.0f);

        Debug.Log("rank=" + rank + "; type=" + type);

        Goble_Player.AI_rank = rank;
        Goble_Player.AI_machine_no = type.ToString("D2");

        Globe.loadName = "get_data_and_final_check";
        Application.LoadLevel ("Loading");
    }
}
