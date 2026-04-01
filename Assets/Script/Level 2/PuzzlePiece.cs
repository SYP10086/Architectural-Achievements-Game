using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2 correctPosition;
    public float snapDistance = 0.5f;

    private Vector3 screenPoint;      // 鼠标点击时物体在屏幕中的位置
    private bool isDragging = false;
    private SpriteRenderer spriteRenderer;
    private PuzzleManager puzzleManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    void OnMouseDown()
    {
        if (puzzleManager != null && puzzleManager.isCompleted) return;

        isDragging = true;
        spriteRenderer.sortingOrder = 10;

        // 关键：记录物体当前的世界位置与鼠标屏幕位置的关系
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    }

    void OnMouseDrag()
    {
        if (!isDragging || (puzzleManager != null && puzzleManager.isCompleted)) return;

        // 获取当前鼠标在屏幕上的位置
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        // 将屏幕坐标转回世界坐标
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);

        // 直接设置物体位置（无额外偏移，因为已通过 screenPoint.z 保持深度一致）
        transform.position = new Vector2(currentPosition.x, currentPosition.y);
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;
        spriteRenderer.sortingOrder = 0;

        if (Vector2.Distance(transform.position, correctPosition) <= snapDistance)
        {
            transform.position = correctPosition;
            if (puzzleManager != null)
                puzzleManager.CheckPuzzleComplete();
        }
    }
}