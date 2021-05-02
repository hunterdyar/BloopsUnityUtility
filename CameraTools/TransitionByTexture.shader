Shader "Bloops/CameraUtility/TexTransition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Transition ("Transition",2D) = "white" {}
        _Color ("Color", Color) = (0, 0, 0, 1)
        _Lerp ("Lerp", Range(0, 1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            sampler2D _MainTex;
            sampler2D _Transition;
            float4 _Color;
            float _Lerp;
            float _Smooth;
            
            fixed4 frag (v2f i) : SV_Target
            {
                if(_Lerp == 1)
                {
                    return _Color;
                }
                fixed4 col = tex2D(_MainTex,i.uv);;
                if(_Lerp == 0)
                {
                    return col;
                }
                fixed4 transition = tex2D(_Transition,i.uv);
                
                if (_Lerp >= transition.r)
				{
                    return _Color;
				}

                return col;
            }
            ENDCG
        }
    }
}