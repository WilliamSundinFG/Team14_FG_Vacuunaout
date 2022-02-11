Shader "Unlit/ScreenspaceTex"
{
    Properties
    {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Geometry"
        }

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float4 screenPosition : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.position);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                float2 textureCoordinate = i.screenPosition.xy / i.screenPosition.w;
                float aspect = _ScreenParams.x / _ScreenParams.y;
                textureCoordinate.x = textureCoordinate.x * aspect;
                textureCoordinate = TRANSFORM_TEX(textureCoordinate, _MainTex);
                fixed4 col = tex2D(_MainTex, textureCoordinate);
                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
}