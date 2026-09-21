using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSM_InventoryForStage : MonoBehaviour {
    public bool isStage1;
    public bool isStage2;

	// Use this for initialization
	void Start () {
        isStage1 = false;
        isStage2 = false;
	}
	
	// Update is called once per frame
	void Update () {
        Inven_StageCheck();
	}

    void Inven_StageCheck()
    {
        Transform TempTns1 = null;
        Transform TempTns2 = null;

        if (GameControlManager.GetInstance().curStage == StageInfo.Stage1 && isStage1 == false)
        {
            TempTns1 = this.transform.Find("Stage 1_Inventory");
            TempTns2 = this.transform.Find("Stage 2_Inventory");

            TempTns1.gameObject.SetActive(true);
            TempTns2.gameObject.SetActive(false);

            isStage1 = true;
            isStage2 = false;
        }

        else if (GameControlManager.GetInstance().curStage == StageInfo.Stage2 && isStage2 == false)
        {
            TempTns1 = this.transform.Find("Stage 1_Inventory");
            TempTns2 = this.transform.Find("Stage 2_Inventory");

            TempTns1.gameObject.SetActive(false);
            TempTns2.gameObject.SetActive(true);

            isStage2 = true;
            isStage1 = false;
        }
    }
}
