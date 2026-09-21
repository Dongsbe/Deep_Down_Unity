using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(EventObject))]

public class EventEditorInspector : Editor {

    public override void OnInspectorGUI()
    {
        EventObject eventObject = target as EventObject;        

        eventObject.ObjectType = (EventObj)EditorGUILayout.EnumPopup("EventType", eventObject.ObjectType);

        switch (eventObject.ObjectType)
        {
            case EventObj.Unknown:
                {
                    Unknown(eventObject);
                }
                break;

            case EventObj.Character:
                {
                    Character(eventObject);
                }
                break;

            case EventObj.Door:
                {
                    Door(eventObject);
                }
                break;

            case EventObj.Item:
                {
                    Item(eventObject);
                }
                break;

            case EventObj.JumpScare:
                {
                    JumpScare(eventObject);
                }
                break;

            case EventObj.Area:
                {
                    Area(eventObject);
                }
                break;

            case EventObj.Puzzle:
                {
                    Puzzle(eventObject);
                }
                break;
        }
    }

    public void Initialize(EventObject eventObject)
    {
        if (eventObject.gameObject.GetComponent<BoxCollider>())
        {
            GameObject.DestroyImmediate(eventObject.gameObject.GetComponent<BoxCollider>());
        }

        if (eventObject.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>())
        {
            GameObject.DestroyImmediate(eventObject.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>());
        }

        if (eventObject.gameObject.GetComponent<AudioSource>())
        {
            if(eventObject.ObjectType != EventObj.Character)
            {
                GameObject.DestroyImmediate(eventObject.gameObject.GetComponent<AudioSource>());
            }
        }
    }

    public void Unknown(EventObject eventObject)
    {
        Initialize(eventObject);
    }

    public void Character(EventObject eventObject)
    {
        Initialize(eventObject);
    }

    public void Door(EventObject eventObject)
    {
        eventObject.DrState = (DoorState)EditorGUILayout.EnumPopup("DoorState", eventObject.DrState);

        if (!eventObject.gameObject.GetComponent<BoxCollider>())
        {
            eventObject.gameObject.AddComponent<BoxCollider>();
            eventObject.gameObject.GetComponent<BoxCollider>().size = new Vector3(eventObject.gameObject.GetComponent<BoxCollider>().size.x, eventObject.gameObject.GetComponent<BoxCollider>().size.y, 2.0f);

            if (eventObject.gameObject.GetComponent<BoxCollider>().isTrigger == false)
            {
                eventObject.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }

        if (!eventObject.gameObject.GetComponent<AudioSource>())
        {
            eventObject.gameObject.AddComponent<AudioSource>();
        }
    }

    public void Area(EventObject eventObject)
    {
        if (!eventObject.gameObject.GetComponent<BoxCollider>())
        {
            eventObject.gameObject.AddComponent<BoxCollider>();

            if (eventObject.gameObject.GetComponent<BoxCollider>().isTrigger == false)
            {
                eventObject.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }

    public void Item(EventObject eventObject)
    {
        eventObject.ObjectAction = (EventAction)EditorGUILayout.EnumPopup("EventAction", eventObject.ObjectAction);
        eventObject.IsVideoUsed = EditorGUILayout.Toggle("Video Used?", eventObject.IsVideoUsed);
        eventObject.TargetObject = (GameObject)EditorGUILayout.ObjectField("TargetObject", eventObject.TargetObject, typeof(GameObject), true);

        if (!eventObject.gameObject.GetComponent<BoxCollider>())
        {
            eventObject.gameObject.AddComponent<BoxCollider>();

            if(eventObject.gameObject.GetComponent<BoxCollider>().isTrigger == false)
            {
                eventObject.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }

        switch (eventObject.ObjectAction)
        {
            case EventAction.Move:
            case EventAction.Rotate:
                {
                    eventObject.ActingBy = (TargetPos)EditorGUILayout.EnumPopup("ActingBy", eventObject.ActingBy);
                    eventObject.ActionScale = (float)EditorGUILayout.FloatField("ActionScale",eventObject.ActionScale);
                }
                break;
            case EventAction.Drop:
                {
                    if (!eventObject.gameObject.GetComponent<BoxCollider>())
                    {
                        eventObject.gameObject.AddComponent<BoxCollider>();
                        eventObject.gameObject.GetComponent<BoxCollider>().size = new Vector3(eventObject.gameObject.GetComponent<BoxCollider>().size.x, 0.1f, eventObject.gameObject.GetComponent<BoxCollider>().size.z);
                    }

                    if(!eventObject.gameObject.GetComponent<AudioSource>())
                    {
                        eventObject.gameObject.AddComponent<AudioSource>();
                    }
                }
                break;
        }


        if (eventObject.IsVideoUsed)
        {
            if (!eventObject.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>())
            {
                eventObject.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
            }
        }

        else
        {
            if (eventObject.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>())
            {
                GameObject.DestroyImmediate(eventObject.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>());
            }
        }
    }

    public void Puzzle(EventObject eventObject)
    {

    }

    public void JumpScare(EventObject eventObject)
    {
        if (!eventObject.gameObject.GetComponent<BoxCollider>())
        {
            eventObject.gameObject.AddComponent<BoxCollider>();
            eventObject.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
