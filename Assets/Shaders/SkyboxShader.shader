Shader "thquinn/SkyboxShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color", Color) = (1,1,1,1)
        _Color3 ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 position : POSITION;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float4 vec : TEXCOORD0;
            };

            fixed4 _Color, _Color2, _Color3;

            v2f vert(appdata vertex) 
            {
                v2f output;
                output.position = UnityObjectToClipPos(vertex.position);
                output.vec = normalize(mul(unity_ObjectToWorld, vertex.position));
                return output;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = lerp(_Color, _Color2, i.vec.y);
                float z = (i.vec.z + 1) / 2;
                c = lerp(c, _Color3, i.vec.z * .1);
                return c;
            }
            ENDCG
        }
    }
}
