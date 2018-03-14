using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_script : MonoBehaviour {

	public Texture2D background_img;
	public Texture2D slider_bg;
	public Texture2D slider_fill;
	private float fps = 10.0f;
	private float time = 0f;
	private int nowFram = 0;
	private int displayProgress = 0;
	private float tmp = 0f;

	//異步對象
	private AsyncOperation async;

	//處理進度0-1
	private int progress = 0;

	public GameObject sliderBar;

	// Use this for initialization
	void Start () {
		//開啟異步
		StartCoroutine (LoadingScene ());
	}
	
	// Update is called once per frame
	void Update () {

		//在這裡計算讀取的進度，
		//progress 的取值範圍在0.1 - 1之間， 但是它不會等於1
		//也就是說progress可能是0.9的時候就直接進入新場景了
		//所以在寫進度條的時候需要注意一下。
		//為了計算百分比 所以直接乘以100即可
		if (Globe.loadName != "maze") {
			progress = (int)(async.progress * 100);
		}

		//Debug.Log("進度" +progress + "%");
	}

	IEnumerator LoadingScene(){
		int toProgress = 0;
		
		//異步讀取前一場景的目標場景
		async = Application.LoadLevelAsync (Globe.loadName);
		
		async.allowSceneActivation = false; //設置為false就會停在這個場景

		while (async.progress < 0.9f) {
			toProgress = (int)(async.progress * 100);
			if (displayProgress < toProgress) {
				displayProgress+=2;
				SetLoadingSlider (displayProgress);
				yield return new WaitForEndOfFrame ();
			}
		}
		toProgress = 100;
		while (displayProgress < toProgress){
			displayProgress+=2;
			SetLoadingSlider (displayProgress);
			yield return new WaitForEndOfFrame ();
		}

		if (displayProgress >= 100) {
			async.allowSceneActivation = true; //設置為false就會停在這個場景
		}

		/*while (async.progress < 0.9f)
		{
			toProgress = (int)async.progress * 100;
			while (displayProgress < toProgress)
			{
				++displayProgress;
				SetLoadingSlider(displayProgress);
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForEndOfFrame();
		}
		toProgress = 100;
		while (displayProgress < toProgress)
		{
			++displayProgress;
			SetLoadingSlider(displayProgress);
			yield return new WaitForEndOfFrame();
		}*/

		//讀取完返回場景
		yield return async;

	}

	void OnGUI()
	{

		GUIStyle BoxStyle_BG = new GUIStyle(GUI.skin.box);
		BoxStyle_BG.normal.background = background_img;

		GUIStyle BoxStyle_SliderFill = new GUIStyle(GUI.skin.box);
		BoxStyle_SliderFill.normal.background = slider_fill;

		GUIStyle Slider_backgroundStyle = new GUIStyle (GUI.skin.horizontalScrollbar);
		Slider_backgroundStyle.normal.background =  slider_bg;
		Slider_backgroundStyle.margin.top = (int)(Screen.height * 0.5f);

		GUIStyle LabelStyle_center = new GUIStyle(GUI.skin.label);
		LabelStyle_center.alignment =  TextAnchor.MiddleCenter;

		float loading_y;
		if (Screen.height <= Screen.width * 0.75f) {
			if ((int)(Screen.height * 0.08f) < 18) {
				Slider_backgroundStyle.fixedHeight = LabelStyle_center.fontSize = 18;
			} else {
				Slider_backgroundStyle.fixedHeight = LabelStyle_center.fontSize = (int)(Screen.height * 0.1f);
			}
			loading_y = Screen.height * 0.3f;
		} else {
			if ((int)(Screen.height * 0.08f) < 18) {
				Slider_backgroundStyle.fixedHeight = LabelStyle_center.fontSize = 18;
			} else {
				LabelStyle_center.fontSize = (int)(Screen.height * 0.05f);
				Slider_backgroundStyle.fixedHeight = (int)(Screen.height * 0.1f);
			}
			loading_y = Screen.height * 0.3f;
		}

		//GUI.Box (new Rect (Screen.width * 0f, Screen.height * 0f, Screen.width * 1f, Screen.height * 1f),"",BoxStyle_BG);


		//在這裡顯示讀取的進度。
        GUI.color = Color.yellow;
		GUI.Label(new Rect( Screen.width*0.15f,loading_y,Screen.width*0.7f,Screen.width*0.2f), "Loading..." + displayProgress + "%",LabelStyle_center );

		sliderBar.GetComponent<UISlider> ().value = tmp;

		GUI.color = Color.white;
		/*GUI.HorizontalSlider (new Rect(Screen.width * 0.2f, Screen.height * 0.6f, Screen.width * 0.6f, Screen.height * 0.1f),tmp,0.0f,1.0f,Slider_backgroundStyle,"");
		GUI.Box (new Rect(Screen.width * 0.2f, Screen.height * 0.646f, Screen.width * 0.6f*(tmp+0.0001f), Screen.height * 0.048f), "", BoxStyle_SliderFill);*/
	}

	
	void SetLoadingSlider(int progress)
	{
		tmp = (float)((float)progress / 100);
	}
}
