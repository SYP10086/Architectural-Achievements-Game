using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ButtonForPoem : MonoBehaviour
{

   
    public string Level;
    public Animator animator;
    public Material BlurMat;
    public Material DefaultMat;
    public GameObject GameObject;
    public float animationLength = 1f;



    // Start is called before the first frame update
    void Start()
    {

  
        animator = GetComponent<Animator>();
        animator.SetBool(Level, false);
    }

    // Update is called once per frame
    void Update()
    {

    }
   

    public void OnButtonClick()
    {
        animator.SetBool(Level, true);
       // Blur.BlurGameObject(GameObject,BlurMat);
       Camera cam = Camera.main;
        
        StartCoroutine(Play());
    
    }

    IEnumerator Play()
    {
       /* if (animator != null)
        { 
        animator.SetTrigger(Level);
        
        }*/
        yield return new WaitForSeconds(animationLength);
        if (!string.IsNullOrEmpty(Level))
        {
            SceneManager.LoadScene(Level);
        }
    }

}
