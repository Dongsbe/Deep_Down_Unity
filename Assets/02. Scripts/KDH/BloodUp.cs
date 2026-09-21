using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum type
{
    Blood,
    Leverand
};

public class BloodUp : MonoBehaviour {

    public type eType;  
    private bool suminbabo;
    private bool leverand;
    public float speed;
    Transform cPosition;
    private int count;

    public GameObject Leve;
    WheelChairInfo LeveUp;

	// Use this for initialization
	void Start ()
    {
        speed = 0.1f;
        count = 0;
        //suminbabo = false;
        leverand = false;
        LeveUp = Leve.GetComponent<WheelChairInfo>();
        cPosition = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        switch(eType)
        {
            case type.Blood:
                //{
                //    if (Input.GetKey(KeyCode.X))
                //        suminbabo = true;

                //    if (suminbabo && count < 90)
                //    {
                //        cPosition.localPosition = new Vector3(cPosition.localPosition.x, cPosition.localPosition.y + Time.deltaTime * speed, cPosition.localPosition.z);
                //        count++;
                //    }
                //}
                break;

            case type.Leverand:
                {
                    if (LeveUp.ReturnLeve())
                    {
                        leverand = true;
                    }
                        
                    if (leverand && count < 100)
                    {
                        cPosition.localPosition = new Vector3(cPosition.localPosition.x, cPosition.localPosition.y + Time.deltaTime * speed, cPosition.localPosition.z);
                        count++;
                    }
                }
                break;
            default:
                break;
        }
    }
}
