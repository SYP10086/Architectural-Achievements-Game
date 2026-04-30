using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuildings : MonoBehaviour
{
    [Header("∆¡ƒªπÃ∂®…Ë÷√")]
    [SerializeField] private bool useCustomScreenPosition = false;
    [SerializeField] private Vector3 customScreenPosition;

    [Header("Õœ◊ß”Î∑≈÷√")]
    [SerializeField] public float boxX, boxY;
    [SerializeField] private Vector3 destination;
    [SerializeField] private int num;
    [SerializeField] private float r = 4f;
    [SerializeField] private float smoothForwardTime = 0.3f;
    [SerializeField] private float smoothBackTime = 0.5f;
    [SerializeField] private float maxSpeed = Mathf.Infinity;

    [Header("…‰œﬂºÏ≤‚")]
    public LayerMask GroundLayer;
    public float heightOffset = 0f;

    private bool isDragging = false;
    private bool hasExecuted = false;
    private bool hasBeenDragged = false;
    private Rigidbody rb;
    private Vector3 currentVelocity;
    private float distance;

    private Vector3 targetScreenPosition;
    private float initialDepth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        if (useCustomScreenPosition)
        {
            targetScreenPosition = customScreenPosition;
            initialDepth = customScreenPosition.z;
            transform.position = Camera.main.ScreenToWorldPoint(customScreenPosition);
        }
        else
        {
            Vector3 initScreen = Camera.main.WorldToScreenPoint(transform.position);
            targetScreenPosition = initScreen;
            initialDepth = initScreen.z;
        }

        isDragging = false;
        hasExecuted = false;
        hasBeenDragged = false;
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, destination);

        if (!isDragging && num != DestinationMovement.DestinationCount)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(targetScreenPosition.x, targetScreenPosition.y, initialDepth)
            );
            transform.position = worldPos;
            return;
        }

        if (isDragging && num == DestinationMovement.DestinationCount)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundLayer))
            {
                Vector3 targetPos = hit.point;
                targetPos.y += heightOffset;
                transform.position = targetPos;
            }
        }

        if (!isDragging && num == DestinationMovement.DestinationCount)
        {
            if (!hasBeenDragged)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(
                    new Vector3(targetScreenPosition.x, targetScreenPosition.y, initialDepth)
                );
                transform.position = worldPos;
            }
            else
            {
                if (distance <= r)
                {
                    transform.position = Vector3.SmoothDamp(
                        transform.position,
                        destination,
                        ref currentVelocity,
                        smoothForwardTime,
                        maxSpeed
                    );
                }
                else
                {
                    Vector3 originalPos = Camera.main.ScreenToWorldPoint(
                        new Vector3(targetScreenPosition.x, targetScreenPosition.y, initialDepth)
                    );
                    transform.position = Vector3.SmoothDamp(
                        transform.position,
                        originalPos,
                        ref currentVelocity,
                        smoothBackTime,
                        maxSpeed
                    );
                }
            }
        }

        if (Vector3.Distance(transform.position, destination) <= 0.1f && !hasExecuted &&
            num == DestinationMovement.DestinationCount && DestinationMovement.DestinationCount >= 0)
        {
            hasExecuted = true;
            DestinationMovement.DestinationCount++;
            this.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (DestinationMovement.DestinationCount == -1)
        {
            DestinationMovement.DestinationCount = 0;

            if (num == 0)
            {
                isDragging = true;
                hasBeenDragged = true;
            }
            return;
        }

        if (num == DestinationMovement.DestinationCount)
        {
            isDragging = true;
            hasBeenDragged = true;
        }
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        currentVelocity = rb.velocity;
        distance = Vector3.Distance(transform.position, destination);
        isDragging = false;
    }
}