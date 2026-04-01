using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Object = UnityEngine.Object;

public class DoInStart : MonoBehaviour
{
    bool i = false;
    public DialogueManager dialogueManager;
    public void TriggerDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.OnButtonClick();
        }
    }
    private void LateUpdate()
    {
        if(!i)
        {
            i = true;
            //Time.timeScale = 0;
            TriggerDialogue();
        }
    }
}
