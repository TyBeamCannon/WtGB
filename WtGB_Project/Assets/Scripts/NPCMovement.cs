using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float stoppingDistance = 0.2f;
    private NavMeshAgent npc;
    private Transform target;
    private bool isMoving = false;

    private void Awake()
    {
       npc = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if(distance <= stoppingDistance)
            {
                isMoving = false;
                npc.ResetPath();
            }
        }
        
    }

    public void MoveTo(Transform destination)
    {
        target = destination;
        isMoving = true;
        npc.SetDestination(destination.position);
    }
}
