using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnViewLock : MonoBehaviour {

    int cnt = 0;
    float speed = 3.0f;
    public bool istrue;
    private Transform cPosition;
    // Use this for initialization
    void Start () {
        cPosition = GetComponent<Transform>();
        istrue = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (istrue && cnt < 9)
        {
            cnt++;
            cPosition.localPosition = new Vector3(cPosition.localPosition.x, cPosition.localPosition.y, cPosition.localPosition.z + Time.deltaTime * speed);
        }
	}
}
