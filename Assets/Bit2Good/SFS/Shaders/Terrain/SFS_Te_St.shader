//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Terrain/Specular Translucent"{
	Properties{
		[HideInInspector] _Control	("Control (RGBA)", 2D)	= "red" {}
		[HideInInspector] _Splat3	("Layer 3 (A)", 2D)		= "white" {}
		[HideInInspector] _Splat2	("Layer 2 (B)", 2D)		= "white" {}
		[HideInInspector] _Splat1	("Layer 1 (G)", 2D)		= "white" {}
		[HideInInspector] _Splat0	("Layer 0 (R)", 2D)		= "white" {}
		[HideInInspector] _Normal3	("Normal 3 (A)", 2D)	= "bump" {}
		[HideInInspector] _Normal2	("Normal 2 (B)", 2D)	= "bump" {}
		[HideInInspector] _Normal1	("Normal 1 (G)", 2D)	= "bump" {}
		[HideInInspector] _Normal0	("Normal 0 (R)", 2D)	= "bump" {}
		
		_Wrapping	("Light Wrapping", Range(0.0, 1.0))		= 0.5
		_Color		("Fresnel Color (RGBA)", Color)			= (1.0, 1.0, 1.0, 1.0)
		_Factor		("Fresnel Factor", float)				= 0.5
		_FPow		("Fresnel Power", float)				= 2.0
		_SpecK		("Specularity", float)					= 46.0
		_Strength	("Specular Strength", float)			= 2.0
		_Glossyness	("Gloss", Range(0.0, 1.0))				= 0.25
		_Distr		("SubSurf Dis.", Range(1.0, -2.0))		= 0.5
		_Power		("SubSurf Pow.", Range(0.5, 3.5))		= 2.0
	}
	
	SubShader{
		Tags{
			"SplatCount" = "4"
			"Queue" = "Geometry-100"
			"RenderType" = "Opaque"
		}
		
		CGPROGRAM
		#pragma surface surf SmoothFresnel vertex:vert  fullforwardshadows approxview
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
		sampler2D	_Normal0, _Normal1, _Normal2, _Normal3;
		half4		_Color;
		half		_Factor, _FPow, _SpecK, _Strength, _Distr, _Power;
		fixed		_Wrapping, _Glossyness;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 tLightDir = lightDir + (s.Normal * _Distr);
			half tDot = pow(max(0.0, dot(viewDir, -tLightDir)), _Power) * max(0.0, _Distr * -10.0);
			fixed3 tLight = atten * 2.0 * tDot * s.Alpha * s.Albedo.rgb;
			fixed3 tAlbedo = s.Albedo * _LightColor0.rgb * tLight;
			
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			half spec = _Strength * s.Specular * pow(saturate(dot(normalize(lightDir + viewDir), s.Normal)), _SpecK);
			
			half4 c;
			c.rgb = tAlbedo + (wrap * ((s.Albedo * _LightColor0.rgb * diff) + (spec * lerp(s.Albedo, _LightColor0.rgb, _Glossyness))));
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			fixed4 splat_control = tex2D(_Control, IN.uv_Control);
			
			fixed4 nrm;
			nrm  = splat_control.r * tex2D(_Normal0, IN.uv_Splat0);
			nrm += splat_control.g * tex2D(_Normal1, IN.uv_Splat1);
			nrm += splat_control.b * tex2D(_Normal2, IN.uv_Splat2);
			nrm += splat_control.a * tex2D(_Normal3, IN.uv_Splat3);
			
			fixed splatSum = dot(splat_control, fixed4(1.0, 1.0, 1.0, 1.0));
			fixed4 flatNormal = fixed4(0.5, 0.5, 1.0, 0.5);
			nrm = lerp(flatNormal, nrm, splatSum);
			o.Normal = UnpackNormal(nrm);
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			
			fixed4 col;
			col  = splat_control.r * tex2D(_Splat0, IN.uv_Splat0);
			col += splat_control.g * tex2D(_Splat1, IN.uv_Splat1);
			col += splat_control.b * tex2D(_Splat2, IN.uv_Splat2);
			col += splat_control.a * tex2D(_Splat3, IN.uv_Splat3);
			o.Albedo = col.rgb;
			o.Alpha = nrm.r;
			o.Specular = col.a;
			o.Emission = lerp(o.Albedo, _Color.rgb, _Color.a) * fresnel;
		}
		
		ENDCG  
	}
	Dependency "AddPassShader" = "Hidden/Bit2Good/SFS/Terrain/SpecularTAddPass"
	Fallback "Bit2Good/SFS/Terrain/Diffuse"
}