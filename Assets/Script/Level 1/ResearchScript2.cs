using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//两个目标点
public enum SquareTarget
{
    TargetPoint1,
    TargetPoint2
}
public class SquareManager : MonoBehaviour
{
    public static SquareManager manager;//单例

    //储存所有方块
    public Square[] squares1;
    public Square[] squares2;
    public Square[] squares3;
    public Square[] squares4;
    public Square[,] squares = new Square[4, 4];

    Dictionary<Vector2, List<int>> road = new Dictionary<Vector2, List<int>>();//路径

    //数组最大维度
    private const int MAX = 3;
    private const int MIN = 0;

    private SquareTarget target;

    //判断是否完成
    bool isChecked1, isChecked2;

    //记录Connect方法执行第一次，第一次执行不存储上一个点
    bool isFirstConnect = true;

    private void Awake()
    {
        if (manager == null)
            manager = this;
    }

    //初始化数组
    void Start()
    {
        for (int i = MIN; i <= MAX; i++)
        {
            for (int j = MIN; j <= MAX; j++)
            {
                if (i == 0)
                {
                    squares[j, i] = squares1[j];
                }
                if (i == 1)
                {
                    squares[j, i] = squares2[j];
                }
                if (i == 2)
                {
                    squares[j, i] = squares3[j];
                }
                if (i == 3)
                {
                    squares[j, i] = squares4[j];
                }
            }
        }

        StartConnect();
    }

    //核心方法，根据来的流向和方块类型还有方块状态判断向下递归
    public bool CheckConnect(int x, int y, int comeDir, int lastX, int lastY, int lastDir)
    {
        //保存上一步方块，保存当前方块的话会导致一直高亮
        if (!isFirstConnect)
        {
            if (road.ContainsKey(new Vector2(x, y)) && road[new Vector2(x, y)].Contains(comeDir))
                return false;
            if (!road.ContainsKey(new Vector2(lastX, lastY)))
            {
                road.Add(new Vector2(lastX, lastY), new List<int>() { lastDir });
            }
            else
            {
                road[new Vector2(lastX, lastY)].Add(lastDir);
            }
        }
        else
        {
            isFirstConnect = false;
        }
        //越界返回
        if (x > 3 || x < 0 || y > 3 || y < 0)
            return false;
        //结束条件
        switch (target)
        {
            case SquareTarget.TargetPoint1:
                if (x == 3 && y == 0)
                {
                    if (squares[x, y].up == 2 && comeDir == 0)
                    {
                        if (!road.ContainsKey(new Vector2(x, y)))
                        {
                            road.Add(new Vector2(x, y), new List<int>() { comeDir });
                        }
                        else if (!road[new Vector2(x, y)].Contains(comeDir))
                        {
                            road[new Vector2(x, y)].Add(comeDir);
                        }
                        isChecked1 = true;
                        Debug.Log("-----------------------------Success1-------------------------------");
                        return true;
                    }
                }
                break;
            case SquareTarget.TargetPoint2:
                if (x == 0 && y == 0)
                {
                    if ((squares[x, y].up == 2 || squares[x, y].up == 0) && comeDir == 1)
                    {
                        if (!road.ContainsKey(new Vector2(x, y)))
                        {
                            road.Add(new Vector2(x, y), new List<int>() { comeDir });
                        }
                        else if (!road[new Vector2(x, y)].Contains(comeDir))
                        {
                            road[new Vector2(x, y)].Add(comeDir);
                        }

                        isChecked2 = true;
                        Debug.Log("-----------------------------Success2-------------------------------");
                        return true;
                    }
                }
                break;
        }

        //上0 右1 下2 左3
        //根据条件递归
        switch (comeDir)
        {
            case 0:
                switch (squares[x, y].type)
                {
                    case SquareType.Yi:
                        if (squares[x, y].up == 0 || squares[x, y].up == 2)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Er:
                        if (squares[x, y].up == 1)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 2)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.San:
                        if (squares[x, y].up == 0)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 2)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Si:
                        if (squares[x, y].up == 0 || squares[x, y].up == 2)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1 || squares[x, y].up == 3)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Shi:
                        CheckConnect(x, y - 1, 0, x, y, comeDir);
                        break;
                }
                break;
            case 1:
                switch (squares[x, y].type)
                {
                    case SquareType.Yi:
                        if (squares[x, y].up == 1 || squares[x, y].up == 3)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Er:
                        if (squares[x, y].up == 2)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 3)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.San:
                        if (squares[x, y].up == 1)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 2)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 3)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Si:
                        if (squares[x, y].up == 0 || squares[x, y].up == 2)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1 || squares[x, y].up == 3)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Shi:
                        CheckConnect(x - 1, y, 1, x, y, comeDir);
                        break;
                }
                break;
            case 2:
                switch (squares[x, y].type)
                {
                    case SquareType.Yi:
                        if (squares[x, y].up == 0 || squares[x, y].up == 2)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Er:
                        if (squares[x, y].up == 0)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 3)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.San:
                        if (squares[x, y].up == 0)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 2)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 3)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Si:
                        if (squares[x, y].up == 0 || squares[x, y].up == 2)
                        {
                            CheckConnect(x - 1, y, 1, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1 || squares[x, y].up == 3)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Shi:
                        CheckConnect(x, y + 1, 2, x, y, comeDir);
                        break;
                }
                break;
            case 3:
                switch (squares[x, y].type)
                {
                    case SquareType.Yi:
                        if (squares[x, y].up == 1 || squares[x, y].up == 3)
                        {
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Er:
                        if (squares[x, y].up == 0)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.San:
                        if (squares[x, y].up == 0)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 3)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                            CheckConnect(x + 1, y, 3, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Si:
                        if (squares[x, y].up == 0 || squares[x, y].up == 2)
                        {
                            CheckConnect(x, y - 1, 0, x, y, comeDir);
                        }
                        else if (squares[x, y].up == 1 || squares[x, y].up == 3)
                        {
                            CheckConnect(x, y + 1, 2, x, y, comeDir);
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case SquareType.Shi:
                        CheckConnect(x + 1, y, 3, x, y, comeDir);
                        break;
                }
                break;
            default:
                return false;
        }
        return false;
    }

    //路径高亮
    void OnOffLight()
    {

        foreach (var item in squares)
        {
            if (item.type == SquareType.Si)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (item.type == SquareType.Shi)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.transform.GetChild(1).gameObject.SetActive(false);
                item.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        foreach (var item in road.Keys)
        {
            if (squares[(int)item.x, (int)item.y].type == SquareType.Si)
            {
                if (squares[(int)item.x, (int)item.y].up == 0)
                {
                    foreach (var dirs in road[item])
                    {
                        if (dirs == 2 || dirs == 3)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.SetActive(true);
                        }
                        if (dirs == 0 || dirs == 1)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
                if (squares[(int)item.x, (int)item.y].up == 1)
                {
                    foreach (var dirs in road[item])
                    {
                        if (dirs == 3 || dirs == 0)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.SetActive(true);
                        }
                        if (dirs == 2 || dirs == 1)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
                if (squares[(int)item.x, (int)item.y].up == 2)
                {
                    foreach (var dirs in road[item])
                    {
                        if (dirs == 2 || dirs == 3)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);
                        }
                        if (dirs == 0 || dirs == 1)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
                if (squares[(int)item.x, (int)item.y].up == 3)
                {
                    foreach (var dirs in road[item])
                    {
                        if (dirs == 3 || dirs == 0)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);
                        }
                        if (dirs == 2 || dirs == 1)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (squares[(int)item.x, (int)item.y].type == SquareType.Shi)
            {
                if (squares[(int)item.x, (int)item.y].up == 0 || squares[(int)item.x, (int)item.y].up == 2)
                {
                    foreach (var dirs in road[item])
                    {
                        if (dirs == 0 || dirs == 2)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);
                        }
                        if (dirs == 3 || dirs == 1)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
                if (squares[(int)item.x, (int)item.y].up == 1 || squares[(int)item.x, (int)item.y].up == 3)
                {
                    foreach (var dirs in road[item])
                    {
                        if (dirs == 0 || dirs == 2)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.SetActive(true);
                        }
                        if (dirs == 3 || dirs == 1)
                        {
                            squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
                if (squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.activeSelf && squares[(int)item.x, (int)item.y].transform.GetChild(1).gameObject.activeSelf)
                {
                    squares[(int)item.x, (int)item.y].transform.GetChild(2).gameObject.SetActive(true);
                }
            }
            else
            {
                squares[(int)item.x, (int)item.y].transform.GetChild(0).gameObject.SetActive(true);

            }
        }
    }

    //统一调用
    public void StartConnect()
    {
        target = SquareTarget.TargetPoint2;
        isFirstConnect = true;
        CheckConnect(3, 3, 1, 3, 3, 1);

        //这一步是为了实现从第二个点向外路径高亮
        target = SquareTarget.TargetPoint2;
        isFirstConnect = true;
        CheckConnect(3, 0, 1, 3, 0, 1);
        OnOffLight();
        road.Clear();

        target = SquareTarget.TargetPoint1;
        isFirstConnect = true;
        CheckConnect(3, 3, 1, 3, 3, 1);
        road.Clear();

        if (isChecked1 && isChecked2)
        {
            Debug.Log("-----------------------------Success-------------------------------");
        }

        isChecked1 = false;
        isChecked2 = false;
    }
}

