//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Terrain/Diffuse"{
	Properties{
		[HideInInspector] _Control	("Control (RGBA)", 2D)	= "red" {}
		[HideInInspector] _Splat3	("Layer 3 (A)", 2D)		= "white" {}
		[HideInInspector] _Splat2	("Layer 2 (B)", 2D)		= "white" {}
		[HideInInspector] _Splat1	("Layer 1 (G)", 2D)		= "white" {}
		[HideInInspector] _Splat0	("Layer 0 (R)", 2D)		= "white" {}
		
		_Wrapping	("Light Wrapping", Range(0.0, 1.0))		= 0.5
		_Color		("Fresnel Color (RGBA)", Color)			= (1.0, 1.0, 1.0, 1.0)
		_Factor		("Fresnel Factor", float)				= 0.5
		_FPow		("Fresnel Power", float)				= 2.0
	}
	
	SubShader{
		Tags{
			"SplatCount" = "4"
			"Queue" = "Geometry-100"
			"RenderType" = "Opaque"
		}
		
		CGPROGRAM
		#pragma surface surf SmoothFresnel vertex:vert fullforwardshadows approxview noambient
		#pragma target 3.0
		
		void vert (inout appdata_full v){
			v.tangent.xyz = cross(v.normal, float3(0.0, 0.0, 1.0));
			v.tangent.w = -1;
		}
		
		struct Input{
			half2 uv_Control : TEXCOORD0;
			half2 uv_Splat0 : TEXCOORD1;
			half2 uv_Splat1 : TEXCOORD2;
			half2 uv_Splat2 : TEXCOORD3;
			half2 uv_Splat3 : TEXCOORD4;
			half3 viewDir;
		};
		
		sampler2D	_Control;
		sampler2D	_Splat0, _Splat1, _Splat2, _Splat3;
		half4		_Color;
		half		_Factor, _FPow;
		fixed		_Wrapping;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			
			half4 c;
			c.rgb = wrap * s.Albedo * _LightColor0.rgb * diff;
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			fixed4 splat_control = tex2D(_Control, IN.uv_Control);
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			
			fixed4 col;
			col  = splat_control.r * tex2D(_Splat0, IN.uv_Splat0);
			col += splat_control.g * tex2D(_Splat1, IN.uv_Splat1);
			col += splat_control.b * tex2D(_Splat2, IN.uv_Splat2);
			col += splat_control.a * tex2D(_Splat3, IN.uv_Splat3);
			o.Albedo = col.rgb;
			
			o.Emission = lerp(o.Albedo, _Color.rgb, _Color.a) * fresnel;
		}
		
		ENDCG  
	}
	Dependency "AddPassShader" = "Hidden/Bit2Good/SFS/Terrain/DiffuseAddPass"
	Fallback "Nature/Terrain/Diffuse"
}