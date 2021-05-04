Shader "Bloops/CameraUtility/WipeTransition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            float4 _Color;
            float _Lerp;
            
            fixed4 frag (v2f i) : SV_Target
            {
                //the uv x position is a nice 0->1 left to right
                fixed4 col = _Color;
                if(i.uv.x < 1-_Lerp)
                {
                    col = tex2D(_MainTex, i.uv);
                }

                return col;
            }
            ENDCG
        }
    }
}