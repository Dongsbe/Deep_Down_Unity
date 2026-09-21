using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class YSM_Inventory_Item : MonoBehaviour {

    //public UISprite _sprite;
    public UISprite _message1;
    public Transform ItemTransform;
    //public Transform BrainPosition;
    public VideoPlayer _movie;
    public Camera _Camera;
    public GameObject Item_go;

    public bool brainCheck;

    //public bool[] isFull = new bool[5];
    public GameObject[] BrainPosition_go = new GameObject[4];
    public YSM_BrainIsFull isFull;  // bool 형 4개의 배열

    public YSM_IsOrderCorrect UserOrder;

    public int UserPositionCnt;

    // Use this for initialization
    void Start() {
        _message1.enabled = false;
        brainCheck = false;
        _movie.enabled = false;
        UserPositionCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C))
        {
            ResetItemPosition();
        }
    }

    // _message1 과 _movie 를 띄울지 말지를 결정(마우슷 갖다대는 중의 여부에 따라)
    void OnHover(bool isHover)
    {
        _message1.enabled = (isHover) ? true : false;
        _movie.enabled = (isHover) ? true : false;
        //Debug.Log("_movie.enabled = " + _movie.enabled);
    }

    // 드래그 중
    void OnDrag(Vector2 delta)
    {
        if (Screen.width == 1280)
        {
            Item_go.transform.localPosition += (Vector3)delta * 1.5f;
        }
        else if (Screen.width == 1920)
        {
            Item_go.transform.localPosition += (Vector3)delta;
        }
    }

    // 드래그 종료, 여기서 비어있는 지를 비교하면 될 듯
    void OnDragEnd()
    {
        // 'BrainPosition 이 비어있다면'을 어떻게 체크해야 하는가?
        // Item의 좌표가 Brain_Position와 일치한다. -> 오브젝트가 들어가있다.
        // Item의 좌표가 Brain_Position과 일치하지 않는다. -> 오브젝트가 들어가 있지 않다.
        // ex) Item3 을 처음으로 드래그해서 Brain1의 포지션에 넣으려 한다면..

        if (brainCheck == true)     // 스프라이트가 Brain 안에 있다면
        {
            int j = 0;
            int distanceCount = 0;
            float shortest = 1.0f;
            for (int i = 0; i < 4; i++)     // i 는 for문 돌리기용
            {
                // 여기서 거리 계산 후 가장 짧은 것 선택
                for (distanceCount = 0; distanceCount < 4; distanceCount++)     // 0부터 4까지 돌면서
                {
                    //// 짧으면 대입하기
                    if (shortest > Vector2.Distance(Item_go.transform.position, BrainPosition_go[distanceCount].transform.position))
                    {
                        if (isFull.IsFull[distanceCount] == true)
                            continue;
                        shortest = Vector2.Distance(Item_go.transform.position, BrainPosition_go[distanceCount].transform.position);
                    }
                }
                distanceCount = 0;

                // 여기서 distanceCount 를 돌면서 shortest와 맞는 포지션인지 검사
                for (; j < 4; j++, distanceCount++)
                {
                    if (shortest == Vector2.Distance(this.gameObject.transform.position, BrainPosition_go[distanceCount].transform.position) && isFull.IsFull[j] == false)
                    {
                        //Debug.Log("Run Insert Object");
                        this.gameObject.transform.position = BrainPosition_go[j].transform.position;    // 맞으면 대입

                        UserPositionCnt = j;
                        isFull.IsFull[j] = true;
                    }
                }

                j = 0;
                distanceCount = 0;
            }

            UserOrder.SaveUserOrder[UserPositionCnt] = this.gameObject;
            UserOrder.inCorrectOrder[UserPositionCnt] = _movie;        // 오브젝트 넣은 순서대로 영상 집어넣기
            UserOrder.inCorrectOrderCamera[UserPositionCnt] = _Camera;
            UserOrder.OrderCount++;
        }

        else if (brainCheck == false)       // 스프라이트가 Brain 안에 없다면
        {
            // 아이템의 포지션을 인벤토리의 포지션으로 (제자리로)
            Item_go.gameObject.transform.position = ItemTransform.gameObject.transform.position;
        }
    }

    public void ResetItemPosition()
    {
        this.gameObject.transform.position = ItemTransform.gameObject.transform.position;
        UserOrder.OrderCount = 0;

        for(int i = 0; i < isFull.IsFull.Length; i++)
        {
            isFull.IsFull[i] = false;
        }
    }

    // OnTriggerEnter와 OnTriggerExit는 항상 실행 가능(조건만 맞다면)
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brain")
        {
            //Debug.Log("braincheck = true");
            brainCheck = true;
        }

        //YSM_BrainCollideAnything test = other.GetComponent<YSM_BrainCollideAnything>();
        //if (other.tag == "BrainPos")
        //{
        //    //Debug.Log("braincheck = true");
        //    brainCheck = true;
        //    for(int i = 0; i < 4; i++)
        //    {
        //        if (test.gameObject.transform.position == test.MyCol[i].transform.position)
        //        {
        //            test.Checked[i] = true;
        //        }
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Brain")
        {
            //Debug.Log("braincheck = false");
            brainCheck = false;
        }

        //YSM_BrainCollideAnything test = other.GetComponent<YSM_BrainCollideAnything>();
        //if (other.tag == "BrainPos")
        //{
        //    //Debug.Log("braincheck = true");
        //    brainCheck = true;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (test.gameObject.transform.position == test.MyCol[i].transform.position)
        //        {
        //            test.Checked[i] = false;
        //        }
        //    }
        //}

        if (other.name == "Brain1_Position")
        {
            isFull.IsFull[0] = false;
        }
        if (other.name == "Brain2_Position")
        {
            isFull.IsFull[1] = false;
        }
        if (other.name == "Brain3_Position")
        {
            isFull.IsFull[2] = false;
        }
        if (other.name == "Brain4_Position")
        {
            isFull.IsFull[3] = false;
        }
    }
}
