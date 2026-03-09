using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private SpriteRenderer SpriteRenderer;
    Animator animator;
 
    // Start is called before the first frame update
    void Start()
    {
    SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxis("Horizontal");
        if (dir < 0)
        {
            SpriteRenderer.flipX = false;
            animator.SetBool("IsRunning", true);

        }
        else if (dir > 0)
        { 
            SpriteRenderer.flipX = true;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false); 
        }
            Vector3 movement = new Vector3(speed * dir * Time.deltaTime, 0, 0);
        transform.Translate(movement);
    }
}
