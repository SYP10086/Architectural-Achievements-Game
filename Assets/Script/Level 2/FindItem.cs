using UnityEngine;
using System.Collections;

public class FindItemNonUI_Coroutine : MonoBehaviour
{
    [Header("动画配置")]
    [Tooltip("拽出后完全显示的位置（世界坐标）")]
    public Vector2 pullOutTargetPos;
    [Tooltip("最终移动到的屏幕下方位置（世界坐标）")]
    public Vector2 finalTargetPos;
    [Tooltip("拽出动画速度（值越小越慢，建议1-5）")]
    public float pullOutSpeed = 2f;
    [Tooltip("移动到目标位置的速度（建议2-8）")]
    public float moveToTargetSpeed = 4f;
    [Tooltip("拽出后停留时间（秒），默认0.5秒")]
    public float waitTimeAfterPullOut = 0.5f;

    // 标记是否正在执行动画（防止重复点击）
    private bool isAnimating = false;
    // 主相机引用
    private Camera mainCamera;

    void Start()
    {
        // 获取主相机
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("场景中没有主相机！请确保有相机标记为MainCamera");
            return;
        }

        // 初始化碰撞体
        InitCollider();

        // 初始化物品图层
        InitItemLayer();
    }

    void Update()
    {
        // 检测鼠标左键点击（仅在非动画状态下响应）
        if (!isAnimating && Input.GetMouseButtonDown(0))
        {
            CheckMouseClick();
        }
    }

    #region 初始化方法
    private void InitCollider()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        collider.isTrigger = true;
    }

    private void InitItemLayer()
    {
        int clickableLayer = LayerMask.NameToLayer("ClickableItems");
        if (clickableLayer == -1)
        {
            Debug.LogWarning("未找到ClickableItems图层，已将物品放入Default图层");
            clickableLayer = LayerMask.NameToLayer("Default");
        }
        gameObject.layer = clickableLayer;
    }
    #endregion

    #region 点击检测
    private void CheckMouseClick()
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        int layerMask = 1 << LayerMask.NameToLayer("ClickableItems");
        if (LayerMask.NameToLayer("ClickableItems") == -1)
        {
            layerMask = Physics2D.DefaultRaycastLayers;
        }

        RaycastHit2D hit = Physics2D.Raycast(
            mouseWorldPos,
            Vector2.zero,
            0f,
            layerMask
        );

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            Debug.Log($"点击到物品：{gameObject.name}，开始动画流程");
            // 启动协程执行完整动画
            StartCoroutine(ItemAnimationCoroutine());
        }
    }
    #endregion

    #region 核心协程逻辑（重点）
    // 物品完整动画流程协程
    private IEnumerator ItemAnimationCoroutine()
    {
        // 标记为动画中，防止重复点击
        isAnimating = true;

        // 第一步：执行拽出动画
        yield return StartCoroutine(PullOutAnimationCoroutine());

        // 第二步：等待指定时长（0.5秒）
        Debug.Log($"物品{gameObject.name}拽出完成，等待{waitTimeAfterPullOut}秒");
        yield return new WaitForSeconds(waitTimeAfterPullOut);

        // 第三步：执行移动到目标位置动画
        yield return StartCoroutine(MoveToTargetAnimationCoroutine());

        // 动画全部完成，重置标记
        isAnimating = false;
        Debug.Log($"物品{gameObject.name}动画流程全部完成");
    }

    // 拽出动画协程
    private IEnumerator PullOutAnimationCoroutine()
    {
        // 循环移动，直到到达拽出目标位置
        while (Vector2.Distance(transform.position, pullOutTargetPos) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                pullOutTargetPos,
                pullOutSpeed * Time.deltaTime
            );
            // 等待下一帧（协程核心，让动画平滑执行）
            yield return null;
        }
    }

    // 移动到目标位置动画协程
    private IEnumerator MoveToTargetAnimationCoroutine()
    {
        Debug.Log($"物品{gameObject.name}开始移动到最终位置");
        // 循环移动，直到到达最终目标位置
        while (Vector2.Distance(transform.position, finalTargetPos) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                finalTargetPos,
                moveToTargetSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
    #endregion

    // 重置物品状态
    public void ResetItem()
    {
        // 停止所有协程（防止动画残留）
        StopAllCoroutines();
        // 重置位置和动画标记
        transform.position = transform.position; // 这里可改为初始隐藏位置，建议提前记录
        isAnimating = false;
        Debug.Log($"物品{gameObject.name}已重置");
    }
}