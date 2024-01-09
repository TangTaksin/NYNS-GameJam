using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeControl : MonoBehaviour
{
    private float[] rotations = { 0, 90, 180, 270 }; // Array of valid pipe rotations
    public float[] correctRotation; // Array to store correct rotations
    [SerializeField] bool isCorrect = false; // Flag to track correctness of the pipe

    int PossibleRots = 1;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length; // Set the number of possible rotations based on the length of correctRotation array

        SetRandomRotation(); // Set a random initial rotation for the pipe

        if (PossibleRots > 1)
        {
            if (IsCorrectRotation(transform.eulerAngles.z))
            {
                isCorrect = true;
                gameManager.CorrectMove();
            }
        }
        else
        {
            if (IsCorrectRotation(transform.eulerAngles.z))
            {
                isCorrect = true;
                gameManager.CorrectMove();
            }
        }
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90)); // Rotate the pipe 90 degrees on mouse click

        // Snap the rotation to the nearest valid rotation
        SnapToNearestRotation();

        if (PossibleRots > 1)
        {
            float normalizedRotation = NormalizeAngle(transform.eulerAngles.z);

            if (IsCorrectRotation(normalizedRotation) && !isCorrect)
            {
                isCorrect = true;
                gameManager.CorrectMove();
            }
            else if (isCorrect)
            {
                isCorrect = false;
                gameManager.WrongMove();
            }
        }
        else
        {
            float normalizedRotation = NormalizeAngle(transform.eulerAngles.z);

            if (IsCorrectRotation(normalizedRotation) && !isCorrect)
            {
                isCorrect = true;
                gameManager.CorrectMove();
            }
            else if (isCorrect)
            {
                isCorrect = false;
                gameManager.WrongMove();
            }
        }
    }

    private void SetRandomRotation()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
    }

    // Normalize angle to be within the range [0, 360)
    private float NormalizeAngle(float angle)
    {
        return (angle % 360 + 360) % 360;
    }

    // Check if the rotated angle is one of the correct rotations
    private bool IsCorrectRotation(float angle)
    {
        foreach (float correctRot in correctRotation)
        {
            if (Mathf.Approximately(angle, correctRot))
            {
                return true;
            }
        }
        return false;
    }

    // Snap the rotation to the nearest valid rotation
    private void SnapToNearestRotation()
    {
        float currentRotation = NormalizeAngle(transform.eulerAngles.z);
        float nearestRotation = rotations[0];

        foreach (float rot in rotations)
        {
            if (Mathf.Abs(rot - currentRotation) < Mathf.Abs(nearestRotation - currentRotation))
            {
                nearestRotation = rot;
            }
        }

        transform.eulerAngles = new Vector3(0, 0, nearestRotation);
    }
}
