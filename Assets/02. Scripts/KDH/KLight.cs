using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLight : MonoBehaviour {

    public GameObject KDH_Light;
    public GameObject KDH_MeshRenderer;
    public GameObject KDH_MeshRenderer2;

    public bool trues = false;

    public GameObject GameMamager;

    OpenDoorTrigger tile;

    // Use this for initialization
    void Start () {
        KDH_Light.GetComponent<Light>().enabled = false;
        KDH_MeshRenderer.GetComponent<MeshRenderer>().enabled = false;
        KDH_MeshRenderer2.GetComponent<MeshRenderer>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        trues = GameControlManager.GetInstance().IsStageObjClear;

        if (trues)
        {
            KDH_Light.GetComponent<Light>().enabled = true;
            KDH_MeshRenderer.GetComponent<MeshRenderer>().enabled = true;
            KDH_MeshRenderer2.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
