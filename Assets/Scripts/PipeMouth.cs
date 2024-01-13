using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMouth : MonoBehaviour
{
    public delegate void ConnectEvent(Pipe pipe);
    public ConnectEvent onConnect;
    public ConnectEvent onDisconnect;
    public ParticleSystem waterParticle;

    Pipe mouthParent;
    bool isConnect;

    private void Start()
    {
        mouthParent = GetComponentInParent<Pipe>();
    }

    private void Update()
    {
        var particleCondition = (mouthParent.GetFlow() && !isConnect);

        waterParticle.gameObject.SetActive(particleCondition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PipeMouth>(out var otherMouth))
        {
            var otherPipe = otherMouth.GetComponentInParent<Pipe>();
            print(otherPipe.name);
            onConnect.Invoke(otherPipe);
            isConnect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PipeMouth>(out var otherMouth))
        {
            var otherPipe = otherMouth.GetComponentInParent<Pipe>();
            onDisconnect.Invoke(otherPipe);
            isConnect = false;
        }
    }

}
