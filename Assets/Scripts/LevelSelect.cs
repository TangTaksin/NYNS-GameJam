using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private RectTransform character;
    [SerializeField] private float durationLoadScene = 0.5f;
    [SerializeField] private Vector2 target;
    [SerializeField] private int totalLevel = 8;
    [SerializeField] private Button[] levelButton;
    public Image Lock;
    public Image UnLock;

    private void Start()
    {
        UnlockLevel(1);
        UpdateUILevelSelect();
    }

    public void UpdateUILevelSelect()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            int levelNum = i + 1;

            if (levelButton[i] != null && levelButton[i].GetComponent<Button>() != null && levelButton[i].GetComponent<Image>() != null)
            {
                if (levelNum > totalLevel || !IsLevelUnlocked(levelNum))
                {
                    levelButton[i].interactable = false;
                    levelButton[i].GetComponent<Image>().sprite = Lock.sprite;
                }
                else
                {
                    levelButton[i].interactable = true;
                    levelButton[i].GetComponent<Image>().sprite = UnLock.sprite;
                }
            }
            else
            {
                Debug.LogError("One of the required components is null in levelButton[" + i + "]");
            }
        }
    }

    public void loadLevel(int levelIndex)
    {
        StartCoroutine(UIMovement.CharacterMovement(character, target, durationLoadScene, levelIndex));
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 1 && levelIndex < totalLevel)
        {
            Debug.Log("ClearLevel");
            PlayerPrefs.SetInt("Level" + levelIndex + "_Unlocked", 1);
            PlayerPrefs.Save();
            UpdateUILevelSelect();
        }
        else
        {
            Debug.LogWarning("Invalid level index for unlocking: " + levelIndex);
        }
    }

    public void ResetAllLevel()
    {
        PlayerPrefs.DeleteAll();
        UnlockLevel(1);  // Unlock level 1 as the default
        PlayerPrefs.Save();
        UpdateUILevelSelect();
    }

    private bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level" + levelIndex + "_Unlocked", 0) == 1;
    }
}
