using DPUtils.Systems.DateTime;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Apple;

[System.Serializable]
public class NPCSchedule
{
    public int hour;
    public Transform destination;
}

public class NPCMovementSchedule : MonoBehaviour
{
    public List<NPCSchedule> schedule = new List<NPCSchedule>();
    private int currentTargetIndex = -1;
    private NPCMovement mover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<NPCMovement>();
        TimeManager.OnDateTimeChanged += CheckSchedule;

    }
    
    private void CheckSchedule(DateTime currentTime)
    {
        for (int i = 0; i < schedule.Count; i++)
        {
            if (schedule[i].hour == currentTime.Hour && i != currentTargetIndex)
            {
                currentTargetIndex = i;
                mover.MoveTo(schedule[i].destination);
            }
        }
    }
}
