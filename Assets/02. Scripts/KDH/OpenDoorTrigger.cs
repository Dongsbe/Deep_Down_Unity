using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    bool isOpen;
    bool isClasesDoor = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isClasesDoor)
        {
            isClasesDoor = true;
            GameControlManager.GetInstance().isStage2OpenDoor = true;
        }
    }

}
