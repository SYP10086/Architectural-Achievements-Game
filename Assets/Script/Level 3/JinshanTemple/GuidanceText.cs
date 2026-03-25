using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuidanceText : MonoBehaviour
{
    private int lastDetectedValue;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject text;          // 用于激活/失活的容器物体
    [SerializeField] private TextMeshPro targetText;   // 直接在Inspector中拖拽TMP组件
    private Camera camera;

    void Start()
    {
        lastDetectedValue = -1;
        camera = Camera.main;

        if (targetText == null)
        {
            Debug.LogError("GuidanceText: targetText 未在Inspector中赋值！请拖拽场景中的 TextMeshPro 组件到此字段。");
        }
        else
        {
            Debug.Log("GuidanceText: targetText 引用成功。");
        }
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
                targetText.text = @"诸位，咱们脚下这地方，正能看出金山寺的妙处。常言道""金山寺裹山""——你且抬头看，这满山不是石头泥土，而是一层层的殿阁楼台，把整座山裹得严严实实。远望只见寺院，不见山形，这便是""山在寺中，寺即是山""了。今日我便领诸位走走，看看几处有意思的建筑。";
                break;
            case 1:
                targetText.text = @"这塔立在最高处，却并非孤零零一座——你瞧，它从殿宇间""长""出来，周围屋檐层层叠叠，像花瓣托着花蕊一般。塔身七层，不粗不细，秀气挺拔。登塔远眺，江风浩荡，帆影点点。当年我夜宿金山，常在这塔下与佛印和尚闲谈，江声塔影，至今想来仍觉惬意。";
                break;
            case 2:
                targetText.text = @"这亭子建在山崖拐角处，半悬于外——胆子不小。四面通透，不砌墙壁，只围一圈栏杆，为的便是让你看风景无遮无拦。站在这儿，脚下江水滔滔，天连着水，水连着天，金山便如碧玉浮于银涛之上。这亭子本身倒像个画框，将万里江山框了进去。";
                break;
            case 3:
                targetText.text = @"观音阁从外头看并不起眼，与周围殿宇浑然一体。但一入其间，便觉豁然开朗——殿内无柱，敞亮通透。后头藏着个小天井，天光从那儿照进来，恰好落在观音面上，晨昏明暗，菩萨眉眼便有了不同神色。这叫""外朴内巧""，颇有几分深意。";
                break;
            case 4:
                targetText.text = @"财神殿不大，前头伸出一间抱厦，夏日可卸下门窗，便成了半敞的轩堂，通风凉快。屋顶是卷棚式，弧线柔和，少了庙堂的威严，多了几分亲近。檐下雕着如意、铜钱纹样，虽是世俗祈愿，倒也精致可爱。";
                break;
            case 5:
                targetText.text = @"这地方我得说说。当年中秋，我约了佛印、参寥子二人，便在这台上饮酒赏月。你看这高台，并非天然，而是青石垒成，上建亭阁，四柱撑起，不设墙壁，只围栏杆——这便是""台""的本意了。立于台上，江风拂面，明月当空，真个是""高处不胜寒""。我那首《水调歌头》，虽写于别处，多少也得了几分此间的意境。";
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