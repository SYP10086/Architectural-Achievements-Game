using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public float typingSpeed = 0.05f;   // 打字速度
    public Dialogue dialogue;
    public Dialogue dialogue2;
    public Dialogue dialogue3;

    private Dialogue currentDialogue;
    private int sentenceIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool isActive = false;
    private Text currentDialogueText;  // 当前对话的Text组件

    void Start()
    {
        // 确保所有对话面板初始是关闭的
        if (dialogue != null && dialogue.dialoguePanel != null)
            dialogue.dialoguePanel.SetActive(false);
        if (dialogue2 != null && dialogue2.dialoguePanel != null)
            dialogue2.dialoguePanel.SetActive(false);
        if (dialogue3 != null && dialogue3.dialoguePanel != null)
            dialogue3.dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // 处理开始新对话的点击
        if (Input.GetMouseButtonDown(0) && !isActive)
        {
            // 检查第一个对话
            if (dialogue != null && !dialogue.hasShown)
            {
                StartDialogue(dialogue);
                return;
            }
            // 检查第二个对话（第一个已显示）
            else if (dialogue2 != null && dialogue.hasShown && !dialogue2.hasShown)
            {
                StartDialogue(dialogue2);
                return;
            }
            // 检查第三个对话（第一、二个已显示）
            else if (dialogue3 != null && dialogue.hasShown && dialogue2.hasShown && !dialogue3.hasShown)
            {
                StartDialogue(dialogue3);
                return;
            }
        }

        // 处理对话进行中的点击（显示下一句）
        if (isActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogue.hasShown = true;
        isActive = true;
        currentDialogue = dialogue;
        sentenceIndex = 0;

        // 获取当前对话的Text组件
        if (dialogue.dialogueText != null)
        {
            currentDialogueText = dialogue.dialogueText;
        }

        // 关闭其他对话的面板
        if (dialogue != this.dialogue && this.dialogue != null && this.dialogue.dialoguePanel != null)
            this.dialogue.dialoguePanel.SetActive(false);
        if (dialogue != dialogue2 && dialogue2 != null && dialogue2.dialoguePanel != null)
            dialogue2.dialoguePanel.SetActive(false);
        if (dialogue != dialogue3 && dialogue3 != null && dialogue3.dialoguePanel != null)
            dialogue3.dialoguePanel.SetActive(false);

        // 打开当前对话的面板
        if (dialogue.dialoguePanel != null)
        {
            dialogue.dialoguePanel.SetActive(true);
        }

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
                if (currentDialogueText != null)
                    currentDialogueText.text = sentence;
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
        if (currentDialogueText != null)
            currentDialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (currentDialogueText != null)
                currentDialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        if (currentDialogue != null && currentDialogue.dialoguePanel != null)
        {
            currentDialogue.dialoguePanel.SetActive(false);
        }
        isActive = false;
        currentDialogueText = null;
    }
}