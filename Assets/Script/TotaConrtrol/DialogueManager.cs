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

    // 静态变量，跨场景保存
    public static bool hasShown1 = false;
    public static bool hasShown2 = false;
    public static bool hasShown3 = false;

    private Dialogue currentDialogue;
    private int currentSentenceIndex;
    private bool isTyping;
    private Coroutine typingCoroutine;

    private enum DialogueState
    {
        NotStarted,
        Dialogue1,
        Dialogue2,
        Dialogue3,
        WaitingForNext,
        Completed
    }

    private DialogueState currentState = DialogueState.NotStarted;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ValidateDialogues();
        CloseAllPanels();

        // 根据静态变量恢复状态
        if (hasShown1 && !hasShown2)
        {
            currentState = DialogueState.WaitingForNext;
        }
        else if (hasShown1 && hasShown2 && !hasShown3)
        {
            currentState = DialogueState.WaitingForNext;
        }
        else if (hasShown1 && hasShown2 && hasShown3)
        {
            currentState = DialogueState.Completed;
        }
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

    public void OnButtonClick()
    {
        switch (currentState)
        {
            case DialogueState.NotStarted:
                StartDialogue(dialogue1);
                currentState = DialogueState.Dialogue1;
                break;

            case DialogueState.Dialogue1:
            case DialogueState.Dialogue2:
            case DialogueState.Dialogue3:
                DisplayNextSentence();
                break;

            case DialogueState.WaitingForNext:
                StartNextDialogue();
                break;

            case DialogueState.Completed:
                Debug.Log("所有对话已完成");
                break;
        }
    }

    void StartDialogue(Dialogue dialogue)
    {
        GameObject trigger = GameObject.Find("Trigger");
        if (trigger != null) { trigger.SetActive(true); }

        if (dialogue == null || dialogue.dialogueText == null)
        {
            return;
        }

        CloseAllPanels();
        Camera camera = Camera.main;
        if (camera != null)
            camera.GetComponent<CameraBlurDarken>().enabled = true;

        currentDialogue = dialogue;
        currentSentenceIndex = 0;

        // 使用静态变量记录
        if (dialogue == dialogue1) hasShown1 = true;
        else if (dialogue == dialogue2) hasShown2 = true;
        else if (dialogue == dialogue3) hasShown3 = true;

        if (dialogue.dialoguePanel != null)
            dialogue.dialoguePanel.SetActive(true);

        DisplayNextSentence();
    }

    void StartNextDialogue()
    {
        if (currentState == DialogueState.WaitingForNext)
        {
            if (hasShown1 && dialogue2 != null && !hasShown2)
            {
                StartDialogue(dialogue2);
                currentState = DialogueState.Dialogue2;
            }
            else if (hasShown1 && hasShown2 && dialogue3 != null && !hasShown3)
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

        if (currentSentenceIndex < currentDialogue.sentences.Length)
        {
            DioManager.OnDio = true;
            if (GameObject.Find("Player") != null && GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.y > 0)
                GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            string sentence = currentDialogue.sentences[currentSentenceIndex];

            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                currentDialogue.dialogueText.text = sentence;
                isTyping = false;
                currentSentenceIndex++;
            }
            else
            {
                typingCoroutine = StartCoroutine(TypeSentence(sentence));
            }
        }
        else
        {
            DioManager.OnDio = false;
            EndCurrentDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
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
        currentSentenceIndex++;
        isTyping = false;
    }

    void EndCurrentDialogue()
    {
        GameObject trigger = GameObject.Find("Trigger");
        if (trigger != null) { trigger.SetActive(false); }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isTyping = false;

        if (currentDialogue != null && currentDialogue.dialoguePanel != null)
        {
            currentDialogue.dialoguePanel.SetActive(false);
        }

        switch (currentState)
        {
            case DialogueState.Dialogue1:
            case DialogueState.Dialogue2:
            case DialogueState.Dialogue3:
                currentState = DialogueState.WaitingForNext;
                break;
        }

        currentDialogue = null;

        Camera camera = Camera.main;
        if (camera != null)
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
}