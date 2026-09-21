using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YSM_ChangeScene_test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        SceneManager.LoadScene("(UI SpinWithMouse)Example 11 - Drag & Drop");
    }

    public void OffClick()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
