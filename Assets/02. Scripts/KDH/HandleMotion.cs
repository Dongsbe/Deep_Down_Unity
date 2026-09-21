using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMotion : MonoBehaviour {

    Animator Ani;


    HandCreate HandCre;

    bool HandState = false;
    public bool IsMotion = false;

	// Use this for initialization
	void Start () {
        Ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void StartAni()
    {
        if (IsMotion)
        {
            StartCoroutine("MotionStart");
        }
    }

    public IEnumerator MotionStart()
    {
        yield return new WaitForSeconds(0.0f);
        HandState = true;
        Ani.SetBool("isTrue", HandState);
    }
}
