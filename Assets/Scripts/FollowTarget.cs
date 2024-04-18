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
        startPosition = transform.position;//记录自己的初始位置
    }

    // Update is called once per frame
    void Update()
    {
        //this.nav.SetDestination(this.target.position);
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance < chaseRange) 
        {
            //如果玩家在范围内，追踪玩家
            nav.SetDestination(target.position);
        }
        else if(distance > loseRange) 
        {
            //如果玩家超出范围，返回初始位置
            nav.SetDestination(startPosition);
        }
    }
}
