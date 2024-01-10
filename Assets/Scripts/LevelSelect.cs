using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private int totalLevel = 8;

    private void Start()
    {
        UnlockLevel(1);
    }

    public void loadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("Level" + levelIndex + "_Unlocked", 1);
        PlayerPrefs.Save();
    }

}
