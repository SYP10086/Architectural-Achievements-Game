using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidanceText : MonoBehaviour
{
    private int lastDetectedValue;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject text;          
    [SerializeField] private TextMeshPro targetText;   
    private Camera camera;

    void Start()
    {
        lastDetectedValue = -1;
        camera = Camera.main;

    }

    void Update()
    {
        int currentValue = DestinationMovement.DestinationCount;

        if (currentValue != lastDetectedValue)
        {
            HandleDestinationCountChange(currentValue);
            lastDetectedValue = currentValue;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ToggleObject();
        }
    }

    void HandleDestinationCountChange(int currentValue)
    {
        if (targetText == null) return;

        if (!targetText.enabled)
            targetText.enabled = true;

        if (text != null) text.SetActive(true);

        switch (currentValue)
        {
            case 0:
                targetText.text = @"诸位，我这老宅的魂魄，就在一个‘裹’字上。你且看这片园子——山石顺着地势蔓延，林木层层叠叠，将亭台楼阁都‘裹’入怀中。建筑不争不抢，高低错落，像从泥土里长出来似的。这叫‘山形入宅，宅隐于山’。咱们边走边看，你就明白其中的妙处了。(按QE转动视角，寻找光点，用鼠标拖动建筑至光点处)";
                break;
            case 1:
                targetText.text = @"这座亭子藏在竹林深处，四面被翠竹‘裹’着，只露一个顶。它建在一块微微隆起的小山包上，顺着地势抬高了三尺。四面通透，无墙无碍。佛家说利、衰、毁、誉等八风，无论哪一阵来，亭子都岿然不动。坐在这里，竹影摇曳，风过有声——那份‘也无风雨也无晴’的心境，正是被这山林裹出来的。";
                break;
            case 2:
                targetText.text = @"这亭子倚着一道缓坡，地基随山形略微抬高，斗拱清瘦硬朗，像山脊上的一根骨。取名自宋玉，取其风流儒雅。你看它周围的梅竹，疏朗有致，把亭子半遮半露——不是建在平地上生硬地摆着，而是被草木‘裹’得恰到好处。文人造园，要的就是这份‘雅’与‘隐’。";
                break;
            case 3:
                targetText.text = @"这轩建在园中地势较高处，石基沉稳，像扎进山土里。穿斗与抬梁结合，立柱如风骨般挺拔。它不像亭子那么轻灵，有一种沉甸甸的分量，仿佛是从山体中直接‘裹’出来的。这里藏着苏家‘奋厉有当世志’的入世精神——读书人的风骨，也要像这山石一样，稳得住。";
                break;
            case 4:
                targetText.text = @"这座重檐歇山顶的二层楼阁，坐南朝北，背临瑞莲西池。它的妙处在于：后身靠着一段土坡，前身探向池水，整个建筑像被山和水‘裹’在中间。正脊上有寿字宝瓶，下层四面通透，设有美人靠。风从池上来，被飞檐轻轻‘披’开。你倚着栏杆，背后是山势，眼前是池水——观景之人，也被这山水裹成了一景。当年陆游来此，就是站在这里感叹我眉山的‘诗书城’气象。";
                break;
            case 5:
                targetText.text = @"哈哈，我最爱的海棠亭来了。这亭子建在一片低洼处，四周的海棠花树将它密密‘裹’住。屋顶弧线如海棠花瓣舒展，雕花柔美。我曾写‘嫣然一笑竹篱间’——这亭子就像那位嫣然一笑的佳人，半藏半露，羞答答地躲在花与竹之间。每到花开时节，我恨不得‘烧高烛照红妆’。你看，若不是这山形地势把亭子‘裹’得这么含蓄，哪来这份痴情？";
                break;
            default:
                break;
        }

        camera.GetComponent<CameraBlurDarken>().enabled = true;
        exitButton.SetActive(true);
    }

    public void ToggleObject()
    {
        if (targetText != null)
        {
            targetText.text = "";
            targetText.enabled = false;
        }

        if (text != null) text.SetActive(false);
        exitButton.SetActive(false);
        camera.GetComponent<CameraBlurDarken>().enabled = false;
    }
}