using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMovement : MonoBehaviour
{
    public static IEnumerator MoveBuildingToLeftAndLoadScene(RectTransform building, Vector2 target, float duration)
    {
        // Define the target position (move to the left)
        Vector3 targetPosition = building.anchoredPosition - target; // Adjust the values as needed

        // Get the starting position
        Vector3 startPosition = building.anchoredPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = EaseInOut(elapsedTime / duration);
            building.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the building is at the exact target position
        building.anchoredPosition = targetPosition;

        // Load the LevelSelect scene
        SceneManager.LoadScene("LevelSelect");
    }

    public static IEnumerator MoveBuildingToRightAndLoadScene(RectTransform building, Vector2 target, float duration)
    {
        // Define the target position (move to the left)
        Vector3 targetPosition = building.anchoredPosition - target; // Adjust the values as needed

        // Get the starting position
        Vector3 startPosition = building.anchoredPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = EaseInOut(elapsedTime / duration);
            building.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the building is at the exact target position
        building.anchoredPosition = targetPosition;

        // Load the LevelSelect scene
        SceneManager.LoadScene("MainMenu");
    }

    public static IEnumerator CharacterMovement(RectTransform _character, Vector2 target, float duration, int levelIndex)
    {
        Vector3 targetPosition = _character.anchoredPosition + target; // Adjust the values as needed

        // Get the starting position
        Vector3 startPosition = _character.anchoredPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = EaseInOut(elapsedTime / duration);
            _character.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the building is at the exact target position
        _character.anchoredPosition = targetPosition;
        SceneManager.LoadScene("Level" + levelIndex);

    }

    private static float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
