using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTest : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] Transform[] ropeTransforms;
    [SerializeField] int ropResolution;
    [SerializeField] float maxLenght;
    [SerializeField] float stretchiness;

    private void OnEnable()
    {
        TryGetComponent<LineRenderer>(out line);

        if (line == null)
        {
            line = new LineRenderer();
        }
    }

    private void Update()
    {
        RopeBehaviour();

        RopeVisual();
    }

    void RopeBehaviour()
    {

    }

    void RopeVisual()
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            var interpolate = ((float)(i) / (float)(line.positionCount - 1));
            print(interpolate);

            var abLine = Vector3.Lerp(ropeTransforms[0].position, ropeTransforms[1].position, interpolate);
            var bcLine = Vector3.Lerp(ropeTransforms[1].position, ropeTransforms[2].position, interpolate);
            line.SetPosition(i, Vector3.Lerp(abLine, bcLine, interpolate));
        }
    }
}
