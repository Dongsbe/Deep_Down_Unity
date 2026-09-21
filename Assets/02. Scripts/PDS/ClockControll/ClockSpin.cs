using UnityEngine;

public class ClockSpin : MonoBehaviour
{
    public Transform target;
    public float speed = .1f;
    Transform mTrans;

    void Start()
    {
        mTrans = transform;
    }


    void OnDrag(Vector2 delta)
    {
        UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;

        if (target != null)
        {
            target.rotation = Quaternion.Euler(0f, 0f, -1.5f * speed) * target.rotation;
        }
        else
        {
            //Base : 0.5f* delta.x * speed
            mTrans.rotation = Quaternion.Euler(0f, 0f, -1.5f * speed) * mTrans.rotation;
        }
    }
}