using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PipeType
{
    Straight, // 直管
    LShape,   // L型管
    TShape,   // T型管
    Cross,     // 十字管
    Start,    // 起点 (新加)
    End       // 终点 (新加)
}

public class Pipe : MonoBehaviour
{
    public PipeType pipeType;
    public int gridX, gridY;

    // 数组索引代表方向：0=上, 1=右, 2=下, 3=左
    public bool[] connections = new bool[4];

    public void Initialize(PipeType type, int x, int y)
    {
        pipeType = type;
        gridX = x;
        gridY = y;

        // 根据类型初始化开口方向 (默认状态)
        switch (type)
        {
            case PipeType.Straight:
                connections = new bool[] { true, false, true, false }; // 上下通
                break;
            case PipeType.LShape:
                connections = new bool[] { true, true, false, false }; // 上右通
                break;
            case PipeType.TShape:
                connections = new bool[] { true, true, true, false };  // 上右下通
                break;
            case PipeType.Cross:
            case PipeType.Start: // 起点和终点在逻辑上设为全通，方便对接任何方向的管道
            case PipeType.End:
                connections = new bool[] { true, true, true, true };   // 全通
                break;
        }

        // 随机旋转几次打乱初始状态
        if (type != PipeType.Start && type != PipeType.End) {
            int randomRotations = Random.Range(0, 4);
            if (GameManager.Instance.isStart)
            {
                for (int i = 0; i < randomRotations; i++)
                {
                    RotateLogic();
                    transform.Rotate(0, 0, -90f); // 顺时针旋转90度
                }
            }
        }
        
    }

    // 鼠标点击事件（需要物体上有Collider2D）
    private void OnMouseDown()
    {
        if (GameManager.Instance.isSolved || pipeType == PipeType.Start || pipeType == PipeType.End)
            return;

        // 视觉旋转 (顺时针90度)
        transform.Rotate(0, 0, -90f);
        // 逻辑旋转
        RotateLogic();
    }

    // 旋转逻辑：顺时针旋转时，开口方向数组向右移位
    private void RotateLogic()
    {
        bool[] newConnections = new bool[4];
        // 旧的左(3)变成新的上(0)，旧的上(0)变成新的右(1)...
        for (int i = 0; i < 4; i++)
        {
            newConnections[i] = connections[(i + 3) % 4];
        }
        connections = newConnections;
    }
}
