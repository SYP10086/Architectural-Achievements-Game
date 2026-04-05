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
    private void Awake()
    {
        DialogueManager.hasShown1 = false;
        DialogueManager.hasShown2 = false;
        DialogueManager.hasShown3 = false;
    }
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
            TriggerDialogue();
        }
    }
}
