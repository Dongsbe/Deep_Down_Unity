using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSM_test_Prefab : MonoBehaviour {

    public GameObject[] go = null;
    public GameObject clone = null;
    public ScenarioItem Index;

    public YSM_CameraRays Cam;

    public bool InstanceFlag;
    public Camera CamPos;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Cam.rayHit.collider.gameObject.name == "frame_memory")
        //{
        //    InstanceClone();
        //    DestroyClone();
        //}
    }

    public void InstanceClone()
    {
        if (Cam.isCancle == false && Cam.UI3D_Cam.enabled == true && InstanceFlag)
        {
            Vector3 Pos = CamPos.ScreenToWorldPoint(new Vector3(CamPos.pixelWidth/2, CamPos.pixelHeight/2, 2));
            // E키를 눌렀을 때 해당 오브젝트를 Item_MakePrefab 에서 찾고 그 오브젝트를 인스턴스 해야함
            //clone = Instantiate(go[(int)Index], new Vector3(-55.0f, 31.0f, 42.6f), go[(int)Index].transform.rotation);
            clone = Instantiate(go[(int)Index], Pos, go[(int)Index].transform.rotation);
            clone.SetActive(true);
            InstanceFlag = !InstanceFlag;
        }
    }

    public void DestroyClone()
    {
        if(!InstanceFlag)
        {
            Destroy(clone);
        }
        //if (Cam.isCancle == true)
        //{
        //    Destroy(clone);
        //}
    }
}
