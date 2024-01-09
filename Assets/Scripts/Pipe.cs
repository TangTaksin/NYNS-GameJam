using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour // is a Pipe
{
    // has mouths
    PipeMouth[] mouths;
    Collider2D col2d;

    public void Start()
    {
        TryGetComponent<Collider2D>(out col2d);
    }

    public void SetCollider(bool state)
    {
        col2d.enabled = state;
    }

    //can Rotate
    void Rotate()
    {
        
    }
}
