using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("管道预制体")]
    public GameObject straightPrefab;
    public GameObject lShapePrefab;
    public GameObject tShapePrefab;
    public GameObject crossPrefab;

    [Header("起点与终点预制体")]
    public GameObject startPrefab; // 新加：起点预制体
    public GameObject endPrefab;   // 新加：终点预制体

    [Header("关卡设置")]
    public int width = 4;
    public int height = 4;
    public Vector2 startPos = new Vector2(0, 0); // 起点坐标
    public Vector2 endPos = new Vector2(3, 3);   // 终点坐标

    private Pipe[,] grid;
    public bool isSolved = false;
    public bool isStart = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isStart = true;
        GenerateFixedLevel();
    }

    private void Update()
    {
        // 按照需求，每帧检测是否连通
        if (!isSolved)
        {
            if (CheckConnection())
            {
                isSolved = true;
                Debug.Log("管道已连通");
            }
        }
    }

    // 生成一个固定且有解的4x4关卡
    private void GenerateFixedLevel()
    {
        grid = new Pipe[width, height];

        // 定义一个固定的布局 (这里设计了一条从 0,0 到 3,3 的通路)
        PipeType[,] levelLayout = new PipeType[4, 4]
        {
            // Y=0 (底部)
            { PipeType.Start, PipeType.Straight, PipeType.TShape, PipeType.LShape },
            // Y=1
            { PipeType.Straight, PipeType.Cross, PipeType.LShape, PipeType.Straight },
            // Y=2
            { PipeType.LShape, PipeType.TShape, PipeType.Straight, PipeType.Cross },
            // Y=3 (顶部)
            { PipeType.Cross, PipeType.LShape, PipeType.Straight, PipeType.End }
        };

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                PipeType type = levelLayout[y, x];
                GameObject prefabToSpawn = GetPrefabByType(type);

                // 实例化并排列在屏幕上
                GameObject go = Instantiate(prefabToSpawn, new Vector3(x, y, 0), Quaternion.identity);

                // 【修复点】：尝试获取预制体上自带的 Pipe 组件，如果没有才 AddComponent
                // 这样可以完美避免挂载两个脚本导致旋转 180° 的 Bug
                Pipe pipe = go.GetComponent<Pipe>();
                if (pipe == null)
                {
                    pipe = go.AddComponent<Pipe>();
                }

                pipe.Initialize(type, x, y);
                grid[x, y] = pipe;
            }
        }
        isStart = false;
    }

    private GameObject GetPrefabByType(PipeType type)
    {
        switch (type)
        {
            case PipeType.Straight: return straightPrefab;
            case PipeType.LShape: return lShapePrefab;
            case PipeType.TShape: return tShapePrefab;
            case PipeType.Cross: return crossPrefab;
            case PipeType.Start: return startPrefab; // 新加
            case PipeType.End: return endPrefab;     // 新加
            default: return straightPrefab;
        }
    }

    // 使用DFS（深度优先搜索）检测连通性
    private bool CheckConnection()
    {
        bool[,] visited = new bool[width, height];
        return DFS((int)startPos.x, (int)startPos.y, visited);
    }

    private bool DFS(int x, int y, bool[,] visited)
    {
        // 如果到达终点，说明连通
        if (x == (int)endPos.x && y == (int)endPos.y) return true;

        visited[x, y] = true;
        Pipe currentPipe = grid[x, y];

        // 检查四个方向：0=上, 1=右, 2=下, 3=左
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };
        // 对应的邻居需要的反向接口：上对下(2)，右对左(3)，下对上(0)，左对右(1)
        int[] oppositeDir = { 2, 3, 0, 1 };

        for (int i = 0; i < 4; i++)
        {
            // 如果当前管道在这个方向有开口
            if (currentPipe.connections[i])
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                // 检查邻居是否在网格内，且未被访问过
                if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visited[nx, ny])
                {
                    Pipe neighborPipe = grid[nx, ny];
                    // 检查邻居管道在对应的反方向是否有开口
                    if (neighborPipe.connections[oppositeDir[i]])
                    {
                        // 递归继续找
                        if (DFS(nx, ny, visited)) return true;
                    }
                }
            }
        }
        return false;
    }
}
