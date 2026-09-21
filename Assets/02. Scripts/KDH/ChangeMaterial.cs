using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dtype
{
    Mirror1,
    Mirror2
}

public class ChangeMaterial : MonoBehaviour {

    public Material[] material = new Material[4];
    Renderer rend;
    public dtype ctype;
    private bool isOn;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch(ctype)
        {
            case dtype.Mirror1:
                {
                    if (GameControlManager.GetInstance().isXrayMaterailChange)
                    {
                        rend.sharedMaterial = material[1];
                    }
                    else
                    {
                        rend.sharedMaterial = material[0];
                    }
                }
                break;
            default:
                break;
        }

    }

    public void Click()
    {
            if (isOn)
            {
                rend.sharedMaterial = material[0];
                isOn = false;
            }
            else
            {
                rend.sharedMaterial = material[1];
                isOn = true;
            }
        GameSoundManager.GetInstance().GetCharacterSound();
        GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.SwitchSound);
    }

    public bool ReturnBool()
    {
        return isOn;
    }
}
