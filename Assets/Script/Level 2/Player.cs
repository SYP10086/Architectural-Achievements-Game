using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] Transform groundDetector;
    [SerializeField] Vector2 groundDetectSize;
    [SerializeField] Vector2 LoadLocation;

    Rigidbody2D rb;

    public string nextSceneName = "Level 2 After";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        Collider2D[] cols = Physics2D.OverlapBoxAll(groundDetector.position, groundDetectSize, 0, LayerMask.GetMask("Ground"));
        if (Input.GetKeyDown(KeyCode.Space) && cols.Length > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        if(transform.position.x - LoadLocation.x > 0.5f)
        {
            LoadNextScene();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(groundDetector.position + new Vector3(groundDetectSize.x / 2, groundDetectSize.y / 2), groundDetector.position + new Vector3(-groundDetectSize.x / 2, groundDetectSize.y / 2));
        Gizmos.DrawLine(groundDetector.position + new Vector3(groundDetectSize.x / 2, -groundDetectSize.y / 2), groundDetector.position + new Vector3(-groundDetectSize.x / 2, -groundDetectSize.y / 2));
        Gizmos.DrawLine(groundDetector.position + new Vector3(groundDetectSize.x / 2, groundDetectSize.y / 2), groundDetector.position + new Vector3(groundDetectSize.x / 2, -groundDetectSize.y / 2));
        Gizmos.DrawLine(groundDetector.position + new Vector3(-groundDetectSize.x / 2, groundDetectSize.y / 2), groundDetector.position + new Vector3(-groundDetectSize.x / 2,-groundDetectSize.y / 2));

    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }


}

