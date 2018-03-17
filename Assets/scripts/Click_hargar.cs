using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Click_hargar : MonoBehaviour {

    private hangar_controller con;
    private Color tmp_color;

	void Awake()
	{
		EventListener.Get (gameObject).onClick += ObjectOnClick;
		EventListener.Get (gameObject).onPress += ObjectOnPress;
        EventListener.Get (gameObject).onHover += ObjectOnHover;
	}

	void Start()
	{
        con = GameObject.Find ("controller").GetComponent<hangar_controller> ();
	}

	private void ObjectOnClick(GameObject go)
	{
		ObjectOnHover (go, false);
		switch(gameObject.name)
		{
            case "attack+":
                con.attack_add_change("add");
                break;
            case "attack-":
                con.attack_add_change("minus");
                break;
            case "HP+":
                con.HP_add_change("add");
                break;
            case "HP-":
                con.HP_add_change("minus");
                break;
            case "moveSpeed+":
                con.MS_add_change("add");
                break;
            case "moveSpeed-":
                con.MS_add_change("minus");
                break;
            case "shootSpeed+":
                con.SS_add_change("add");
                break;
            case "shootSpeed-":
                con.SS_add_change("minus");
                break;
            case "fillingSpeed+":
                con.FS_add_change("add");
                break;
            case "fillingSpeed-":
                con.FS_add_change("minus");
                break;
            case "max_marble_ball+":
                con.MMB_add_change("add");
                break;
            case "max_marble_ball-":
                con.MMB_add_change("minus");
                break;
            case "unlock_s1":
                con.item_button_change("s1", true, 3); //要加上前的代碼技能=3，瞄準器=4、特殊彈=5...。
                break;
            case "unlock_s1_cancel":
                con.item_button_change("s1", false, 3);
                break;
            case "unlock_s2":
                con.item_button_change("s2", true, 3);
                break;
            case "unlock_s2_cancel":
                con.item_button_change("s2", false, 3);
                break;
            case "unlock_s3":
                con.item_button_change("s3", true, 3);
                break;
            case "unlock_s3_cancel":
                con.item_button_change("s3", false, 3);
                break;
            case "unlock_aim":
                con.item_button_change("aim", true, 4);
                break;
            case "unlock_aim_cancel":
                con.item_button_change("aim", false, 4);
                break;
            case "reset":
                con.data_reset();
                break;
            case "use_btn":
                con.change_appear_machine();
                break;
            case "next":
                con.change_machine("Right");
                break;
            case "prev":
                con.change_machine("Left");
                break;
            case "send_data":
                con.send_data_ok();
                break;

		
		}
		//Debug.Log (gameObject.name + " : Click");
	}

	private void ObjectOnPress(GameObject go, bool isPress)
	{
        //Debug.Log (gameObject.name + " : Press : " + isPress);
        UITexture tmpUI = go.GetComponent<UITexture> ();
        if (tmpUI != null && isPress)
            tmpUI.color = Color.red;
        else if (tmpUI != null && !isPress)
        {
            foreach (GameObject btn in GameObject.FindGameObjectsWithTag("btn"))
            {
                btn.GetComponent<UITexture>().color = tmp_color;//new Color(255.0f, 255.0f, 255.0f);
            }
            tmpUI.color = tmp_color;//new Color(255.0f, 255.0f, 255.0f);
        }
	}

	private void ObjectOnHover(GameObject go, bool isHover)
	{
        UITexture tmpUI = go.GetComponent<UITexture> ();
        if (tmpUI != null && isHover)
        {
            tmp_color = tmpUI.color;
            tmpUI.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        }
        else if (tmpUI != null && !isHover)
        {
            foreach (GameObject btn in GameObject.FindGameObjectsWithTag("btn"))
            {
                btn.GetComponent<UITexture>().color = tmp_color;//new Color(255.0f, 255.0f, 255.0f);
            }
            tmpUI.color = tmp_color;//new Color(255.0f, 255.0f, 255.0f);
        }
    }
}
