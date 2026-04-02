using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DioManager : MonoBehaviour
{
    [SerializeField]
    GameObject trigger1, trigger2, trigger3, trigger4, trigger5;
    [SerializeField]
    GameObject DialogueManager1, DialogueManager2, DialogueManager3, DialogueManager4, DialogueManager5;
    static public bool OnDio=false;
    static int number;
    int collectNumber=0;
    // Start is called before the first frame update
    void Start()
    {
        trigger1.SetActive(false);
        trigger2.SetActive(false);
        trigger3.SetActive(false);
        trigger4.SetActive(false);
        trigger5.SetActive(false);
    }
    static public void CheckWhichCollect(string n)
    {
        if (n == "Gong")
            number = 1;
        else if (n == "Ang")
            number = 3;
        else if (n == "Qiao")
            number = 2;
        else if (n == "ShuaTou")
            number = 4;

    }
    // Update is called once per frame
    void Update()
    {
        if (collectNumber >= 4&& !OnDio)
        {
            trigger5.SetActive(true);
            trigger5.GetComponent<DialogueTrigger>().TriggerDialogue();
            collectNumber = 0;
            number = 5;
        }
        if(number==5&& !OnDio)
        {
            MakeBiger.start = true;
        }
        if (!OnDio&&number!=0) 
        {
            collectNumber++;
        switch (number)
        {
            case 1:
                    trigger1.SetActive(true);
                    trigger2.SetActive(false);
                    trigger3.SetActive(false);
                    trigger4.SetActive(false);

                    trigger1.GetComponent<DialogueTrigger>().TriggerDialogue();
                number = 0;
                break;
            case 2:
                    trigger2.SetActive(true);
                    trigger3.SetActive(false);
                    trigger4.SetActive(false);

                    trigger2.GetComponent<DialogueTrigger>().TriggerDialogue();
                    number = 0;
                    break;
            case 3:
                    trigger3.SetActive(true);
                    trigger4.SetActive(false);

                    trigger3.GetComponent<DialogueTrigger>().TriggerDialogue();
                    number = 0;
                    break;
            case 4:
                    trigger4.SetActive(true);
                    
                    trigger4.GetComponent<DialogueTrigger>().TriggerDialogue();
                    number = 0;
                    break;
            default:
                number = 0;
                break;
        }
        }
    }
}
