// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/WorldspaceTiling"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }
    SubShader
    {
	    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    LOD 100

	    ZWrite Off
	    Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag        
            #pragma target 2.0
            #pragma multi_compile_fog    
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Gets the xy position of the vertex in worldspace.
                float2 worldXY = mul(unity_ObjectToWorld, v.vertex).xy;
                // Use the worldspace coords instead of the mesh's UVs.
                o.uv = TRANSFORM_TEX(worldXY, _MainTex);

                UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {                   
                fixed4 col = tex2D(_MainTex, i.uv);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}