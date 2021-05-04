Shader "Bloops/CameraUtility/TextureOverlay"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OverlayTex ("Overlay",2D) = "white" {}
        _Intensity ("Intensity",Range(0,1)) = 0
        _MultScreen ("Factor",Range(0,1)) = 0.5
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
            sampler2D _OverlayTex;
            float _Intensity;
            float _MultScreen;
            
            float overlayChannel(float a,float b)
            {
                //Overlay function from wikipedia.
                if(a < _MultScreen)
                    return 2*a*b;
                else
                {
                    return 1-2*(1-a)*(1-b);
                }
            }

            float4 overlay4(float4 a,float4 b)
            {
                return float4(overlayChannel(a.x,b.x),overlayChannel(a.y,b.y),overlayChannel(a.z,b.z),overlayChannel(a.w,b.w));
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
               
                fixed4 a = tex2D(_MainTex,i.uv);;
                fixed4 b = tex2D(_OverlayTex,i.uv);
                return lerp(a,overlayChannel(a,b),_Intensity);       
            }
            ENDCG
        }
    }
}