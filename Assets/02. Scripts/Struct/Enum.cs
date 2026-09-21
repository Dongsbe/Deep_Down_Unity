using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventCondition
{
    Unknown,
    State,
    Target,
    Condition
}

public enum EventObj
{
    Unknown,
    Character,
    Door,
    Item,
    JumpScare,
    Area,
    Puzzle
}

public enum StageInfo
{
    StageTutorial,
    Stage1,
    Stage2,
    StageCount
}

public enum EventAction
{
    Unknown,
    Animation,
    Appear,
    Move,
    Rotate,
    Pick,
    Drop
}

public enum TargetPos
{
    Unknown,
    X,
    Y,
    Z
}

public enum DoorState
{
    Open,
    Close,
    Locked,
    Lock,
    Stage2_PrivateToilet,
    Stage2_PublicToilet,
    Unknown,
}

public enum ObjectSound
{
    Cabinet_Open,
    Cabinet_Close,
    BookShelf,
    Drawer_Open,
    Drawer_Close,
    WheelChairMove,
    WheelChairCrash,
    PatientRise,
    PatienScrim,
    LockSpinSound,
    LockOpenSound,
    SwitchSound,
    PatienBGM,
}

public enum HorrorSoundTrigger
{
    WalkMan,
    EnterLib,
    JumpScare_Hand,
    DropFrame,
    Mirror,
    MirrorBGM,
    Blood,
    GetRing,
    GetWallet1,
    GetWallet2,
    GetWallet3,
    GetWallet4,
    Mannequinn,
    XrayLightOff,
}

public enum AreaSound
{
    MainMenu,
    Tutorial,
    Stage1,
    Stage1_Hidden_BGM,
    Stage1_Hidden_BABY,
}

public enum ScenarioItem        // 0805 Item_MakePrefabs 하이나키의 오브젝트들 추가
{
    StartRoom_Scenario_Book,
    Library_Scenario_frame_memory,
    Library_Scenario_Paper_S,
    Library_Scenario_Paper_N,
    Library_Scenario_Paper_O,
    SecretRoom_Scenario_Secert_Book,
    Test_TutorialRoom_Lock,
    Test_TutorialRoom_LightButton,
    TutorialRoom_Lock,
    TutorialRoom_LightButton,
    TutorialRoom_StartLock,
    Clock,

    // 원형 Library_Scenario_frame_memory,
}

public enum Explain             // 0805 Item_MakrPrefabs 하이나키의 오브젝트를 위해 수정
{
    // 1 Stage
    Geo_openbook,
    Paper_S,
    Paper_N,
    Paper_O,
    Secret_Book,
    FatherWill,
    Diagnosis,
    FamilyPhoto,
    NamePlateRe,
    ClockClue,
    DoctorPhoto,
    cross_frame,
    //MagicSquare,

    // 2 Stage (20180919 추가)
    Shower,
    WeddingFrame,
    Ticket,
    Letter,
    //PBdoor02,
    ToiletDoor2,
    Trolley_06,
    Xray,
    Medicine01,
    Medicine02,
    Medicine03,
    Anatomy2,
    Dignosis01,
    DIgnosis02,
    Xray_Paper1,
    Xray_Paper2,
    Mannequin01,
    Mannequin02,
    Mannequin03,
    End
}

public enum PuzzleRoomBrainPosition
{
    Brain1_Position,
    Brain2_Position,
    Brain3_Position,
    Brain4_Position,
    Brain5_Position
}

public enum PuzzleRoomItem
{
    Item1,
    Item2,
    Item3,
    Item4,
    Item5
}

public enum RotateHinge
{
    Unknown,
    Start,
    Main,
}

public enum LightState
{
    NullLight,
    PublicToilet,
    StartHospitalRoom,
    HospitalRoom,
    XrayLight,
    HandsLight,
    TolletDoor
}
