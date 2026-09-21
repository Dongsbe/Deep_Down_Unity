using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairInfo : MonoBehaviour {

    private Transform cPosition;
    float speed = 6.0f;
    public GameObject Trigg;
    EnemyRespawn EObj;
    bool isMove;
    bool isLeveStart;

    bool playSound;

    // Use this for initialization
    void Start ()
    {
        EObj = Trigg.GetComponent<EnemyRespawn>();
        cPosition = GetComponent<Transform>();
        isMove = true;
        isLeveStart = false;
        playSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EObj.GetisEnemyResepawn() && isMove)
        {
            if(playSound == false)
            {
                GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
                GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.WheelChairMove);
            }
            //문닫기
            GameControlManager.GetInstance().isPublicTolletClear = false;


            cPosition.localPosition = new Vector3(cPosition.localPosition.x + Time.deltaTime * speed, cPosition.localPosition.y, cPosition.localPosition.z );
            playSound = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyEnemy"))
        {
            isMove = false;
            GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
            GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.WheelChairCrash);
            StartCoroutine("UpLeverend");
            GameControlManager.GetInstance().dooropentest = true;
        }
    }


    IEnumerator UpLeverend()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("들어옴");
        GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
        GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.PatientRise);
        isLeveStart = true;
    }

    public bool ReturnLeve()
    {
        return isLeveStart;
    }

}
