using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameControlManager : MonoBehaviour
{
    private static GameControlManager instance; //게임매니저 싱글톤화
    public GameObject Character;                //게임 캐릭터
    public StageInfo curStage;                  //현재 스테이지

    public int StageMaxCount;                   //스테이지 개수
    public List<Transform> StartingPoint;       //스테이지 시작지점
    public List<GameObject> StageDoorObject;    //스테이지 입구
    public bool IsStageObjClear = false;           //스테이지오브젝트 클리어확인
    public bool IsStageClear = false;           //스테이지 클리어확인

    public Image fade;                          //UI에 들어갈 FadeInOut용 이미지
    float fades = 1.0f;                         //fade 이미지 Alpha값
    float time = 0.0f;                          //fade시 사용될 시간
    bool fadeinout = false;                     //fade완료 확인

    public bool m_bIsOnGame;                    //인게임 = true or UI화면 = false

    bool m_bIsAutoClose;
    bool m_bIsAutoClose2;
    public bool m_bIsCabinetOpen;

    //Stage1치트
    public bool Stage1Cheat;
    //Stage2치트
    public bool Stage2VidClose;
    public bool PatientSound;
    public bool PatienChase;

    public VideoPlayer EndingVid;

    public bool isCatch;



    /// <summary>
    /// Stage1 Hint
    /// </summary>
    [HideInInspector]
    public bool m_bIsClockOn;
    public GameObject Target;
    public bool Stage1StartRoom;
    public bool Stage1Glass;
    public bool Stage1Frame;
    public bool Stage1Secret;

    /// <summary>
    /// Sage2 Scenario and Memory, Now Only Memory
    /// </summary>
    public bool Stage2Blade;
    public bool Stage2Paper;
    public bool Stage2Wallet;
    public bool Stage2Ring;


    // 20180920 기억 영상 한 번은 보고 그 다음에 스킵 가능하도록 만드는 bool형 변수
    public bool AlreadyLook_MemoryVid_1;
    public bool AlreadyLook_MemoryVid_2;
    public bool AlreadyLook_MemoryVid_3;
    public bool AlreadyLook_MemoryVid_4;

    //동현
    public bool isXrayMaterailChange;
    public bool isPublicTolletClear;
    public bool isStage2OpenDoor;
    public bool isTolletOpenDoor;
    public bool isTolletCloseDoor;
    public bool dooropentest;

    public bool isLockOpen;

    public bool XraySound;
    public bool XrayGhost;

    //게임 매니저 싱글톤
    public static GameControlManager GetInstance()
    {
        if (!instance)
        {
            instance = (GameControlManager)GameObject.FindObjectOfType(typeof(GameControlManager));
            if (!instance)
            {
                Debug.LogError("There needs to be one active GameControl script on a GameObject in your scene.");
            }
        }
        return instance;
    }


    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);          //씬 변경시에도 계속 유지시키기 위한 함수.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;            // 프레임 60으로 고정

        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);

        // 컬링 관련 세팅은 다음주에(09/14~)
    }

    // Use this for initialization
    void Start()
    {
        curStage = StageInfo.StageTutorial;
        StageMaxCount = (int)StageInfo.StageCount;
        IsStageClear = false;

        m_bIsOnGame = true;

        m_bIsAutoClose = false;
        m_bIsAutoClose2 = false;

        m_bIsClockOn = false;
        m_bIsCabinetOpen = false;
        isXrayMaterailChange = false;
        isPublicTolletClear = false;
        isStage2OpenDoor = false;
        isTolletOpenDoor = false;
        dooropentest = false;
        isTolletCloseDoor = false;

        XraySound = false;
        XrayGhost = false;
        Stage2VidClose = false;
        PatientSound = false;
        PatienChase = false;

        isLockOpen = false;

        EndingVid.enabled = false;

        isCatch = false;
        //Character.transform.position = StartingPoint[(int)stageCount].position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_bIsOnGame == true)
        {
            if (IsStageClear == false)
            {
                switch (curStage)
                {
                    case StageInfo.Stage1:
                        {
                            // 문이 잠겨있지 않다면
                            if (m_bIsAutoClose == false)
                            {
                                if (StageDoorObject[(int)curStage].GetComponent<iTween>() == null)
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(StageDoorObject[(int)curStage].GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().PlayDoorSound(DoorState.Close);
                                    // 20180918 1스테이지에 진입하지 못하는 버그가 있어서 문 스피드 수정 (50.0f -> 100.0f)
                                    iTween.RotateAdd(StageDoorObject[(int)curStage].gameObject, iTween.Hash("y", -90, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 100.0f));

                                    // Open에서 Lock으로 바꿈
                                    StageDoorObject[(int)curStage].GetComponent<EventObject>().DrState = DoorState.Lock;
                                    m_bIsAutoClose = true;
                                }
                            }
                        }
                        break;

                    // 20181001 YSM 2스테이지 코드 작성
                    case StageInfo.Stage2:
                        {

                        }
                        break;

                    case StageInfo.StageTutorial:
                        {

                        }
                        break;
                }
            }

            if (IsStageClear == true)
            {
                // 20181001 YSM
                if (StageDoorObject[(int)curStage + 1] == null)
                {
                    return;
                }

                StageDoorObject[(int)curStage].GetComponent<EventObject>().DrState = DoorState.Open;
                StageDoorObject[(int)curStage + 1].GetComponent<EventObject>().DrState = DoorState.Open;
            }

            if (m_bIsClockOn == true)
            {
                m_bIsClockOn = false;
                iTween.MoveBy(Target.gameObject, iTween.Hash("y", 1.2, "easeType", "linear", "speed", 0.1f));
                GameSoundManager.GetInstance().GetAudioSource(Target.GetComponent<AudioSource>());
                GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.BookShelf);
            }

            if (GameControlManager.GetInstance().curStage == StageInfo.Stage1)
            {
                if (Stage1StartRoom)
                {
                    if (Stage1Frame)
                    {
                        if (Stage1Glass)
                        {
                            if (Stage1Secret)
                            {
                                IsStageObjClear = true;
                            }
                        }
                    }
                }
            }

            // 0930 YSM 2스테이지 퍼즐룸 가능 여부 체크
            if (GameControlManager.GetInstance().curStage == StageInfo.Stage2)
            {
                if (Stage2Blade)
                {
                    if (Stage2Paper)
                    {
                        if (Stage2Wallet)
                        {
                            if (Stage2Ring)
                            {
                                IsStageObjClear = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ChangeStage()
    {
        //Stage전환 FadeIn FadeOut
        FadeIn();

        FadeOut();
    }

    void FadeIn()
    {
        //점점 어두워짐
        time += Time.deltaTime;

        if (fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0.0f;
        }

        else if (fades <= 0.0f)
        {
            fadeinout = !fadeinout;
        }
    }

    void FadeOut()
    {
        //점점 밝아짐
        time += Time.deltaTime;

        if (fades < 1.0f && time >= 0.1f)
        {
            fades += 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0.0f;
        }

        else if (fades >= 1.0f)
        {
            fadeinout = !fadeinout;
        }
    }       
}
