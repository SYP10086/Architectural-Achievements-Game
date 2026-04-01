using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public void TriggerDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.OnButtonClick();
        }
    }
}