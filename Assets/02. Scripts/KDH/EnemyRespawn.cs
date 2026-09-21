using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

    bool isEnemyRespawn;

    private void Start()
    {
        isEnemyRespawn = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isEnemyRespawn = true;
        }
    }

    public bool GetisEnemyResepawn()
    {
        return isEnemyRespawn;
    }
}
