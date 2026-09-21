using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class item
{
    public int ID;
    public string Name;
    public string Des;

    public item(int id, string name, string des)
    {
        ID = id;
        Name = name;
        Des = des;
    }
}

public class JsonManager : MonoBehaviour {

    public List<item> ItemList = new List<item>();          // 오리진 아이템 리스트
    public List<item> MyInventory = new List<item>();       // 내가 소지한 아이템 리스트

    // 파일 저장 (아이템이나 캐릭터 저장 컨셉)
    void Start () {
        ItemList.Add(new item(0, "Sword", "this is Sword"));
        ItemList.Add(new item(1, "바지", "바지다"));
        ItemList.Add(new item(2, "포션", "포션이다"));
        ItemList.Add(new item(3, "창", "창이다"));
        ItemList.Add(new item(4, "Sword", "this is Sword"));
        ItemList.Add(new item(5, "바지", "바지다"));
        ItemList.Add(new item(6, "포션", "포션이다"));
        ItemList.Add(new item(7, "창", "창이다"));
    }

    public void SaveFunc()
    {
        //for(int i=0; i < ItemList.Count;i++)
        //{
        //    Debug.Log(ItemList[i].ID);
        //}

        JsonData ItemJson = JsonMapper.ToJson(ItemList);
        
        // 새 파일을 만들고 이미 파일이 있으면 거기다가 파일을 덮어 쓴다.
        File.WriteAllText(Application.dataPath + "/02. Scripts/YSM/JsonTut/Resource/ItemData.json", ItemJson.ToString());
    }

    public void LoadFunc()
    {
        StartCoroutine(LoadCo());
    }

    IEnumerator LoadCo()
    {
        // Load 함으로써 JsonString 에다가 파일 저장
        string JsonString = File.ReadAllText(Application.dataPath + "/02. Scripts/YSM/JsonTut/Resource/ItemData.json");

        //Debug.Log(JsonString);

        JsonData ItemData = JsonMapper.ToObject(JsonString);

        ParsingJsonItem(ItemData);
        yield return null;
    }

    private void ParsingJsonItem(JsonData name)
    {
        for(int i = 0; i < name.Count; i++)
        {
            //Debug.Log(name[i]["ID"]);
            string TmpID = (name[i]["ID"].ToString());

            for(int j = 0; j < ItemList.Count; j++)
            {
                if (TmpID == ItemList[j].ID.ToString())
                {
                    MyInventory.Add(ItemList[i]);
                }
            }
        }

        for (int i = 0; i < MyInventory.Count; i++)
        {
            Debug.Log(MyInventory[i].Name);
        }
    }
}
