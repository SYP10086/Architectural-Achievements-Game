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
    End,
    Empty1,
    Empty2,
    Empty3,
    Empty4,
    Empty5,
    Empty6,
    Empty7,
    Empty8,
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
            case PipeType.Empty1:
            case PipeType.Empty2:
            case PipeType.Empty3:
            case PipeType.Empty4:
            case PipeType.Empty5:
            case PipeType.Empty6:
            case PipeType.Empty7:
            case PipeType.Empty8:
                connections = new bool[] { false, false, false, false };
                break;
        }


        if (type != PipeType.Start && type != PipeType.End && type != PipeType.Empty1 && type != PipeType.Empty2 && type != PipeType.Empty3 && type != PipeType.Empty4 && type != PipeType.Empty5 && type != PipeType.Empty6 && type != PipeType.Empty7 && type != PipeType.Empty8) {
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
        if (GameManager.Instance.isSolved || pipeType == PipeType.Start || pipeType == PipeType.End || pipeType == PipeType.Empty1 || pipeType == PipeType.Empty2 || pipeType == PipeType.Empty3 || pipeType == PipeType.Empty4 || pipeType == PipeType.Empty5 || pipeType == PipeType.Empty6 || pipeType == PipeType.Empty7 || pipeType == PipeType.Empty8)
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
