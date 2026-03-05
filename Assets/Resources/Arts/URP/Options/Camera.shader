Shader "Custom/CameraBlurDarken"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 10)) = 3
        _BlurIntensity ("Blur Intensity", Range(0, 1)) = 0.5
        _Darken ("Darken Amount", Range(0, 1)) = 0.3   // 压暗程度：0=不压暗，1=完全变黑
    }
    
    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize;
            float _BlurIntensity;
            float _Darken;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            // 高斯模糊（5x5 核，可简化）
            fixed4 gaussianBlur(sampler2D tex, float2 uv, float2 texelSize, float blurSize)
            {
                float2 offset = texelSize * blurSize;
                
                // 简单 3x3 平均模糊（可换成更精细的高斯核）
                fixed4 col = 0;
                col += tex2D(tex, uv + float2(-offset.x, -offset.y));
                col += tex2D(tex, uv + float2(0, -offset.y));
                col += tex2D(tex, uv + float2(offset.x, -offset.y));
                col += tex2D(tex, uv + float2(-offset.x, 0));
                col += tex2D(tex, uv);
                col += tex2D(tex, uv + float2(offset.x, 0));
                col += tex2D(tex, uv + float2(-offset.x, offset.y));
                col += tex2D(tex, uv + float2(0, offset.y));
                col += tex2D(tex, uv + float2(offset.x, offset.y));
                
                return col / 9.0;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // 原始画面
                fixed4 original = tex2D(_MainTex, i.uv);
                
                // 如果没有模糊或压暗，直接返回
                if (_BlurIntensity <= 0 && _Darken <= 0)
                    return original;
                
                // 对画面进行模糊
                fixed4 blurred = original;
                if (_BlurIntensity > 0)
                {
                    blurred = gaussianBlur(_MainTex, i.uv, _MainTex_TexelSize.xy, _BlurSize);
                }
                
                // 压暗处理：将模糊后的颜色乘以 (1 - _Darken) 或向黑色插值
                fixed4 darkened = blurred;
                if (_Darken > 0)
                {
                    // 方法1：直接降低亮度
                    darkened.rgb *= (1.0 - _Darken);
                    
                    // 方法2：向黑色线性插值（效果更柔和）
                    // darkened.rgb = lerp(blurred.rgb, fixed3(0,0,0), _Darken);
                }
                
                // 根据模糊强度混合原始和模糊压暗后的结果
                fixed4 result = lerp(original, darkened, _BlurIntensity);
                return result;
            }
            ENDCG
        }
    }
}