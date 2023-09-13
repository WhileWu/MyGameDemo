using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    NavMeshAgent agent;
    List<Transform> wayPoints;
    public Transform waypoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoRepath = true;
        wayPoints = new List<Transform>();
    }

    void Update()
    {
        GetMousePoint();
        Patrol();
    }

    void GetMousePoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                //Vector3 pos = hit.point;
                //pos.y = 10;
                //var point = Instantiate(waypoint, pos, Quaternion.identity);
                var point = Instantiate(waypoint, hit.point, Quaternion.identity);
                wayPoints.Add(point);
            }
        }
    }

    int i = 0;
    void Patrol()
    {
        if(wayPoints.Count == 0)
        {
            return;
        }
        //if(i >= wayPoints.Count)
        //{
        //    i = 0;
        //}
        if (!agent.hasPath)
        {
            agent.SetDestination(wayPoints[i].position);
            i = (i + 1) % wayPoints.Count; 
            //if(i + 1 < wayPoints.Count)
            //{
            //    ++i;
            //}
            //else
            //{
            //    i = 0;
            //}
        }
    }

}
