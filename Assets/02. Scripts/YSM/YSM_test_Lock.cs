using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSM_test_Lock : MonoBehaviour {

    public int PassWord;
    public int UserInputFirst;
    public int UserInputSecond;
    public int UserInputThird;

    public GameObject lockButton1;
    public GameObject lockButton2;
    public GameObject lockButton3;

    // Use this for initialization
    void Start () {
        PassWord = 0;
        UserInputFirst = 0;
        UserInputSecond = 0;
        UserInputThird = 0;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void ClickLockButton1()
    {
        UserInputFirst += 1;
        if (UserInputFirst == 10)
            UserInputFirst = 0;

        iTween.RotateTo(lockButton1, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "looptype", iTween.LoopType.none, "speed", 50.0f));

        ShowUserInput();
    }
    public void ClickLockButton2()
    {
        UserInputSecond += 1;
        if (UserInputSecond == 10)
            UserInputSecond = 0;

        iTween.RotateTo(lockButton1, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "looptype", iTween.LoopType.none, "speed", 50.0f));

        ShowUserInput();
    }
    public void ClickLockButton3()
    {
        UserInputThird += 1;
        if (UserInputThird == 10)
            UserInputThird = 0;

        iTween.RotateTo(lockButton1, iTween.Hash("y", 90, "easeType", iTween.EaseType.easeInOutQuart, "looptype", iTween.LoopType.none, "speed", 50.0f));

        ShowUserInput();
    }

    public void ShowUserInput()
    {
        Debug.Log("ShowUserInput = " + UserInputFirst + UserInputSecond + UserInputThird);
    }

}
