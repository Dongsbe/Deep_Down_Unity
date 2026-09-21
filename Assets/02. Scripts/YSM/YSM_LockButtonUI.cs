using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSM_LockButtonUI : MonoBehaviour {

    public UILabel stLabel;
    public int PassWord;

	// Use this for initialization
	void Start () {
        PassWord = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void ClickLockButton1()
    {
        PassWord += 100;
        if (PassWord >= 1000)
        {
            PassWord -= 1000;

            stLabel.text = PassWord.ToString("D3");
        }

        stLabel.text = PassWord.ToString("D3");
    }

    public void ClickLockButton2()
    {
        PassWord += 10;
        //stLabel.text = PassWord.ToString();

        if(((PassWord / 10) % 10 == 0) && PassWord >= 100)
        {
            PassWord -= 100;
            stLabel.text = PassWord.ToString("D3");
        }
        stLabel.text = PassWord.ToString("D3");
    }

    public void ClickLockButton3()
    {
        PassWord += 1;

        if(((PassWord % 10) % 10 == 0) && PassWord >= 10)
        {
            PassWord -= 10;
            stLabel.text = PassWord.ToString("D3");
        }

        stLabel.text = PassWord.ToString("D3");
    }
}
