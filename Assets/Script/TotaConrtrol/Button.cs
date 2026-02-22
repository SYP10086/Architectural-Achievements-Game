using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Button : MonoBehaviour
{
   private GameObject positionObject;
    //private GameObject dialoguePanel;
    public string Level;
    Animator animator;

   
    // Start is called before the first frame update
    void Start()
    {
  
      //  dialoguePanel.gameObject.SetActive(false);
       
            positionObject = GameObject.Find(Level);
        if (positionObject != null )
        this.transform.position = positionObject.transform.position;
        animator = GetComponent<Animator>();
        animator.SetBool("FadeOut", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(Level);
        animator.SetBool("FadeOut", true);
    }
    public void Quit()
    {
        Application.Quit();
    }


    

 

}