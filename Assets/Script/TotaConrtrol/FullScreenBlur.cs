using UnityEngine;

// 挂载到相机上
[RequireComponent(typeof(Camera))]
public class FullScreenBlur : MonoBehaviour
{
    public Material blurMaterial;
    public float blurSize = 3f;
    public float blurIntensity = 0.5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

     
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (blurMaterial != null && blurIntensity > 0)
        {
            // 更新材质参数
            blurMaterial.SetFloat("_BlurSize", blurSize);
            blurMaterial.SetFloat("_BlurIntensity", blurIntensity);

            // 应用模糊效果到整个画面
            Graphics.Blit(source, destination, blurMaterial);
        }
        else
        {
            // 无模糊，直接复制
            Graphics.Blit(source, destination);
        }
    }
}