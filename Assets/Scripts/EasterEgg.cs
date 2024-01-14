using System;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] private Toggle[] windows;
    [SerializeField] private int[] passwords;

    [SerializeField] private Image imageToChange;
    [SerializeField] private Image character;

    [SerializeField] private AudioClip easterEggSound;
    [SerializeField] private AudioSource audioSource;

    private bool hasTriggeredEasterEgg = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeEasterEgg();
    }

    // Method to initialize Easter egg functionality
    void InitializeEasterEgg()
    {
        // Subscribe to the onValueChanged event of each Toggle
        foreach (Toggle window in windows)
        {
            window.onValueChanged.AddListener(delegate { OnToggleValueChanged(window); });
        }
    }

    // Method called when a Toggle's value changes
    void OnToggleValueChanged(Toggle changedToggle)
    {
        // Check if the Easter Egg has already been triggered
        if (!hasTriggeredEasterEgg)
        {
            // Check which window was toggled
            int toggledIndex = Array.IndexOf(windows, changedToggle);

            if (toggledIndex != -1)
            {
                Debug.Log("Window " + toggledIndex + " toggled: " + changedToggle.isOn);

                // Check if only the specific windows indicated by passwords[0], passwords[1], and passwords[2] are ON
                bool isCorrectCombination = true;

                for (int i = 0; i < windows.Length; i++)
                {
                    // Check if the specified window is ON
                    if (Array.IndexOf(passwords, i) != -1)
                    {
                        isCorrectCombination &= windows[i].isOn;
                    }
                    else
                    {
                        // Check that windows other than the specified ones are OFF
                        isCorrectCombination &= !windows[i].isOn;
                    }
                }

                if (isCorrectCombination)
                {
                    DoSomeThing();
                }
            }
        }
    }

    public void DoSomeThing()
    {
        Debug.Log("Easter Egg");

        if (imageToChange != null)
        {
            character.sprite = imageToChange.sprite;
        }

        if (audioSource != null && easterEggSound != null)
        {
            audioSource.PlayOneShot(easterEggSound);
        }

        // Set the flag to true to indicate that the Easter Egg has been triggered
        hasTriggeredEasterEgg = true;
    }
}
