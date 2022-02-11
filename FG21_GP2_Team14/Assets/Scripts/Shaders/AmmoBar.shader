Shader "Custom/AmmoBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FullColor("Full Color", Color) = (1,1,1,1)
        _EmptyColor("Empty Color", Color) = (1,1,1,1)
        _BarValue("Bar Value", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _FullColor;
            float4 _EmptyColor;
            float _BarValue;

            Interpolators vert (MeshData v)
            {
                Interpolators i;
                i.vertex = UnityObjectToClipPos(v.vertex);
                i.uv = v.uv;
                return i;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float4 blackCol = float4(0,0,0,0);
                float2 coords = i.uv;
                float minThresh=0.2;
                float maxThresh=0.8;
                
                //Only Colors
                float t =_BarValue;// lerp(step(maxThresh, _BarValue), step(minThresh, _BarValue), _BarValue); 
                float4 color =  lerp(_EmptyColor, _FullColor, t );
                color = lerp(color,blackCol,step(_BarValue,coords.x));
                // clip(color.a-0.0001);// need to be just slightly less than 0 for it to be clipped

                //Solution 1d
                // float4 color  = tex2D(_MainTex, float2(_BarValue, i.uv.y));
                // float healthBarMask= _BarValue > i.uv.x;
                // color=color*healthBarMask;
                //clip(color.a-0.0001);// need to be just slightly less than 0 for it to be clipped

                //fixed4 col = tex2D(_MainTex, i.uv);
                return color;
            }
            ENDCG
        }
    }
}
