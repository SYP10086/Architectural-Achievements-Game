using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;           // UI文本组件
    public GameObject dialoguePanel;    // 对话面板
    public float typingSpeed = 0.05f;   // 打字速度

    private Dialogue currentDialogue;
    private int sentenceIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        sentenceIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentenceIndex < currentDialogue.sentences.Length)
        {
            string sentence = currentDialogue.sentences[sentenceIndex];
            sentenceIndex++;

            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = sentence;
                isTyping = false;
            }
            else
            {
                typingCoroutine = StartCoroutine(TypeSentence(sentence));
            }
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}