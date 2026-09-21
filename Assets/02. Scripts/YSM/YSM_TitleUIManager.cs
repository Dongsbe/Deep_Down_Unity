using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YSM_TitleUIManager : MonoBehaviour {

    public GameObject go;
    public GameObject go1;
    public GameObject go2;

    UISprite ControlKey_Spr;
    UISprite[] ButtonSpr = new UISprite[3];

	// Use this for initialization
	void Start () {
        ControlKey_Spr = GameObject.Find("BackGround").GetComponent<UISprite>();
        ButtonSpr[0] = GameObject.Find("NewGameButton").GetComponent<UISprite>();
        ButtonSpr[1] = GameObject.Find("ControlKeyButton").GetComponent<UISprite>();
        ButtonSpr[2] = GameObject.Find("ExitButton").GetComponent<UISprite>();
    }
	
	// Update is called once per frame
	void Update () {
        if(ControlKey_Spr.spriteName == "Control_UI" && Input.GetKeyDown(KeyCode.E))
        {
            ControlKey_Spr.GetComponent<UISprite>().spriteName = "Main_Menu_Main";

            for (int i = 0; i < ButtonSpr.Length; i++)
            {
                ButtonSpr[i].enabled = true;
            }
        }
	}

    public void OnClickNewGameButton()
    {
        // 여기서 SceneChange 사용
        Debug.Log("Click Start Button");
        
        // 나중에 메인씬으로 바뀌게 해야함
        SceneManager.LoadScene(1);
    }

    public void OnClickControlKey()
    {
        // 컨트롤 UI 띄우기
        ControlKey_Spr.GetComponent<UISprite>().spriteName = "Control_UI";

        for(int i = 0; i < ButtonSpr.Length; i++)
        {
            ButtonSpr[i].enabled = false;
        }
    }

    public void OnClickOptionButton()
    {
        // 옵션창 띄우기 
    }

    public void OnClickExitButton()
    {
        Debug.Log("Click End Button");

        go.gameObject.SetActive(false);
        go1.gameObject.SetActive(false);
        go2.gameObject.SetActive(true);
    }

    public void OnClickSure_YesButton()
    {
        Debug.Log("Click Exit Button");
        Application.Quit();
        //EditorApplication.Exit(0);
    }

    public void OnClickSure_NoButton()
    {
        go.gameObject.SetActive(true);
        go1.gameObject.SetActive(true);
        go2.gameObject.SetActive(false);
    }

    public void DisAppearButton()
    {
    }
}
