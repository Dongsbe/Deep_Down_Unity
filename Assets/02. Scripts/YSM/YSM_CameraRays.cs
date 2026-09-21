using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;


public class YSM_CameraRays : MonoBehaviour
{

    public RaycastHit rayHit;
    public Ray ray;
    public float MAX_RAY_DISTANCE;
    
    public Image KeyImg;
    public Image AimImg;
    public GameObject Char_go;
    public GameObject UI_go;
    public GameObject UI2_go;
    public GameObject[] Explain_go;
    public GameObject PuzzleUI_go;

    public bool isCancle;

    Camera CharCam;
    public Camera mainCamera;
    public Camera UI3D_Cam;
    public Camera UI2D_Cam;

    private FirstPersonController Character;

    public Explain Explain_Index;

    public YSM_test_Prefab InstanceFlag;
    public YSM_IsOrderCorrect isVideoPlay;        // ----- 일단 킵
    public YSM_Inventory_Item ResetItemPosition;
    public YSM_BrainIsFull BrainReset;

    /// <summary>
    /// 손관련 오브젝트, 현재는 필요없음.
    /// </summary>
    public Transform PickTransform;
    HandCreate HandTest;
    public Transform Hand;

    public GameObject ToiletArea;
    public GameObject XrayArea;

    public VideoPlayer Stage2EndVid;
    public VideoPlayer CatchVid;
    public GameObject PatientObj;

    GameObject TempObj = null;
    Transform TempTns = null;

    VideoPlayer[] MemoryVid = new VideoPlayer[8];
    

    Light[] Lightlist = new Light[9];
    bool[] isRun = new bool[6];
    MeshRenderer MirrorObj;
    MeshRenderer MirrorObj2;
    Animator Ani;
    Transform Blood;
    Transform Ring;
    float speed = 0.1f;
    bool isBlood;
    int count;
    int LightSta;
    bool isSt; // 지울거


    // 20180920 기억 영상 한 번은 보고 그 다음에 스킵 가능하도록 만드는 bool형 변수
    public bool[] AlreadyLook_MemoryVid = new bool[8];

    void Start()
    {
        InitVar();
        
        KeyImg = GameObject.Find("Key_E").GetComponent<Image>();
        AimImg = GameObject.Find("Aim").GetComponent<Image>();

        CharCam = Char_go.GetComponent<Camera>();
        UI3D_Cam = UI_go.GetComponent<Camera>();
        UI2D_Cam = UI2_go.GetComponent<Camera>();

        UI3D_Cam.enabled = false;
        UI2D_Cam.enabled = false;
        CharCam.enabled = true;

        isCancle = false;

        //동현
        isBlood = false;
        count = 0;
        LightSta = 0;
        isSt = true;

        for (int i = 0; i < 6; i++)
            isRun[i] = false;

        Lightlist[0] = GameObject.Find("PublicLight1").GetComponent<Light>();
        Lightlist[1] = GameObject.Find("PublicLight2").GetComponent<Light>();
        Lightlist[2] = GameObject.Find("PublicLight3").GetComponent<Light>();
        Lightlist[3] = GameObject.Find("PublicLight4").GetComponent<Light>();
        Lightlist[4] = GameObject.Find("PublicLight5").GetComponent<Light>();
        Lightlist[5] = GameObject.Find("HospitalLight1").GetComponent<Light>();
        Lightlist[6] = GameObject.Find("HospitalLight2").GetComponent<Light>();
        Lightlist[7] = GameObject.Find("XrayLight").GetComponent<Light>();
        Lightlist[8] = GameObject.Find("HandsLight").GetComponent<Light>();

        Character = this.gameObject.GetComponent<FirstPersonController>();

        MirrorObj = GameObject.Find("Mirror1Blood").GetComponent<MeshRenderer>();
        MirrorObj2 = GameObject.Find("Mirror3YourFault").GetComponent<MeshRenderer>();
        Blood = GameObject.Find("BloodEffect").GetComponent<Transform>();
        Ring = GameObject.Find("RingMemory").GetComponent<Transform>();
        Ani = GameObject.Find("Mirror2").GetComponent<Animator>();
        Ani.SetBool("BreakGlass", false);

        // 1 스테이지의 기억영상 4개 오브젝트 가져오기 
        MemoryVid[0] = GameObject.Find("Cross_memory_Start").GetComponent<VideoPlayer>();
        MemoryVid[1] = GameObject.Find("glass_memory_ver001 (1)").GetComponent<VideoPlayer>();
        MemoryVid[2] = GameObject.Find("memory_frame").GetComponent<VideoPlayer>();
        MemoryVid[3] = GameObject.Find("belt_memory").GetComponent<VideoPlayer>();

        // 2 스테이지의 기억영상 4개 오브젝트 가져오기 (S02_M03 은 무슨 오브젝트인지 확인을 못했음)
        MemoryVid[4] = GameObject.Find("RingMemory").GetComponent<VideoPlayer>();
        MemoryVid[5] = GameObject.Find("WalletMemory").GetComponent<VideoPlayer>();
        MemoryVid[6] = GameObject.Find("BladeMemory").GetComponent<VideoPlayer>();
        MemoryVid[7] = GameObject.Find("PaperMemory").GetComponent<VideoPlayer>();

        ToiletArea.SetActive(false);
        XrayArea.SetActive(true);

        PatientObj.SetActive(false);
    }

    void Stage2EndScene()
    {
        //Patient Revil 영상 재생.

        //플레이어 움직임 삭제 이후 모든 플레이어 화면은 자동으로 진행

        //플레이어가 자동으로 뒤를 보게함.-->페이션트쪽 욕조를 보게끔

        //페이션트가 욕조에서 나와 플레이어를 향해 돌아본뒤 비명을 지름

        //위 모든 과정이 끝나면 플레이어는 움직이고 페이션트는 추격해옴.
        UI2D_Cam.enabled = true;
        UI3D_Cam.enabled = true;

        Character.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameControlManager.GetInstance().m_bIsOnGame = false;

        if(GameControlManager.GetInstance().Stage2VidClose == false)
        {
            CharCam.enabled = false;
            PatientObj.SetActive(true);
        }

        if (Stage2EndVid.enabled == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            Debug.Log((ulong)Stage2EndVid.frame >= Stage2EndVid.frameCount - 3);
            if ((ulong)Stage2EndVid.frame >= Stage2EndVid.frameCount - 3)
            {
                CharCam.enabled = true;

                //페이션트 --> 캐릭터
                Vector3 pos = PatientObj.transform.localPosition;
                Vector3 tar = this.gameObject.transform.localPosition;
                Vector3 angle = new Vector3(0, 0, Mathf.Atan2(tar.x - pos.x, tar.z - pos.z) * 180 / Mathf.PI);

                iTween.RotateAdd(PatientObj, iTween.Hash("y", angle.magnitude, "easeType", iTween.EaseType.easeInOutElastic, "speed", 100.0f));

                //캐릭터 --> 페이션트
                Vector3 pos2 = this.gameObject.transform.localPosition;
                Vector3 tar2 = PatientObj.transform.localPosition;
                Vector3 angle2 = new Vector3(0, 0, Mathf.Atan2(tar2.z - pos2.z, tar2.x - pos2.x) * 180 / Mathf.PI);

                iTween.RotateAdd(this.gameObject, iTween.Hash("y", -angle2.magnitude, "easeType", iTween.EaseType.easeInOutElastic, "speed", 40.0f));
                Stage2EndVid.enabled = false;
                GameControlManager.GetInstance().Stage2VidClose = true;
            }
        }
    }

    float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.x - _from.x, _to.z - _from.z) * 180 / Mathf.PI;
    }

    void Update()
    {

        if(GameControlManager.GetInstance().EndingVid.enabled == true)
        {
            AimImg.enabled = false;
        }


        if (GameControlManager.GetInstance().curStage == StageInfo.Stage2)
        {
            if(GameControlManager.GetInstance().IsStageClear)
            {
                if (GameControlManager.GetInstance().Stage2VidClose == false)
                {
                    Stage2EndScene();
                }

                else
                {
                    if (Stage2EndVid.enabled == false)
                    {
                        if (this.GetComponent<iTween>() == null)
                        {
                            if (PatientObj.GetComponent<iTween>() == null)
                            {
                                UI2D_Cam.enabled = false;
                                UI3D_Cam.enabled = false;
                                Character.enabled = true;
                                CharCam.enabled = true;
                                Character.m_MouseLook.Init(Character.transform, CharCam.transform);
                                GameControlManager.GetInstance().m_bIsOnGame = true;

                                if(GameControlManager.GetInstance().PatientSound == false)
                                {
                                    GameSoundManager.GetInstance().GetAudioSource(PatientObj.GetComponent<AudioSource>());
                                    GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.PatienScrim);
                                    GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.PatienBGM);
                                    GameControlManager.GetInstance().PatientSound = true;
                                    PatientObj.GetComponent<PatientController>().enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        if(GameControlManager.GetInstance().isCatch == true)
        {
            CatchVid.enabled = true;
        }

        if (CatchVid.enabled == true)
        {
            if ((ulong)CatchVid.frame >= CatchVid.frameCount - 3)
            {
                PatientObj.SetActive(false);
                CatchVid.enabled = false;
            }
        }

        CameraRay();
        

        if (Input.GetKeyDown(KeyCode.Escape)) { }
        //CancleOnClick();

        if (isVideoPlay.isCorrect)
        {       // 비디오가 플레이 중이라면 ----- 일단 킵(원하는대로는 됨)
            Character.enabled = false;
            AimImg.enabled = false;
        }

        if (isBlood && count < 40)
        {
            Blood.localPosition = new Vector3(Blood.localPosition.x, Blood.localPosition.y + speed * Time.deltaTime, Blood.localPosition.z);
            Ring.localPosition = new Vector3(Ring.localPosition.x, Ring.localPosition.y + speed * Time.deltaTime, Ring.localPosition.z);
            count++;
        }

        if (isVideoPlay.FullMovie.isPlaying)        // 풀 영상 재생 종료
        {
            if ((ulong)isVideoPlay.FullMovie.frame >= isVideoPlay.FullMovie.frameCount - 3)
            {
                TempObj = GameObject.Find("UI Root");
                TempTns = TempObj.transform.Find("Movie");
                TempTns.gameObject.SetActive(false);

                isVideoPlay.FullMovie.enabled = false;
                isVideoPlay.isCorrect = false;
                Character.enabled = true;
                GameControlManager.GetInstance().IsStageClear = true;
                GameControlManager.GetInstance().IsStageObjClear = false;

                if(GameControlManager.GetInstance().curStage == StageInfo.Stage2)
                {
                    if(GameControlManager.GetInstance().IsStageClear == true)
                    {
                        Stage2EndVid.enabled = true;
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.K) && isSt)
        {
            isRun[0] = false;
            isRun[(int)LightState.StartHospitalRoom] = true;
            LightSta = 2;
            StartCoroutine("LightOn");
            isSt = false;
            XrayArea.SetActive(true);
            GameControlManager.GetInstance().XraySound = true;
        }

        for (int i = 0; i < MemoryVid.Length; i++)      // 기억 영상 재생 파트
        {
            // 보고 있을 때 이미 본 영상이라면 스킵가능하게
            if (MemoryVid[i].isPlaying)     // MemoryVid 가 플레이중인지 검사
            {
                AimImg.enabled = false;
                // 영상 처음 보면 스킵 불가능
                if ((ulong)MemoryVid[i].frame >= MemoryVid[i].frameCount - 1/* || Input.GetKeyDown(KeyCode.T)*/)
                {
                    // 20180920 여기부터하기

                    UI2D_Cam.enabled = false;
                    UI3D_Cam.enabled = false;
                    CharCam.enabled = true;
                    KeyImg.enabled = true;
                    MemoryVid[i].enabled = false;

                    Character.enabled = true;

                    // 20180918 시작 방 십자가 확인 후 진행 가능하도록 수정
                    if (GameControlManager.GetInstance().Stage1StartRoom == true)
                    {
                        // 시작방 자물쇠의 태그가 Untagged 에서 Puzzle(3D) 로 바뀌면서 Image 가 뜸
                        GameObject.Find("StartLock").tag = "Puzzle";
                    }

                    // 처음 다 보고 나면 본 영상에 해당하는 Bool 변수를 true로 만들어서 다음부터는 스킵 가능하게
                    AlreadyLook_MemoryVid[i] = true;

                    if (!MemoryVid[4].enabled && AlreadyLook_MemoryVid[4] && !isRun[(int)LightState.NullLight]) // 반지
                    {
                        Ani.SetBool("BreakGlass", true);
                        isRun[(int)LightState.NullLight] = true;

                        GameSoundManager.GetInstance().GetAudioSource(MemoryVid[4].gameObject.GetComponent<AudioSource>());
                        GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.GetRing);
                    }

                    if (!MemoryVid[5].enabled && AlreadyLook_MemoryVid[5] && !isRun[(int)LightState.PublicToilet]) // 지갑
                    {
                        for (int j = 0; j < 5; j++)
                            Lightlist[j].enabled = false;
                        isRun[(int)LightState.PublicToilet] = true;
                        GameControlManager.GetInstance().isTolletCloseDoor = true;

                        GameSoundManager.GetInstance().GetAudioSource(MemoryVid[5].gameObject.GetComponent<AudioSource>());
                        GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.GetWallet1);
                        LightSta = 1;
                        StartCoroutine("LightOn");
                    }

                    if (!MemoryVid[7].enabled && AlreadyLook_MemoryVid[7] && !isRun[(int)LightState.StartHospitalRoom]) // xray
                    {
                        isRun[0] = false;
                        isRun[(int)LightState.StartHospitalRoom] = true;
                        LightSta = 2;
                        StartCoroutine("LightOn");
                    }
                }

                // 영상 두 번 이상 볼 때 부터 스킵 가능하게
                else if (AlreadyLook_MemoryVid[i] == true && Input.GetKeyDown(KeyCode.T))
                {
                    UI2D_Cam.enabled = false;
                    UI3D_Cam.enabled = false;
                    CharCam.enabled = true;
                    KeyImg.enabled = true;
                    MemoryVid[i].enabled = false;
                    Character.enabled = true;
                }
            }
        }
    }

    void CameraRay()
    {
        ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));

        if(GameControlManager.GetInstance().m_bIsOnGame == false)
        {
            Character.enabled = false;
            AimImg.enabled = false;
        }

        else
        {
            Character.enabled = true;
            AimImg.enabled = true;
        }

        if (UI2D_Cam.enabled == true || UI3D_Cam.enabled == true)
        {
            GameControlManager.GetInstance().m_bIsOnGame = false;

            ///자물쇠 해결하면 자동으로 없앰
            if(GameControlManager.GetInstance().isLockOpen == true)
            {
                DisappearExplain();

                GameControlManager.GetInstance().isLockOpen = false;
            }

            // 원래는 시작방의 동영상이 재생 중이 아닐 때에 스프라이트가 뜨게 하는 것이었음(생략 불가능하게)
            // (MemoryVid.enabled 가 false 였을 때 true가 되어서 진입)
            // MemoryVid 가 배열로 바뀜 -> 전부 아닐 때 들어왔던 거니까 마찬 가지로 전부 아닐 때 진입하게 하면 됨
            if (!MemoryVid[0].enabled && !MemoryVid[1].enabled && !MemoryVid[2].enabled && !MemoryVid[3].enabled
                && !MemoryVid[4].enabled && !MemoryVid[5].enabled && !MemoryVid[6].enabled && !MemoryVid[7].enabled)
            {
                if(GameControlManager.GetInstance().IsStageClear == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }


                AppearExplain();

                if (Input.GetKeyDown(KeyCode.E))     // UI끄기
                {
                    //DisappearExplain();
                    isVideoPlay.CompareOrder();
                    isVideoPlay.ResetUserOrder();    // 유저의 배열을 리셋

                    DisappearExplain();

                    //Character.enabled = true;
                    PuzzleUI_go.SetActive(false);

                    for (int i = 0; i < BrainReset.IsFull.Length; i++)
                    {
                        BrainReset.IsFull[i] = false;
                    }

                    GameControlManager.GetInstance().m_bIsOnGame = true;
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    //ResetItemPosition.ResetItemPosition();      // 0905 추가
                    //DisappearExplain();

                    DisappearExplain();

                    isVideoPlay.ResetUserOrder();
                    isVideoPlay.CancleClick();
                    PuzzleUI_go.SetActive(false);

                    for (int i = 0; i < BrainReset.IsFull.Length; i++)
                    {
                        BrainReset.IsFull[i] = false;
                    }

                    GameControlManager.GetInstance().m_bIsOnGame = true;
                }
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // 게임 플레이 중에는 이 상태로 진입함
        // 20180918 오브젝트와의 상호작용에 필요한 거리 수정(MAX_RAY_DISTANCE 를 5에서 2로)
        // 이미지가 사라지지 않는 버그가 있어서 코드도 추가함
        else
        {
            GameControlManager.GetInstance().m_bIsOnGame = true;

            // 레이를 그릴 때(레이에 닿는 물체가 있다면)
            if (Physics.Raycast(ray, out rayHit, MAX_RAY_DISTANCE, 1 << 0) && !(isVideoPlay.isCorrect | isVideoPlay.inCorrect))
            {
                Debug.DrawLine(ray.origin, rayHit.point, Color.red);

                //2D UI 화면만
                if (rayHit.collider.gameObject.tag == "Scenario")
                {
                    KeyImg.enabled = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isCancle = false;                  // Cancle 버튼을 눌렀는지 여부

                        UI2D_Cam.enabled = true;
                        CharCam.enabled = false;
                        KeyImg.enabled = false;

                        Character.m_MouseLook.m_cursorIsLocked = false;
                        Character.m_MouseLook.lockCursor = false;
                        Character.enabled = false;

                        GameControlManager.GetInstance().m_bIsOnGame = false;
                    }
                }

                //3D UI
                else if (rayHit.collider.gameObject.tag == "Puzzle")
                {
                    KeyImg.enabled = true;

                    if (Input.GetKeyDown(KeyCode.E) )
                    {
                        HitColliderName();

                        isCancle = false;                  // Cancle 버튼을 눌렀는지 여부

                        UI3D_Cam.enabled = true;
                        CharCam.enabled = false;
                        KeyImg.enabled = false;

                        InstanceFlag.InstanceFlag = true;
                        InstanceFlag.InstanceClone();

                        Character.m_MouseLook.m_cursorIsLocked = false;
                        Character.m_MouseLook.lockCursor = false;
                        Character.enabled = false;

                        GameControlManager.GetInstance().m_bIsOnGame = false;
                    }
                }
                
                //조건넘겨주는곳
                else if(rayHit.collider.gameObject.tag == "Material")
                {
                    KeyImg.enabled = true;
                    
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        KeyImg.enabled = false;

                        // 머테리얼 바꾸는 코드
                        if(rayHit.collider.gameObject.name == "Mirror1Blood")
                        {
                            MirrorObj.enabled = true;
                            GameSoundManager.GetInstance().GetAudioSource(rayHit.collider.gameObject.GetComponent<AudioSource>());
                            GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.Mirror);
                        }
                    }
                }

                else if (rayHit.collider.gameObject.tag == "Blood")
                {
                    KeyImg.enabled = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        KeyImg.enabled = false;
                        isBlood = true;
                        GameSoundManager.GetInstance().GetAudioSource(rayHit.collider.gameObject.GetComponent<AudioSource>());
                        GameSoundManager.GetInstance().PlayTriggerSound(HorrorSoundTrigger.Blood);
                    }
                }

                //PDS
                else if (rayHit.collider.gameObject.tag == "Object")
                {
                    KeyImg.enabled = true;
                    EventObject target = rayHit.collider.gameObject.GetComponent<EventObject>();

                    if (GameControlManager.GetInstance().m_bIsCabinetOpen == true)
                    {
                        target.enabled = true;
                    }


                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //HandTest = rayHit.collider.gameObject.GetComponent<HandCreate>();

                        if (target.enabled == false)
                        {
                            return;
                        }

                        switch (target.ObjectAction)
                        {
                            case EventAction.Move:
                                {
                                    if (target.GetComponent<iTween>() == null)
                                    {
                                        if (target.IsUsed == false)
                                        {
                                            iTween.MoveAdd(target.gameObject, iTween.Hash(target.ActingBy.ToString(), target.ActionScale, "easeType", "linear", "speed", 1.5));
                                            GameSoundManager.GetInstance().GetAudioSource(target.GetComponent<AudioSource>());
                                            GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.Drawer_Open);
                                        }

                                        else
                                        {
                                            iTween.MoveAdd(target.gameObject, iTween.Hash(target.ActingBy.ToString(), -target.ActionScale, "easeType", "linear"));
                                            GameSoundManager.GetInstance().GetAudioSource(target.GetComponent<AudioSource>());
                                            GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.Drawer_Close);
                                        }

                                        target.IsUsed = !target.IsUsed;
                                    }
                                }
                                break;
                            case EventAction.Rotate:
                                {
                                    if (target.enabled == true)
                                    {
                                        if (target.IsUsed == false)
                                        {
                                            if (target.ActionScale != 0)
                                            {
                                                iTween.RotateTo(target.gameObject, iTween.Hash("y", 90 * target.ActionScale, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                GameSoundManager.GetInstance().GetAudioSource(target.GetComponent<AudioSource>());
                                                GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.Cabinet_Open);
                                                target.IsUsed = true;
                                            }
                                        }

                                        else
                                        {
                                            if (target.ActionScale != 0)
                                            {
                                                iTween.RotateTo(target.gameObject, iTween.Hash("y", 0 * -target.ActionScale, "easeType", iTween.EaseType.easeInOutQuart, "loopType", iTween.LoopType.none, "speed", 50.0f));
                                                GameSoundManager.GetInstance().PlayObjectSound(ObjectSound.Cabinet_Close);
                                                target.IsUsed = false;
                                            }
                                        }
                                    }
                                }
                                break;

                            case EventAction.Pick:
                                {
                                    //target.gameObject.transform.SetParent(PickTransform);
                                    //target.gameObject.transform.localPosition = Vector3.zero;
                                    //target.gameObject.transform.rotation = new Quaternion(90, 0, 0, 1.0f);
                                }
                                break;
                        }
                    }
                }

                // 1. 0906_하나라서 name으로 체크, 여러개라면 tag로 체크하는게 이득일 것 같음
                // 2. 스프라이트는 그냥 지워버리는지 체크
                else if (rayHit.collider.gameObject.tag == "memory")
                {
                    KeyImg.enabled = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameControlManager.GetInstance().m_bIsOnGame = false;

                        UI2D_Cam.enabled = true;
                        UI3D_Cam.enabled = true;
                        CharCam.enabled = false;
                        KeyImg.enabled = false;

                        // memory 태그의 오브젝트 중 어떤 오브젝트인지 찾기
                        //MemoryVid.enabled = true;
                        rayHit.collider.gameObject.GetComponent<VideoPlayer>().enabled = true;

                        Character.enabled = false;

                        // MemoryVid 의 인덱스에 따라 값 바꿔주기
                        // 현재 0, 4, 5, 6, 7
                        if (MemoryVid[0].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage1StartRoom = true;
                        }

                        if (MemoryVid[1].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage1Glass = true;
                        }

                        if (MemoryVid[2].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage1Frame = true;
                        }

                        if (MemoryVid[3].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage1Secret = true;
                        }

                        if (MemoryVid[4].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage2Ring = true;
                        }

                        if (MemoryVid[5].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage2Wallet = true;
                        }

                        if (MemoryVid[6].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage2Blade = true;
                        }

                        if (MemoryVid[7].name == rayHit.collider.gameObject.GetComponent<VideoPlayer>().name)
                        {
                            GameControlManager.GetInstance().Stage2Paper = true;
                        }
                    }
                }

                // 20180918 오브젝트를 다 주워도 StageDoor 에는 이미지가 출력되지 않음
                else if (rayHit.collider.gameObject.tag == "StageDoor")
                {
                    KeyImg.enabled = false;
                    if (GameControlManager.GetInstance().IsStageObjClear == true)
                    {
                        // 스테이지 클리어 안됬을 때에만 퍼즐 출력
                        if (Input.GetKeyDown(KeyCode.E) && GameControlManager.GetInstance().IsStageClear == false)
                        {
                            //KeyImg.enabled = false;
                            AppearPuzzleUI();
                            rayHit.collider.gameObject.GetComponent<EventObject>().DrState = DoorState.Open;
                        }
                    }
                }

                else
                {
                    // 20180918 디버깅용 GreenLine (위의 tag 가 전부 아니라면 초록색 라인으로 표시함)
                    Debug.DrawLine(ray.origin, rayHit.point, Color.green);
                    KeyImg.enabled = false;
                }
            }

            else
            {
                KeyImg.enabled = false;
            }
        }
    }
    
    void InitVar()
    {
        rayHit = new RaycastHit();
        // 20180918 MAX_RAY_DISTANCE 5.0f 에서 1.7f 로 줄임
        MAX_RAY_DISTANCE = 1.7f;
    }

    //public void CancleOnClick()
    //{
    //    UI3D_Cam.enabled = false;
    //    UI2D_Cam.enabled = false;
    //    CharCam.enabled = true;
    //    KeyImg.enabled = true;
    //    isCancle = true;
    //    Character.GetComponent<FirstPersonController>().enabled = true;
    //    Character.m_MouseLook.m_cursorIsLocked = true;
    //}

    public void HitColliderName()
    {
        if (rayHit.collider.gameObject.name == "frame_memory")
        {
            InstanceFlag.Index = ScenarioItem.Library_Scenario_frame_memory;
        }

        else if (rayHit.collider.gameObject.name == "Lock")     // 0805 "Lock" Test
        {
            InstanceFlag.Index = ScenarioItem.TutorialRoom_Lock;
        }

        else if (rayHit.collider.gameObject.name == "StartLock")
        {
            InstanceFlag.Index = ScenarioItem.TutorialRoom_StartLock;
        }

        else if (rayHit.collider.gameObject.name == "LightButton")       // 0806 "LightButton" Test 
        {
            InstanceFlag.Index = ScenarioItem.TutorialRoom_LightButton;
        }

        else if (rayHit.collider.gameObject.name == "ClockTrigger")       // 0806 "LightButton" Test 
        {
            InstanceFlag.Index = ScenarioItem.Clock;
        }
    }

    // cross_frame이 빠지면서 End로 대체했음
    public void AppearExplain()
    {
        if (rayHit.collider.gameObject.name == "Geo_openbook")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Geo_openbook)
                {
                    Explain_go[(int)Explain.Geo_openbook].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "FatherWill")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.FatherWill)
                {
                    Explain_go[(int)Explain.FatherWill].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Diagnosis")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Diagnosis)
                {
                    Explain_go[(int)Explain.Diagnosis].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Secret_Book")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Secret_Book)
                {
                    Explain_go[(int)Explain.Secret_Book].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }

            GameControlManager.GetInstance().Stage1Secret = true;
        }

        if (rayHit.collider.gameObject.name == "FamilyPhoto")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.FamilyPhoto)
                {
                    Explain_go[(int)Explain.FamilyPhoto].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "NamePlateRe")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.NamePlateRe)
                {
                    Explain_go[(int)Explain.NamePlateRe].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "ClockClue")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.ClockClue)
                {
                    Explain_go[(int)Explain.ClockClue].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "DoctorPhoto")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.DoctorPhoto)
                {
                    Explain_go[(int)Explain.DoctorPhoto].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "cross_frame")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.cross_frame)
                {
                    Explain_go[(int)Explain.cross_frame].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Paper_S")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Paper_S)
                {
                    Explain_go[(int)Explain.Paper_S].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Paper_N")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Paper_N)
                {
                    Explain_go[(int)Explain.Paper_N].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Paper_O")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Paper_O)
                {
                    Explain_go[(int)Explain.Paper_O].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        ///                 ///
        ///     Stage 2     ///
        ///                 ///
        if (rayHit.collider.gameObject.name == "Shower")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Shower)
                {
                    Explain_go[(int)Explain.Shower].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "WeddingFrame")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.WeddingFrame)
                {
                    Explain_go[(int)Explain.WeddingFrame].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Ticket")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Ticket)
                {
                    Explain_go[(int)Explain.Ticket].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Letter")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Letter)
                {
                    Explain_go[(int)Explain.Letter].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        // 20180919 원래 PBDoor02 였지만 하위 오브젝트인 TolietDoor2 에 콜라이더가 있어서 그걸로 수정. 나머지도 똑같이 하면 됨
        if (rayHit.collider.gameObject.name == "ToiletDoor2")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.ToiletDoor2)
                {
                    Explain_go[(int)Explain.ToiletDoor2].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Trolley_06")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Trolley_06)
                {
                    Explain_go[(int)Explain.Trolley_06].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Xray")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Xray)
                {
                    Explain_go[(int)Explain.Xray].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Medicine01")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Medicine01)
                {
                    Explain_go[(int)Explain.Medicine01].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Medicine02")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Medicine02)
                {
                    Explain_go[(int)Explain.Medicine02].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Medicine03")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Medicine03)
                {
                    Explain_go[(int)Explain.Medicine03].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Anatomy2")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Anatomy2)
                {
                    Explain_go[(int)Explain.Anatomy2].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Dignosis01")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Dignosis01)
                {
                    Explain_go[(int)Explain.Dignosis01].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Dignosis02")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.DIgnosis02)
                {
                    Explain_go[(int)Explain.DIgnosis02].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Xray_Paper1")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Xray_Paper1)
                {
                    Explain_go[(int)Explain.Xray_Paper1].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Xray_Paper2")
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Xray_Paper2)
                {
                    Explain_go[(int)Explain.Xray_Paper2].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        // 20180929 마네킹 머리, 가슴, 다리 총 3개
        if (rayHit.collider.gameObject.name == "Manequinn_Head")     // (하이어라키 이름 수정, head_dontmove -> Manequinn_Head)
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Mannequin01)
                {
                    Explain_go[(int)Explain.Mannequin01].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Manequinn_Mid")     // (하이어라키 이름 수정, head_dontmove -> Manequinn_Head)
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Mannequin02)
                {
                    Explain_go[(int)Explain.Mannequin02].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }

        if (rayHit.collider.gameObject.name == "Manequinn_Bottom")     // (하이어라키 이름 수정, head_dontmove -> Manequinn_Head)
        {
            for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
            {
                if (i == (int)Explain.Mannequin03)
                {
                    Explain_go[(int)Explain.Mannequin03].SetActive(true);
                    continue;
                }
                Explain_go[i].SetActive(false);
            }
        }
    }


    public void DisappearExplain()
    {
        for (int i = (int)Explain.Geo_openbook; i < (int)Explain.End; i++)
        {
            if (Explain_go[i].gameObject.activeSelf == true)
            {
                Explain_go[i].SetActive(false);
            }
        }

        // UI 창 뿐만 아니라 퍼즐룸의 UI까지 종료
        InstanceFlag.DestroyClone();        // 클론 삭제

        UI3D_Cam.enabled = false;           //  
        UI2D_Cam.enabled = false;           // UI 캠 끄고
        CharCam.enabled = true;             // 캐릭터 캠 켜고
        PuzzleUI_go.SetActive(false);

        // 퍼즐 화면이 아니라면 Movie 찾아서 종료(20180930 원래 논리 상 true가 맞음)
        // False로 들어와서 False로 나감

        //if (GameControlManager.GetInstance().m_bIsOnGame == true)
        //{
        //    TempObj = GameObject.Find("UI Root");
        //    TempTns = TempObj.transform.Find("Movie");
        //    TempTns.gameObject.SetActive(false);
        //}

        if (isVideoPlay.isCorrect == false && isVideoPlay.inCorrect == false)
        {
            TempObj = GameObject.Find("UI Root");
            TempTns = TempObj.transform.Find("Movie");
            TempTns.gameObject.SetActive(false);
        }

        Character.m_MouseLook.lockCursor = true;        // 마우스 묶고
        Character.m_MouseLook.m_cursorIsLocked = true;  // 마우스 묶고
        Cursor.lockState = CursorLockMode.Locked; // 한번 더 묶고
        Character.enabled = true;

        GameControlManager.GetInstance().m_bIsOnGame = true;
    }

    public void AppearPuzzleUI()
    {
        // 영상 재생 관련해서 하이어라키의 movie를 따로 빼놓았음
        // 처음엔 off로 해두고 여기에서 on으로 바꾸도록 함
        GameObject TempObj = null;
        Transform TempTns = null;

        UI3D_Cam.enabled = true;
        UI2D_Cam.enabled = true;
        CharCam.enabled = false;
        isCancle = false;
        PuzzleUI_go.SetActive(true);

        // 퍼즐룸 UI 뜨면서 Movie를 켬
        TempObj = GameObject.Find("UI Root");
        TempTns = TempObj.transform.Find("Movie");
        TempTns.gameObject.SetActive(true);


        Character.m_MouseLook.m_cursorIsLocked = false;
        Character.m_MouseLook.lockCursor = false;

        Character.enabled = false;        // FirstPersonController On / Off
        GameControlManager.GetInstance().m_bIsOnGame = false;
    }

    IEnumerator LightOn()
    {
        switch(LightSta)
        {
            case (int)LightState.PublicToilet:
                {
                    yield return new WaitForSeconds(5.0f);
                    for (int i = 0; i < 5; i++)
                        Lightlist[i].enabled = true;
                    GameControlManager.GetInstance().isTolletOpenDoor = true;
                    MirrorObj2.enabled = true;
                    LightSta = 6;
                    StartCoroutine("LightOn");
                }
                break;
            case (int)LightState.StartHospitalRoom:
                {
                    //yield return new WaitForSeconds(2.0f);
                    yield return new WaitForSeconds(1.0f);
                    for (int i = 5; i < 7; i++)
                        Lightlist[i].enabled = false;
                    LightSta = 3;
                    StartCoroutine("LightOn");
                }
                break;
            case (int)LightState.HospitalRoom:
                {
                    //yield return new WaitForSeconds(6.0f);
                    yield return new WaitForSeconds(4.0f);
                    Lightlist[7].enabled = false;
                    LightSta = 4;
                    StartCoroutine("LightOn");
                }
                break;
            case (int)LightState.XrayLight:
                {
                    //yield return new WaitForSeconds(5.0f);
                    yield return new WaitForSeconds(3.0f);
                    GameControlManager.GetInstance().isXrayMaterailChange = true;
                    Lightlist[8].enabled = true;
                    LightSta = 5;
                    GameControlManager.GetInstance().XrayGhost = true;
                    StartCoroutine("LightOn");
                }
                break;
            case (int)LightState.HandsLight:
                {
                    yield return new WaitForSeconds(6.0f);
                    for (int i = 5; i < 8; i++)
                        Lightlist[i].enabled = true;
                    GameControlManager.GetInstance().isXrayMaterailChange = false;
                    Lightlist[8].enabled = false;
                }
                break;
            case (int)LightState.TolletDoor:
                {
                    yield return new WaitForSeconds(2.0f);

                    GameControlManager.GetInstance().isPublicTolletClear = true;
                }
                break;
            default:
                break;
        }


    }
}

