using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public string Name;
    public GameObject dialoguePanel;
    [HideInInspector] public  bool hasShown=false;
    public Text dialogueText;
    [TextArea(3, 5)]
    public string[] sentences;

}
