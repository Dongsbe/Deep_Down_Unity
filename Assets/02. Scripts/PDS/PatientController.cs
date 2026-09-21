using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class PatientController : MonoBehaviour {

    public Transform target;
    Vector3 direction;
    float velocity;
    float accelaration;

    private Animator animator;

    NavMeshAgent NMAgent;
    Vector3 prevPos;

    public bool isCatch;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        this.enabled = false;
        NMAgent = GetComponent<NavMeshAgent>();
        isCatch = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameControlManager.GetInstance().m_bIsOnGame)
        {
            if (this.GetComponent<iTween>() == null)
            {
                //MoveToTarget_Position();
                MoveToTarget();
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void MoveToTarget()
    {
        NMAgent.SetDestination(target.gameObject.transform.position);

        //if (Vector3.Distance(this.gameObject.transform.position , prevPos) > 20.0f)
        //{
        //    animator.SetBool("Chase", false);

        //    transform.LookAt(target);
        //}

        //else
        //{
        //    animator.SetBool("Chase", true);
        //}

        if (NMAgent.destination != target.transform.position)
        {
            animator.SetBool("Chase", true);
        }

        else
        {
            animator.SetBool("Chase", false);

            GameControlManager.GetInstance().isCatch = true;
        }

        prevPos = this.gameObject.transform.position;
    }

    void MoveToTarget_Position()
    {
        //캐릭터와 환자몹 사이의 위치를 빼고 단위를 벡터화함.
        direction = (target.position - this.transform.position).normalized;
        Quaternion Rot = Quaternion.identity;

        Rot.eulerAngles = direction;

        //가속도
        accelaration = 0.008f;
        velocity = (velocity + accelaration * Time.deltaTime);

        float distance = Vector3.Distance(target.position, transform.position);
        distance += 5.0f;

        if (distance <= 20.0f)
        {
            animator.SetBool("Chase", true);
            this.transform.position = new Vector3(transform.position.x + (direction.x * velocity), transform.position.y, transform.position.z + (direction.z * velocity));
            //transform.rotation = Quaternion.Slerp(transform.rotation, Rot, Time.deltaTime * 50);
            transform.LookAt(target);
        }

        else
        {
            animator.SetBool("Chase", false);
            velocity = 0.0f;
        }
    }
}
