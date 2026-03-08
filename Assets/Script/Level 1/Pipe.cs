using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PipeType
{
    Straight, 
    LShape,   
    TShape,   
    Cross,     
    Start,    
    End       
}

public class Pipe : MonoBehaviour
{
    public PipeType pipeType;
    public int gridX, gridY;
    public bool[] connections = new bool[4];

    public void Initialize(PipeType type, int x, int y)
    {
        pipeType = type;
        gridX = x;
        gridY = y;

        switch (type)
        {
            case PipeType.Straight:
                connections = new bool[] { true, false, true, false };
                break;
            case PipeType.LShape:
                connections = new bool[] { true, true, false, false };
                break;
            case PipeType.TShape:
                connections = new bool[] { true, true, true, false };
                break;
            case PipeType.Cross:
            case PipeType.Start: 
            case PipeType.End:
                connections = new bool[] { true, true, true, true };
                break;
        }


        if (type != PipeType.Start && type != PipeType.End) {
            int randomRotations = Random.Range(0, 4);
            if (GameManager.Instance.isStart)
            {
                for (int i = 0; i < randomRotations; i++)
                {
                    RotateLogic();
                    transform.Rotate(0, 0, -90f); 
                }
            }
        }
        
    }


    private void OnMouseDown()
    {
        if (GameManager.Instance.isSolved || pipeType == PipeType.Start || pipeType == PipeType.End)
            return;

        transform.Rotate(0, 0, -90f);

        RotateLogic();
    }

    private void RotateLogic()
    {
        bool[] newConnections = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            newConnections[i] = connections[(i + 3) % 4];
        }
        connections = newConnections;
    }
}
