Shader "Custom/BlurShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        [PerRendererData] _SpriteTex("Sprite Texture", 2D) = "white" {}
        _BlurSize("Blur Radius", Range(0,10)) = 3
        _BlurIntensity("Blur Intensity", Range(0,1)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    
    // 模糊效果SubShader
    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
        }
        
        // GrabPass - 捕获当前屏幕内容
        GrabPass
        {
            "_BackgroundTexture"
        }
        
        Pass
        {
            Name "Gaussian Blur"
            
            Cull Off
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 grabPos : TEXCOORD1;
                fixed4 color : COLOR;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BackgroundTexture;
            float4 _BackgroundTexture_TexelSize;
            float _BlurSize;
            float _BlurIntensity;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                // 计算屏幕坐标
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                
                o.color = v.color;
                return o;
            }
            
            // 高斯模糊函数
            fixed4 gaussianBlur(sampler2D tex, float2 uv, float2 offset, int samples)
            {
                float2 texelSize = _BackgroundTexture_TexelSize.xy;
                
                // 高斯核权重 (5x5高斯核)
                float weights[5] = {0.0545, 0.2442, 0.4026, 0.2442, 0.0545};
                
                fixed4 color = 0;
                float totalWeight = 0;
                
                // 水平方向采样
                for (int i = -2; i <= 2; i++)
                {
                    float2 sampleUV = uv + float2(texelSize.x * i * _BlurSize, 0);
                    color += tex2D(tex, sampleUV) * weights[i+2];
                    totalWeight += weights[i+2];
                }
                
                // 垂直方向采样
                for (int j = -2; j <= 2; j++)
                {
                    float2 sampleUV = uv + float2(0, texelSize.y * j * _BlurSize);
                    color += tex2D(tex, sampleUV) * weights[j+2];
                    totalWeight += weights[j+2];
                }
                
                return color / (totalWeight * 0.5); // 归一化
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // 获取屏幕空间UV
                float2 screenUV = i.grabPos.xy / i.grabPos.w;
                
                // 原始纹理颜色
                fixed4 originalColor = (tex2D(_MainTex, i.uv) + _TextureSampleAdd) * i.color * _Color;
                
                // 如果模糊强度为0，直接返回原始颜色
                if (_BlurIntensity <= 0)
                {
                    return originalColor;
                }
                
                // 如果模糊强度大于0，应用模糊
                if (_BlurIntensity > 0)
                {
                    // 执行高斯模糊
                    fixed4 blurredColor = gaussianBlur(_BackgroundTexture, screenUV, _BackgroundTexture_TexelSize.xy, 5);
                    
                    // 混合原始颜色和模糊背景
                    fixed4 finalColor = lerp(originalColor, blurredColor, _BlurIntensity);
                    finalColor.a = originalColor.a; // 保持原始透明度
                    
                    return finalColor;
                }
                
                return originalColor;
            }
            ENDCG
        }
    }
    
    FallBack "Diffuse"
}