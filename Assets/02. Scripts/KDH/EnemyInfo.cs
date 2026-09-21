using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class EnemyInfo : MonoBehaviour
{

    private Transform cPosition;

    public GameObject Character;
    public GameObject CharCam;
    GameObject preChar;
    GameObject preCharCam;

    float speed = 1.5f;
    private bool isDead = false;
    public bool isMonster;
    public GameObject Door;

    bool SoundCheck = false;
    EnemyRespawn EObj;
    Animator Ani;
    // Use this for initialization
    void Start()
    {
        isMonster = true;
        EObj = Door.GetComponent<EnemyRespawn>();
        cPosition = GetComponent<Transform>();
        Ani = GetComponent<Animator>();
        Ani.SetBool("isSurprise", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EObj.GetisEnemyResepawn())
        {
            Ani.SetBool("isSurprise", false);
            cPosition.localPosition = new Vector3(cPosition.localPosition.x, cPosition.localPosition.y, cPosition.localPosition.z + Time.deltaTime * speed);
            GameControlManager.GetInstance().m_bIsOnGame = false;

            //Vector3 vec = this.gameObject.transform.position - Character.transform.position;
            //vec.Normalize();

            //Quaternion q = Quaternion.LookRotation(vec);
            //Character.transform.rotation = q;

            Character.transform.LookAt(this.gameObject.transform);
            Cursor.visible = false;

            if (SoundCheck == false)
            {
                GameSoundManager.GetInstance().GetAudioSource(this.gameObject.GetComponent<AudioSource>());
                GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.WalkMan);
                SoundCheck = true;
            }
        }

        else
        {
            Ani.SetBool("isSurprise", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DestroyEnemy"))
        {
            //isMonster = false;
            //Character.GetComponent<FirstPersonController>().m_MouseLook.Init(Character.transform, CharCam.transform);
            GameControlManager.GetInstance().m_bIsOnGame = true;
            Destroy(this.gameObject);
        }
    }
}
