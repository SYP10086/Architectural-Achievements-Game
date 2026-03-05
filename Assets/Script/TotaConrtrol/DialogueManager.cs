using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public float typingSpeed = 0.05f;

    [Header("三段对话配置")]
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public GameObject Background;
    public Material Material;
    public Material DefaultMat;

    private Dialogue currentDialogue;
    private int currentSentenceIndex;
    private bool isTyping;
    private Coroutine typingCoroutine;
    

    private enum DialogueState
    {
        NotStarted,     // 未开始，等待显示第一段
        Dialogue1,      // 对话1进行中
        Dialogue2,      // 对话2进行中
        Dialogue3,      // 对话3进行中
        WaitingForNext, // 等待点击开始下一段
        Completed       // 全部完成
    }

    private DialogueState currentState = DialogueState.NotStarted;

    void Start()
    {
     
        ValidateDialogues();
        CloseAllPanels();
    }

    void ValidateDialogues()
    {
        if (dialogue1 != null && dialogue1.dialogueText == null)
            Debug.LogError("dialogue1 的 dialogueText 未赋值！");
        if (dialogue2 != null && dialogue2.dialogueText == null)
            Debug.LogError("dialogue2 的 dialogueText 未赋值！");
        if (dialogue3 != null && dialogue3.dialogueText == null)
            Debug.LogError("dialogue3 的 dialogueText 未赋值！");
    }


  

    // 同一个按钮点击方法
    public void OnButtonClick()
    {
        switch (currentState)
        {
            case DialogueState.NotStarted:
                // 开始第一段对话
                StartDialogue(dialogue1);
                currentState = DialogueState.Dialogue1;
                break;

            case DialogueState.Dialogue1:
            case DialogueState.Dialogue2:
            case DialogueState.Dialogue3:
                // 对话进行中：显示下一句
                DisplayNextSentence();
                break;

            case DialogueState.WaitingForNext:
                // 上一段已结束，开始下一段
                StartNextDialogue();
                break;

            case DialogueState.Completed:
                Debug.Log("所有对话已完成");
                // 可选：禁用按钮或显示提示
                break;
        }
    }

    void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.dialogueText == null)
        {
            //Debug.LogError("无法开始对话：dialogue 或 dialogueText 为 null");
            return;
        }

        // 关闭所有面板
        CloseAllPanels();
        Camera camera = Camera.main;
       camera.GetComponent<CameraBlurDarken>().enabled = true;
        
        // 设置当前对话
        currentDialogue = dialogue;
        currentSentenceIndex = 0;

        // 标记为已显示
        if (dialogue == dialogue1) dialogue1.hasShown = true;
        else if (dialogue == dialogue2) dialogue2.hasShown = true;
        else if (dialogue == dialogue3) dialogue3.hasShown = true;

        // 打开当前对话的面板
        if (dialogue.dialoguePanel != null)
            dialogue.dialoguePanel.SetActive(true);

        // 显示第一句
        DisplayNextSentence();
    }

    void StartNextDialogue()
    {
        // 根据状态决定下一段对话
        if (currentState == DialogueState.WaitingForNext)
        {
            if (dialogue1.hasShown && dialogue2 != null && !dialogue2.hasShown)
            {
                StartDialogue(dialogue2);
                currentState = DialogueState.Dialogue2;
            }
            else if (dialogue1.hasShown && dialogue2.hasShown && dialogue3 != null && !dialogue3.hasShown)
            {
                StartDialogue(dialogue3);
                currentState = DialogueState.Dialogue3;
            }
            else
            {
                currentState = DialogueState.Completed;
            }
        }
    }

    void DisplayNextSentence()
    {
        if (currentDialogue == null)
        {
            Debug.LogError("currentDialogue 为 null");
            return;
        }

        if (currentDialogue.dialogueText == null)
        {
            Debug.LogError("currentDialogue.dialogueText 为 null");
            return;
        }

        // 检查是否还有句子
        if (currentSentenceIndex < currentDialogue.sentences.Length)
        {
            string sentence = currentDialogue.sentences[currentSentenceIndex];
            currentSentenceIndex++;

            // 如果正在打字，直接显示完整句子
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                currentDialogue.dialogueText.text = sentence;
                isTyping = false;
            }
            else
            {
                // 开始打字效果
                typingCoroutine = StartCoroutine(TypeSentence(sentence));
            }
        }
        else
        {
            // 当前对话所有句子已显示完，结束对话
            EndCurrentDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;

        // 缓存当前对话和Text组件的引用
        Dialogue typingDialogue = currentDialogue;
        Text typingText = currentDialogue?.dialogueText;

        if (typingDialogue == null || typingText == null)
        {
            Debug.LogWarning("TypeSentence: 对话已结束，无法显示文字");
            isTyping = false;
            yield break;
        }

        typingText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (typingText != null)
            {
                typingText.text += letter;
            }
            else
            {
                Debug.LogWarning("TypeSentence: Text组件丢失");
                break;
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndCurrentDialogue()
    {
        // 停止正在进行的打字协程
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;

        // 关闭当前对话面板
        if (currentDialogue != null && currentDialogue.dialoguePanel != null)
        {
            currentDialogue.dialoguePanel.SetActive(false);
        }

        // 根据当前状态设置等待状态
        switch (currentState)
        {
            case DialogueState.Dialogue1:
            case DialogueState.Dialogue2:
            case DialogueState.Dialogue3:
                currentState = DialogueState.WaitingForNext;
                break;
        }

        currentDialogue = null;
       // Renderer renderer = Background.GetComponent<Renderer>();
       // renderer.material = DefaultMat;
       Camera camera = Camera.main;
       camera.GetComponent<CameraBlurDarken>().enabled = false;
    }

    void CloseAllPanels()
    {
        if (dialogue1 != null && dialogue1.dialoguePanel != null)
            dialogue1.dialoguePanel.SetActive(false);
        if (dialogue2 != null && dialogue2.dialoguePanel != null)
            dialogue2.dialoguePanel.SetActive(false);
        if (dialogue3 != null && dialogue3.dialoguePanel != null)
            dialogue3.dialoguePanel.SetActive(false);
    }

    public void ResetDialogues()
    {
        // 停止所有协程
        StopAllCoroutines();
        isTyping = false;

        currentState = DialogueState.NotStarted;
        currentDialogue = null;

        if (dialogue1 != null) dialogue1.hasShown = false;
        if (dialogue2 != null) dialogue2.hasShown = false;
        if (dialogue3 != null) dialogue3.hasShown = false;

        CloseAllPanels();
    }
}
