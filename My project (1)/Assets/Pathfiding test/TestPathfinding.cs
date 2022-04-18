using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class TestPathfinding : MonoBehaviour
{
    [SerializeField] private Seeker seeker;
    [SerializeField] private Transform Target;
    [SerializeField] private float Velocity = 1f;
    [SerializeField] protected float nextWayPointDistance=0.1f;

    private int currenWayPoint=0;
    private bool reachEndOfPath=false;
    private Path path;
    private void Start()
    {
        InvokeRepeating("CreatPath", 0, 1);
    }
    void Update()
    {
        if (path == null)
            return;
        if(currenWayPoint>path.vectorPath.Count)
        {
            reachEndOfPath=true;
            return;
        }    
        else
            reachEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currenWayPoint] - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction* Velocity * Time.deltaTime;


        float distance = Vector2.Distance(transform.position,path.vectorPath[currenWayPoint]);
        Debug.Log(path.vectorPath[currenWayPoint].ToString()+" "+distance);
        if(distance<nextWayPointDistance)
        {
            currenWayPoint++;
        }    
    }

    private void CreatPath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, Target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
       if(!p.error)
        {
            path = p;
            currenWayPoint = 0;
        }    

      
    }

}
