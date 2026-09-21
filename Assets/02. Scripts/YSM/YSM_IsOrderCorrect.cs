using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class YSM_IsOrderCorrect : MonoBehaviour
{

    //public GameObject[] CorrectOrder = new GameObject[4];
    public GameObject[] CorrectPositionOrder = new GameObject[4];
    public GameObject[] SaveUserOrder = new GameObject[4];
    public VideoPlayer[] inCorrectOrder = new VideoPlayer[4];
    public Camera[] inCorrectOrderCamera = new Camera[4];

    public int OrderCount;
    public int CorrectCount;
    public bool ObjFull;

    public bool isCorrect;
    public bool inCorrect;

    public int inCorrectVideoOrder;

    public VideoPlayer FullMovie;

    // 2스테이지에 진입하게 되면 게임컨트롤매니저의 CurStage가 Stage 2 로 바뀔 것 이므로 그 때 한번 실행하게 하는 변수
    public bool isStage1;
    public bool isStage2;

    //public YSM_Inventory_Item BeforeItemPosition;

    // Use this for initialization
    void Start()
    {
        OrderCount = 0;
        CorrectCount = 0;
        ObjFull = false;
        isCorrect = false;
        inCorrect = false;
        FullMovie.enabled = false;
        inCorrectVideoOrder = 0;
        isStage1 = false;
        isStage2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        AllocationStageVideo();

        for (int i = 0; i < SaveUserOrder.Length; i++)
        {
            if (SaveUserOrder[i] == null)
                ObjFull = false;
            else
                ObjFull = true;
        }

        if (ObjFull && Input.GetKeyDown(KeyCode.E))      // E를 눌렀을 때 풀일 때만 실행
        {
            CompareOrder();
        }

        if (isCorrect == false && inCorrect == true)     // 틀렸을 때 영상 순서대로 재생 구문
        {
            // 여기에다가 영상 사이즈를 화면에 맞추는 함수 넣기
            StretchVideo();

            inCorrectOrder[inCorrectVideoOrder].enabled = true;

            if (inCorrectOrder[inCorrectVideoOrder].isPlaying)
            {
                GameControlManager.GetInstance().m_bIsOnGame = false;

                if ((ulong)inCorrectOrder[inCorrectVideoOrder].frame >= inCorrectOrder[inCorrectVideoOrder].frameCount - 2)
                {
                    inCorrectOrder[inCorrectVideoOrder].enabled = false;
                    // 여기에다가 영상 사이즈 되돌리는 함수 넣기
                    ReStretchVideo();

                    inCorrectVideoOrder++;

                    if (inCorrectVideoOrder == 4)    // 4개의 영상 재생이 모두 끝나면
                    {
                        inCorrectVideoOrder = 0;
                        ResetUserOrder();
                        inCorrect = false;          // 조건에 안들어오게

                        //// 여기에다가 영상 사이즈 되돌리는 함수 넣기
                        //ReStretchVideo();

                        GameControlManager.GetInstance().m_bIsOnGame = true;

                        CancleClick();
                        return;
                    }
                }
            }
        }
    }

    public void CompareOrder()      // 버튼을 클릭하면 실행되는 함수
    {
        for (int i = 0; i < CorrectPositionOrder.Length; i++)       // 4개의 배열에 인자가 있는지 먼저 검사
        {
            if (SaveUserOrder[i] == null)       // 없는 곳이 있다면 배열 다 비우고 리턴
            {
                for (int j = 0; j < inCorrectOrder.Length; j++)
                {
                    inCorrectOrder[j] = null;
                    CorrectCount = 0;
                }
                return;
            }
        }

        for (int i = 0; i < CorrectPositionOrder.Length; i++)
        {
            if (CorrectPositionOrder[i].name != SaveUserOrder[i].name)   // 오브젝트의 순서가 같지 않다면(틀렸다면)
            {
                isCorrect = false;
                inCorrect = true;
            }

            else if (CorrectPositionOrder[i].name == SaveUserOrder[i].name)     // 오브젝트의 순서가 같다면(else if로 사용)
            {
                CorrectCount++;
                if (CorrectCount == CorrectPositionOrder.Length)
                {
                    isCorrect = true;
                    inCorrect = false;
                }
            }
        }

        if (isCorrect && !inCorrect)      // 맞으면 영상 재생
        {
            FullMovie.enabled = true;
            ResetUserOrder();

            //Character.enabled = false;
            CancleClick();
        }
    }

    public void ResetUserOrder()        // 유저의 배열만
    {
        // 여기서 포지션 잡고 리셋 (널이 아니면 -> Length 까지 비교를 하되, null이 아니면으로 수정)
        for (int i = 0; i < SaveUserOrder.Length; i++)
        {
            if (SaveUserOrder[i] != null)
            {
                SaveUserOrder[i].gameObject.transform.position =
                    SaveUserOrder[i].gameObject.GetComponent<YSM_Inventory_Item>().ItemTransform.position;
            }
        }

        for (int i = 0; i < SaveUserOrder.Length; i++)      // 여기가 배열 초기화하는곳
        {
            SaveUserOrder[i] = null;
        }

        OrderCount = 0;
    }

    public void CancleClick()
    {
        for (int i = 0; i < CorrectPositionOrder.Length; i++)       // 4개의 배열에 모두 인자가 있는지 검사
        {
            inCorrectOrder[i] = null;
            CorrectCount = 0;
        }

        OrderCount = 0;
    }

    /// <summary>
    /// 틀렸을 때 or 인게임 용
    /// </summary>
    public void StretchVideo()
    {
        inCorrectOrderCamera[inCorrectVideoOrder].rect = new Rect(0f, 0f, 1.0f, 1.0f);
        inCorrectOrder[inCorrectVideoOrder].aspectRatio = VideoAspectRatio.Stretch;
    }

    /// <summary>
    /// 퍼즐 창 UI용
    /// </summary>
    public void ReStretchVideo()
    {
        inCorrectOrderCamera[inCorrectVideoOrder].rect = new Rect(0.72f, 0.055f, 0.26f, 0.345f);
        inCorrectOrder[inCorrectVideoOrder].aspectRatio = VideoAspectRatio.Stretch;
    }

    /// <summary>
    /// 스테이지 별로 비디오를 할당
    /// </summary>
    public void AllocationStageVideo()
    {
        if (GameControlManager.GetInstance().curStage == StageInfo.Stage1 && isStage1 == false)
        {
            CorrectPositionOrder[0] = GameObject.Find("Stage 1_Frame");
            CorrectPositionOrder[1] = GameObject.Find("Stage 1_Glass");
            CorrectPositionOrder[2] = GameObject.Find("Stage 1_Belt");
            CorrectPositionOrder[3] = GameObject.Find("Stage 1_Cross");

            FullMovie = GameObject.Find("Stage 1_SuccessMovie").GetComponent<VideoPlayer>();

            isStage1 = true;
            isStage2 = false;
        }

        else if (GameControlManager.GetInstance().curStage == StageInfo.Stage2 && isStage2 == false)
        {
            CorrectPositionOrder[0] = GameObject.Find("Stage 2_Blade");
            CorrectPositionOrder[1] = GameObject.Find("Stage 2_Drawing");
            CorrectPositionOrder[2] = GameObject.Find("Stage 2_Ring");
            CorrectPositionOrder[3] = GameObject.Find("Stage 2_Wallet");

            FullMovie = GameObject.Find("Stage 2_SuccessMovie").GetComponent<VideoPlayer>();

            isStage2 = true;
            isStage1 = false;
        }
    }
}
