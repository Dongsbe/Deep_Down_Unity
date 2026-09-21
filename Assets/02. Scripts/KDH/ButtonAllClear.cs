using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAllClear : MonoBehaviour {

    public GameObject[] LightOn = new GameObject[4];
    ChangeMaterial[] ChangeLight = new ChangeMaterial[4];

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 4; i++)
        {
            ChangeLight[i] = LightOn[i].GetComponent<ChangeMaterial>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(ChangeLight[0].ReturnBool() && ChangeLight[1].ReturnBool() && ChangeLight[2].ReturnBool() && ChangeLight[3].ReturnBool())
        {
            GameControlManager.GetInstance().m_bIsCabinetOpen = true;
        }
	}
}
