using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearCanvas : MonoBehaviour
{
    public MissionCondition missionCondition;
    public GameObject levelClearPanel;

    private void Start()
    {
        //Time.timeScale = 1;

    }

    private void Update()
    {
        SetPanel(missionCondition.GetMissionState());

    }

    public void SetPanel(bool state)
    {
        levelClearPanel.SetActive(state);
        Time.timeScale = state ? 0 : 1;
    }

}
