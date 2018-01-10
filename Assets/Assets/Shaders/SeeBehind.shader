 Shader "Custom/SeeBehind" {
 Properties {
     _Color ("Main Color", Color) = (1,1,1,1)
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	 _Transparency("Transparency", Range(0.0,0.75)) = 0.25
	 _ObjPos ("ObjPos", Vector) = (1,1,1,1)
	 _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	 _Radius ("HoleRadius", Range(0.1,5)) = 2
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 200
 
 CGPROGRAM
 #pragma surface surf Lambert alpha
 
 sampler2D _MainTex;
 fixed4 _Color;
 float _Transparency;
 
 struct Input {
     float2 uv_MainTex;
 };
 
 void surf (Input IN, inout SurfaceOutput o) {
     fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
     o.Albedo = c.rgb;
     o.Alpha = _Transparency;
 }
 ENDCG
 }
 
 Category 
     {
         SubShader 
         { 
             Tags { "Queue"="Transparent" "RenderType"="Transparent"}
 
             Pass
             {
                 ZTest Greater
                 Lighting Off
                 
                 SetTexture [_MainTex]
             }
             
             Pass 
             {
                 ZTest Less          
                 SetTexture [_MainTex]
             }
 
             
         }
     }
 
 Fallback "Transparent/VertexLit"
 }