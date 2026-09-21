using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTwi : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("LightOnOff");
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator LightOnOff()
    {
        int randtime = (int)Random.Range(1, 3);
        yield return new WaitForSeconds(randtime);

        if(this.gameObject.GetComponent<Light>().enabled == true)
        {
            this.gameObject.GetComponent<Light>().enabled = false;
        }

        else
        {
            this.gameObject.GetComponent<Light>().enabled = true;
        }

        StartCoroutine("LightOnOff");
    }
}
