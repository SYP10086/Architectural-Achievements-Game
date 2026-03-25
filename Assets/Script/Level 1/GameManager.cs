using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("管道预制体")]
    public GameObject straightPrefab;
    public GameObject lShapePrefab;
    public GameObject tShapePrefab;
    public GameObject crossPrefab;

    [Header("起点、终点与空预制体")]
    public GameObject startPrefab; 
    public GameObject endPrefab;
    public GameObject empty1Prefab;
    public GameObject empty2Prefab;
    public GameObject empty3Prefab;
    public GameObject empty4Prefab;
    public GameObject empty5Prefab;
    public GameObject empty6Prefab;
    public GameObject empty7Prefab;
    public GameObject empty8Prefab;

    [Header("关卡设置")]
    public int width = 5;
    public int height = 5;
    public Vector2 startPos = new Vector2(1, 0); 
    public Vector2 endPos = new Vector2(4, 4);   

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
        if (!isSolved)
        {
            if (CheckConnection())
            {
                isSolved = true;
                Debug.Log("管道已连通");
                AchieveDetect.achieve[0] = true;
                PlayerPrefs.SetInt("achieve[0]", AchieveDetect.achieve[0] ? 1 : 0);
                SceneManager.LoadScene("Options");
            }
        }
    }

    private void GenerateFixedLevel()
    {
        grid = new Pipe[width, height];


        PipeType[,] levelLayout = new PipeType[5, 5]
        {

            { PipeType.Empty1, PipeType.Start, PipeType.Empty2, PipeType.Empty3, PipeType.Empty4 },

            { PipeType.Empty5, PipeType.Straight, PipeType.Cross, PipeType.LShape, PipeType.Straight },

            { PipeType.Empty6,  PipeType.LShape, PipeType.TShape, PipeType.Straight, PipeType.Cross },

            { PipeType.Empty7,  PipeType.Cross, PipeType.LShape, PipeType.Straight, PipeType.LShape},

            { PipeType.Empty8, PipeType.Straight, PipeType.LShape, PipeType.TShape,PipeType.End  }
        };

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                PipeType type = levelLayout[y, x];
                GameObject prefabToSpawn = GetPrefabByType(type);


                GameObject go = Instantiate(prefabToSpawn, new Vector3(x, y, 0), Quaternion.identity);


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
            case PipeType.Start: return startPrefab; 
            case PipeType.End: return endPrefab;  
            case PipeType.Empty1: return empty1Prefab;
            case PipeType.Empty2: return empty2Prefab;
            case PipeType.Empty3: return empty3Prefab;
            case PipeType.Empty4: return empty4Prefab;
            case PipeType.Empty5: return empty5Prefab;
            case PipeType.Empty6: return empty6Prefab;
            case PipeType.Empty7: return empty7Prefab;
            case PipeType.Empty8: return empty8Prefab;
            default: return straightPrefab;
        }
    }


    private bool CheckConnection()
    {
        bool[,] visited = new bool[width, height];
        return DFS((int)startPos.x, (int)startPos.y, visited);
    }

    private bool DFS(int x, int y, bool[,] visited)
    {

        if (x == (int)endPos.x && y == (int)endPos.y) return true;

        visited[x, y] = true;
        Pipe currentPipe = grid[x, y];


        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };
        int[] oppositeDir = { 2, 3, 0, 1 };

        for (int i = 0; i < 4; i++)
        {
            if (currentPipe.connections[i])
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visited[nx, ny])
                {
                    Pipe neighborPipe = grid[nx, ny];
                    if (neighborPipe.connections[oppositeDir[i]])
                    {
                        if (DFS(nx, ny, visited)) return true;
                    }
                }
            }
        }
        return false;
    }
}
