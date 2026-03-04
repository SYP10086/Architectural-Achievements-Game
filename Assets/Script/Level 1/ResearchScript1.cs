using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//记录方块的类型
public enum SquareType
{
    Yi,
    Er,
    San,
    Si,
    Shi
}
public class Square : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public int up = 0;//表示方块方向，最开始用了四个方向，其实一个就可以判断
    public SquareType type;//public方块类型，可以在外部修改

    //点击旋转90度，up方向也随之改变，并且调用管理类封装的方法
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.Rotate(0, 0, -90);
            up = (up + 1) % 4;
            SquareManager.manager.StartConnect();
        }
    }
}

