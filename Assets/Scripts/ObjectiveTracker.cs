using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTracker : MonoBehaviour
{
    [SerializeField] MissionCondition mainMission;
    [SerializeField] TextMeshProUGUI allUnusedPipeTxt, remainingGoalPipetxt;

    Pipe[] allPipe;

    int unflowCount, goalPipeCount;

    private void Start()
    {
        allPipe = FindObjectsByType<Pipe>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        UpdatePipeStatus();
    }

    private void UpdatePipeStatus()
    {
        unflowCount = 0;

        foreach (Pipe pipe in allPipe)
        {
            if (!pipe.GetFlow())
            {
                unflowCount++;
            }
        }

        goalPipeCount = 0;

        foreach (Pipe pipe in mainMission.GetPipes())
        {
            if (!pipe.GetFlow())
            {
                goalPipeCount++;
            }
        }

        if (allUnusedPipeTxt)
            allUnusedPipeTxt.text = unflowCount.ToString();
        if (remainingGoalPipetxt)
            remainingGoalPipetxt.text = goalPipeCount.ToString();
    }
}
