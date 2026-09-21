using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLock : MonoBehaviour {

    public bool isOpen;
    public EventObject parentObj;

    // Use this for initialization
    void Start () {
        isOpen = false ;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            //if (cnt < 3)
            //{
            //    cnt++;
            //    cPosition.localPosition = new Vector3(cPosition.localPosition.x , cPosition.localPosition.y - Time.deltaTime * speed, cPosition.localPosition.z );
            //}

            parentObj.enabled = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        else
        {
            parentObj.enabled = false;
        }
    }
}
