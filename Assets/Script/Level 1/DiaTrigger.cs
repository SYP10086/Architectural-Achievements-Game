using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiaTrigger : MonoBehaviour
{
    public Dialogue Dialogue;
    public GameObject DialoguePanel;
    public string targetScene;
    private bool isTyping;
    private Coroutine typingCoroutine;
    public float typingSpeed = 0.05f;
    private int currentSentenceIndex = 0; // 改为成员变量
    private bool isDialogueActive = false;
    

    void Start()
    {
        DialoguePanel.SetActive(false);
    }

    void Update()
    {
        // 检测玩家输入继续对话
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            
            DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDialogueActive)
        {
            StartDialogue(Dialogue);
        }
    }

    void StartDialogue(Dialogue dialogue)
    {
     
        Dialogue = dialogue;
        isDialogueActive = true;
        currentSentenceIndex = 0;
        DialoguePanel.SetActive(true);

        Camera camera = Camera.main;
        if (camera != null)
        {
            CameraBlurDarken blurEffect = camera.GetComponent<CameraBlurDarken>();
            if (blurEffect != null) blurEffect.enabled = true;
        }

        // 显示第一句对话
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        // 如果正在打字，立即完成当前打字
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            Dialogue.dialogueText.text = Dialogue.sentences[currentSentenceIndex - 1];
            isTyping = false;
            return;
        }

        // 检查是否还有句子
        if (currentSentenceIndex < Dialogue.sentences.Length&&!isTyping)
        {
            string sentence = Dialogue.sentences[currentSentenceIndex];
            typingCoroutine = StartCoroutine(TypeSentence(sentence));
            currentSentenceIndex++;
        }
        else
        {
            EndCurrentDialogue();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        Dialogue.dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            Dialogue.dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndCurrentDialogue()
    {
        // 清理协程
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isTyping = false;
        isDialogueActive = false;
        DialoguePanel.SetActive(false);

        // 恢复相机效果
        Camera camera = Camera.main;
        if (camera != null)
        {
            CameraBlurDarken blurEffect = camera.GetComponent<CameraBlurDarken>();
            if (blurEffect != null) blurEffect.enabled = false;
        }

        // 重置对话状态
        // currentSentenceIndex = 0;
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }

    }
}
