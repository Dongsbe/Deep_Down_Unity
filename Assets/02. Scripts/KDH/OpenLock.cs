using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLock : MonoBehaviour {

    //private int SaveNum[3];

    public GameObject MLock;
    public GameObject LockView;
    public GameObject Hindge;
    MainLock MaLock;
    ReturnViewLock LockViLo;
    TestHindge THindge;
    private bool isLockOpen;
    float speed = 3.0f;
    private int cnt = 0;
    public GameObject[] Number = new GameObject[3];
    LockNumber[] LockNum = new LockNumber[3];

    private int[] CheckNum = new int[3];
    private Transform cPosition;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 3; i++)
        {
            LockNum[i] = Number[i].GetComponent<LockNumber>();
            CheckNum[i] = LockNum[i].ReturnSuccNum();
        }
        LockViLo = LockView.GetComponent<ReturnViewLock>();
        MaLock = MLock.GetComponent<MainLock>();
        THindge = Hindge.GetComponent<TestHindge>();
        cPosition = GetComponent<Transform>();   
    }

    // Update is called once per frame
    public bool ReturnBool()
    {
        return isLockOpen;
    }

    void Update ()
    {
        if (isLockOpen || !IsCorrectCode())
            return;

        Unlock();
    }

    private bool IsCorrectCode()
    {
        for (int i = 0; i < LockNum.Length; i++)
        {
            if (LockNum[i].ReturnNum() != CheckNum[i])
                return false;
        }

        return true;
    }

    private void Unlock()
    {
        isLockOpen = true;
        LockViLo.istrue = true;
        MaLock.isOpen = true;
        THindge.isOpen = true;

        GameControlManager.GetInstance().isLockOpen = true;
        GameSoundManager.GetInstance().GetCharacterSound();
        GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.LockOpenSound);

        StartCoroutine(OpenAnimation());
    }

    private IEnumerator OpenAnimation()
    {
        while (cnt < 9)
        {
            cnt++;
            cPosition.localPosition = new Vector3(
                cPosition.localPosition.x,
                cPosition.localPosition.y,
                cPosition.localPosition.z + Time.deltaTime * speed);

            yield return null;
        }
    }
}
