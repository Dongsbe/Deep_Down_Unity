using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KLight : MonoBehaviour {

    public GameObject KDH_Light;
    public GameObject KDH_MeshRenderer;
    public GameObject KDH_MeshRenderer2;

    public bool trues = false;

    public GameObject GameMamager;

    private Light stageClearLight;
    private MeshRenderer lightOnRenderer;
    private MeshRenderer lightOffRenderer;
    private bool isStageClearApplied;

    // Use this for initialization
    void Start ()
    {
        stageClearLight = KDH_Light.GetComponent<Light>();
        lightOnRenderer = KDH_MeshRenderer.GetComponent<MeshRenderer>();
        lightOffRenderer = KDH_MeshRenderer2.GetComponent<MeshRenderer>();

        ApplyLightState(false);
        isStageClearApplied = false;
    }
	
	// Update is called once per frame
    void Update ()
    {
        if (isStageClearApplied)
            return;

        trues = GameControlManager.GetInstance().IsStageObjClear;

        if (!trues)
            return;

        ApplyLightState(true);
        isStageClearApplied = true;
    }

    private void ApplyLightState(bool isOn)
    {
        stageClearLight.enabled = isOn;
        lightOnRenderer.enabled = isOn;
        lightOffRenderer.enabled = !isOn;
    }
}
