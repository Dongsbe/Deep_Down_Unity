using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSM_test_LightButton : MonoBehaviour {

    public bool[] UserInput = new bool[4];
    public bool[] Answer = new bool[4];
    public GameObject[] LightButton = new GameObject[4];

    public int CheckIndex;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < 4; i++)
        {
            UserInput[i] = false;
            Answer[i] = true;
        }

        CheckIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickLightButton1()
    {
        for(int i = 0; i < 4; i++)
        {
            if (i == 1)
                continue;
            UserInput[i] = !UserInput[i];
        }

        CheckAnswer();
    }

    public void ClickLightButton2()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 1 || i == 2)
                continue;
            UserInput[i] = !UserInput[i];
        }

        CheckAnswer();
    }

    public void ClickLightButton3()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 2)
                continue;
            UserInput[i] = !UserInput[i];
        }

        CheckAnswer();
    }

    public void ClickLightButton4()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 1 || i == 2)
                continue;
            UserInput[i] = !UserInput[i];
        }

        CheckAnswer();
    }

    public void CheckAnswer()
    {
        for(int i = 0; i < 4; i++)
        {
            if(UserInput[i] == true)
            {
                CheckIndex += 1;
            }
            else
            {
                CheckIndex -= 1;
            }
        }

        if(CheckIndex == 4)
        {
            CheckIndex = 0;
        }
    }
}
