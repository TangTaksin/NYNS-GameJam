using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] private Toggle[] windows;
    [SerializeField] private int[] passwords;

    [SerializeField] private Image imageToChange;
    [SerializeField] private Image character;

    [SerializeField] ParticleSystem particle;

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
            for (int i = 0; i < windows.Length; i++)
            {
                if (windows[i] == changedToggle)
                {
                    Debug.Log("Window " + i + " toggled: " + changedToggle.isOn);

                    if (windows[passwords[0]].isOn && windows[passwords[1]].isOn && windows[passwords[2]].isOn)
                    {
                        DoSomeThing();
                    }

                    break;
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

        particle?.Play();

        // Set the flag to true to indicate that the Easter Egg has been triggered
        hasTriggeredEasterEgg = true;
    }
}
