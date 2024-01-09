using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeControl : MonoBehaviour
{
    private float[] rotations = { 0, 90, 180, 270 };
    public float[] correctRotation;
    [SerializeField] bool isCorrect = false;

    int PossibleRots = 1;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (PossibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isCorrect = true;
                gameManager.correctMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isCorrect = true;
                gameManager.correctMove();
            }

        }

    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));

        if (PossibleRots > 1)
        {

            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1] && isCorrect == false)
            {
                isCorrect = true;
                gameManager.correctMove();

            }
            else if (isCorrect == true)
            {
                isCorrect = false;
                gameManager.wrongMove();

            }

        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0] && isCorrect == false)
            {
                isCorrect = true;
                gameManager.correctMove();

            }
            else if (isCorrect == true)
            {
                isCorrect = false;
                gameManager.wrongMove();

            }

        }
    }
}
