using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCorrect : MonoBehaviour {

    public GameObject[] Button = new GameObject[4];
    ChangeMaterial[] ChaMaterial = new ChangeMaterial[4];
    bool isButtonCorrent;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 4; i++)
        {
            ChaMaterial[i] = Button[i].GetComponent<ChangeMaterial>();
        }
        isButtonCorrent = false;
    }

    // Update is called once per frame
    void Update() {

        if (ChaMaterial[0].ReturnBool() && ChaMaterial[1].ReturnBool() && ChaMaterial[2].ReturnBool() && ChaMaterial[3].ReturnBool())
        {
            isButtonCorrent = true;
        }
	}
}
