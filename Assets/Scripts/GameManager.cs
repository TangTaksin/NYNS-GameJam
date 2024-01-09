using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField] int totalPipes = 0;

    int correctedPipes;

    private void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        Debug.Log(correctedPipes);
    }

    public void CorrectMove()
    {
        correctedPipes += 1;
        Debug.Log("Correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You Win!");
            ActivateBoxColliders();
        }

    }

    public void WrongMove()
    {
        correctedPipes -= 1;
        Debug.Log("Wrong Move");
        if (correctedPipes < -1)
        {
            correctedPipes = 0;

        }

    }

    void ActivateBoxColliders()
    {
        foreach (GameObject pipe in Pipes)
        {
            BoxCollider2D collider = pipe.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}
