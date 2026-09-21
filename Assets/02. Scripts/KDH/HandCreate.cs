using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCreate : MonoBehaviour {

    GameObject Hand_Object;
    public Transform startPosition;

    public bool IsPop;
    public bool IsMotion;

    public GameObject obj;
    HandleMotion ani;

    // Use this for initialization
    void Start () {
        Hand_Object = Resources.Load("Hand Animation01") as GameObject;
        //ani = Hand_Object.GetComponent<HandleMotion>();

        //startPosition = GetComponent<Transform>();
        IsPop = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HandPos()
    {
        obj = Instantiate(Hand_Object) as GameObject;
        if (obj != null)
        {
            obj.transform.localPosition = new Vector3(startPosition.localPosition.x,
                                                      startPosition.localPosition.y,
                                                      startPosition.localPosition.z);

            ani = obj.GetComponent<HandleMotion>();
            ani.IsMotion = true;

            ani.StartAni();
        }
    }
}
