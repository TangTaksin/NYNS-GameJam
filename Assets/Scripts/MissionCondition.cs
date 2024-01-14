using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCondition : MonoBehaviour
{
    [SerializeField] List<Pipe> GoalPipes;

    public delegate void CompleteEvent();
    public CompleteEvent onComplete;

    bool goalMet = false;

    // Wining condition
    // All goal pipes got wihat it what.

    private void Update()
    {
        if (GoalPipes.Count > 0)
        {
            goalMet = true;

            foreach (var content in GoalPipes)
            {
                if (!content.CheckGoal())
                {
                    goalMet = false;
                    break;
                }
            }
        }
    }

    public bool GetMissionState()
    {
        return goalMet;
    }
}
