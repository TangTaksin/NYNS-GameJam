using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private RectTransform character;
    [SerializeField] private float loadSceneDuration = 0.5f;
    [SerializeField] private Vector2 target;
    [SerializeField] private int totalLevel = 8;
    [SerializeField] private Button[] levelButtons;
    public Image lockImage;
    public Image unlockImage;

    private void Start()
    {
        UnlockLevel(1);
        UpdateLevelSelectUI();
    }

    public void UpdateLevelSelectUI()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNum = i + 1;

            if (levelButtons[i] != null && levelButtons[i].GetComponent<Button>() != null && levelButtons[i].GetComponent<Image>() != null)
            {
                if (levelNum > totalLevel || !IsLevelUnlocked(levelNum))
                {
                    levelButtons[i].interactable = false;
                    levelButtons[i].GetComponent<Image>().sprite = lockImage.sprite;
                }
                else
                {
                    levelButtons[i].interactable = true;
                    levelButtons[i].GetComponent<Image>().sprite = unlockImage.sprite;
                }
            }
            else
            {
                Debug.LogError("One of the required components is null in levelButtons[" + i + "]");
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(UIMovement.CharacterMovement(character, target, loadSceneDuration, levelIndex));
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 1 && levelIndex < totalLevel)
        {
            Debug.Log("ClearLevel");
            PlayerPrefs.SetInt("Level" + levelIndex + "_Unlocked", 1);
            PlayerPrefs.Save();
            UpdateLevelSelectUI();
        }
        else
        {
            Debug.LogWarning("Invalid level index for unlocking: " + levelIndex);
        }
    }

    public void ClearLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void ResetAllLevels()
    {
        PlayerPrefs.DeleteAll();
        UnlockLevel(1);  // Unlock level 1 as the default
        PlayerPrefs.Save();
        UpdateLevelSelectUI();
    }

    private bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt("Level" + levelIndex + "_Unlocked", 0) == 1;
    }
}
