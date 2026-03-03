using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AreaClickTrigger : MonoBehaviour, IPointerClickHandler
{

      public DialogueManager dialogueManager;
    public Dialogue[] dialogues;  // 按顺序排列的对话数组
    
    private int currentIndex = 0;  // 当前要触发的对话索引
    
    void Start()
    {
        // 确保有Image组件且透明
        Image img = GetComponent<Image>();
        if (img == null)
        {
            img = gameObject.AddComponent<Image>();
        }
        
        Color c = img.color;
        c.a = 0f;
        img.color = c;
        img.raycastTarget = true;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // 检查是否还有未触发的对话
        if (currentIndex >= dialogues.Length)
            return;
        
        // 触发当前对话
        if (dialogueManager != null && dialogues[currentIndex] != null)
        {
           // dialogueManager.StartDialogue(dialogues[currentIndex]);
            currentIndex++;
        }
    }
    
    // 如果需要重置（比如重新开始场景时调用）
    public void ResetDialogues()
    {
        currentIndex = 0;
    }
}