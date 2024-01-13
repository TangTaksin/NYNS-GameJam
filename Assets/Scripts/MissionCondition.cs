using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCondition : MonoBehaviour
{
    [SerializeField] List<Pipe> GoalPipes;

    bool eventSent;
    public delegate void CompleteEvent();
    public static CompleteEvent onComplete;

    // Wining condition
    // All goal pipes got wihat it what.

    private void Update()
    {
        if (GoalPipes.Count > 0)
        {
            var goalMet = true;

            foreach (var content in GoalPipes)
            {
                if (!content.CheckGoal())
                {
                    goalMet = false;
                    eventSent = false;
                    break;
                }
            }

            if (goalMet && !eventSent)
            {
                onComplete?.Invoke();
                eventSent = true;
                print("Clear!");
            }
        }
    }
}
