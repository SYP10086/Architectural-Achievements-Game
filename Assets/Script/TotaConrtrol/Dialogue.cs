using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update

    public string Name;
    [TextArea(3, 5)]
    public string[] sentences;

}
