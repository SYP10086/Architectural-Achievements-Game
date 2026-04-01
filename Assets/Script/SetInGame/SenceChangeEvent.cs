using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MyDel(string senceName);
public class SenceChangeEvent : MonoBehaviour
{
    public static MyDel SenceChange;
}
