using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class way : MonoBehaviour
{
    NavMeshAgent agent;
    NavMeshPath path;
    LineRenderer lineRenderer;
    Rigidbody rb;
    [SerializeField]
    Transform target;
    bool on = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 3f;
        lineRenderer.endWidth = 3f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.black;
        //agent.SetDestination(target.position);

    }
    void Update()
    {

        if (agent.path.corners.Length > 1&&on) Draw_way();
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (on)
            {
                on = false;
                lineRenderer.positionCount = 0;
            }
            else { agent.SetDestination(target.position); on = true; }
        }

    }
    void Draw_way()
    {
        lineRenderer.positionCount = agent.path.corners.Length;
        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, agent.path.corners[i]);
            //Debug.Log(i+": "+agent.path.corners[i]);
        }
    }

}
