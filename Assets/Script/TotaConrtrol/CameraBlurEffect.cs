using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraBlurDarken : MonoBehaviour
{
    [Header("模糊设置")]
    [Range(0, 10)]
    public float blurSize = 3f;          // 模糊半径
    [Range(0, 1)]
    public float blurIntensity = 0.5f;   // 模糊强度（0=无模糊，1=完全模糊）
    public LayerMask ExcludeLayer;

    [Header("压暗设置")]
    [Range(0, 1)]
    public float darkenAmount = 0.3f;     // 压暗程度（0=无压暗，1=全黑）


    private Material material;
    private Shader shader;

    void Start()
    {
        InitializeMaterial();
    }

    void InitializeMaterial()
    {
        if (material == null)
        {
            // 查找内置的 Shader（名称为上面定义的 "Custom/CameraBlurDarken"）
            shader = Shader.Find("Custom/CameraBlurDarken");
            if (shader == null)
            {
               // Debug.LogError("找不到 Shader: Custom/CameraBlurDarken，请确保 Shader 文件名正确且已导入。");
                enabled = false;
                return;
            }
            material = new Material(shader);
            material.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            InitializeMaterial();
            if (material == null)
            {
                Graphics.Blit(source, destination);
                return;
            }
        }

        // 传递参数给 Shader
        material.SetFloat("_BlurSize", blurSize);
        material.SetFloat("_BlurIntensity", blurIntensity);
        material.SetFloat("_Darken", darkenAmount);

        // 应用特效
        Graphics.Blit(source, destination, material);
    }

    void OnValidate()
    {
        // 编辑模式下实时更新参数
        if (material != null)
        {
            material.SetFloat("_BlurSize", blurSize);
            material.SetFloat("_BlurIntensity", blurIntensity);
            material.SetFloat("_Darken", darkenAmount);
        }
    }

    void OnDestroy()
    {
        if (material != null)
        {
            DestroyImmediate(material);
        }
    }
}