using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSM_OnManneQuinn : MonoBehaviour {
    GameObject MannequinObj;
    Transform MannequinTns;

    bool visible;
	// Use this for initialization
	void Start () {
        MannequinObj = GameObject.Find("StandMannequinObj");
        MannequinTns = MannequinObj.transform.Find("Stand Mannequin");

        visible = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameControlManager.GetInstance().Stage2Wallet == true && visible == false)
        {
            MannequinTns.gameObject.SetActive(true);
            visible = true;
        }
	}
}
