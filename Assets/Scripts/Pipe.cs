using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pipe : MonoBehaviour // is a Pipe
{
    // has mouths
    [SerializeField] PipeMouth[] mouths;
    [SerializeField] TextMeshPro text;
    [SerializeField] SpriteRenderer pipeSprite;
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color flowColor = Color.cyan;

    enum PipeType { Start, Passage, End}
    [SerializeField] PipeType pipeType = PipeType.Passage;

    List<Collider2D> colliders = new List<Collider2D>();
    List<Pipe> connectedPipe = new List<Pipe>();
    bool isBeingLift;
    bool hasFlow;
    float angle;

    public delegate void FlowEvent();

    public delegate void CompleteEvent ();
    public static CompleteEvent onComplete;

    public void Start()
    {
        TryGetComponent<SpriteRenderer>(out pipeSprite);

        GetComponentsInChildren<PipeMouth>();

        TryGetComponent<Collider2D>(out var selfCollider);
        colliders.Add(selfCollider);

        foreach (var obj in mouths)
        {
            obj.gameObject.TryGetComponent<Collider2D>(out var mouthCollider);
            colliders.Add(mouthCollider);

            obj.onConnect += AddConnectedPipe;
            obj.onDisconnect += RemoveConnectedPipe;
        }

        if (pipeType == PipeType.Start)
        {
            SetFlow(true);
        }
    }

    private void Update()
    {
        var isTween = LeanTween.isTweening(this.gameObject);
        // Collider will be active when it's <not being lift> AND <Tweening>
        SetCollider(!isBeingLift && !isTween);

        // Will start sending
        if (connectedPipe.Count > 0 || pipeType == PipeType.Start)
            FlowSend();

        FlowUpdate();
    }

    List<Pipe> flowSources = new List<Pipe>();
    //List<Pipe> flowSources;
    
    void AddFlowSource(Pipe sourcePipe)
    {
        if (!(flowSources.Contains(sourcePipe)))
            flowSources.Add(sourcePipe);
    }

    void RemoveFlowSource(Pipe sourcePipe)
    {
        if ((flowSources.Contains(sourcePipe)))
            flowSources.Remove(sourcePipe);
    }

    // check if any of flow sources has flow, if there is any. return true. else false.
    bool CheckAnyFlow()
    {
        var isflow = false;

        foreach(Pipe pipe in flowSources)
        {
            if (pipe.hasFlow)
            {
                isflow = true;
                break;
            }
        }

        return isflow;
    }

    void FlowSend()
    {
        foreach (var pipe in connectedPipe)
        {
            // if the pipe <has flow>,
            // it will send to <all pipe that it did not get flow from>. 
            if (!flowSources.Contains(pipe) && hasFlow)
            {
                pipe.FlowReceive(this);
                print("send flow to : " + pipe);
            }
        }
    }

    public void FlowReceive(Pipe pipe)
    {
        if (pipeType != PipeType.Start)
        {
            AddFlowSource(pipe);
        }

        if (pipeType == PipeType.End)
        {
            if (hasFlow)
            {
                onComplete?.Invoke();
            }
        }
    }

    public void FlowUpdate()
    {
        //The flow will stop when,
        // a) this pipe is not connect to the last pipe
        // b) the last pipe doesn't have flow
        //These condition do not apply to "Start" Pipe

        if (pipeType != PipeType.Start)
        {
            if (flowSources == null)
            {
                SetFlow(false);
            }
            else
                SetFlow(CheckAnyFlow());

            foreach (var pipe in flowSources)
            {
                if (!pipe.hasFlow)
                    RemoveFlowSource(pipe);
            }
        }

    }

    public void SetFlow(bool state)
    {
        hasFlow = state;
        pipeSprite.color = hasFlow ? flowColor : normalColor;
    }


    public void SetLiftState(bool state)
    {
        isBeingLift = state;
    }

    public void SetCollider(bool state)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = state;
        }
    }


    void AddConnectedPipe(Pipe pipe)
    {
        connectedPipe.Add(pipe);
    }

    void RemoveConnectedPipe(Pipe pipe)
    {
        connectedPipe.Remove(pipe);
        RemoveFlowSource(pipe);
    }

    //can Rotate
    public void Rotate()
    {
        if (!LeanTween.isTweening(this.gameObject))
        {
            angle += 90;

            if (angle >= 360)
                angle = 0;

            LeanTweenExt.LeanRotateZ(this.gameObject, angle, .2f);
        }
    }
}
