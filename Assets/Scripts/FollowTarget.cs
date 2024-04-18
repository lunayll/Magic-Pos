using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent nav;
    public float chaseRange = 10f;
    public float loseRange = 15f;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;//��¼�Լ��ĳ�ʼλ��
    }

    // Update is called once per frame
    void Update()
    {
        //this.nav.SetDestination(this.target.position);
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance < chaseRange) 
        {
            //�������ڷ�Χ�ڣ�׷�����
            nav.SetDestination(target.position);
        }
        else if(distance > loseRange) 
        {
            //�����ҳ�����Χ�����س�ʼλ��
            nav.SetDestination(startPosition);
        }
    }
}
