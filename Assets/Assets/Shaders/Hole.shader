// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/HoleShader"
{
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma exclude_renderers d3d11 xbox360
            #include "UnityCG.cginc"
           
 
           
            struct VertexInput
            {
                half4 vertex : POSITION;            
            };
 
            struct VertOut {
                half4 pos: SV_POSITION;
                half4 scrPos;
            };
 
            VertOut vert(VertexInput v) {
                VertOut o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.scrPos = ComputeScreenPos(o.pos);
				return o;
            }
			
			
 
            fixed4 frag(VertOut i) : COLOR0 {
                float2 wcoord = (i.scrPos.xy/i.scrPos.w);
               
                float vig = clamp(3.0*length(wcoord-0.5),0.0,1.0);
 
                return fixed4(1,1,1,vig);
            }
 
            ENDCG
        }
    }
}