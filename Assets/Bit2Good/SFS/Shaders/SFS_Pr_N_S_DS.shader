//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Prototype/Normal/Specular/Dissolve Self-Illumination"{
	Properties{
		[HideInInspector] _Color	("Cutout", Color)			= (1.0, 1.0, 1.0, 1.0)
		_Diffuse		("Diffuse Texture (RGB)", 2D)			= "white"{}
		_Wrapping		("Light Wrapping", Range(0.0, 1.0))		= 0.5
		_FColor			("Fresnel Color (RGBA)", Color)			= (1.0, 1.0, 1.0, 1.0)
		_Factor			("Fresnel Factor", float)				= 0.5
		_FPow			("Fresnel Power", float)				= 2.0
		_NormalMap		("Normal Map", 2D)						= "bump"{}
		_SpecMap		("Specular Map (A)", 2D)				= "gray"{}
		_SpecK			("Specularity", float)					= 46.0
		_Strength		("Specular Strength", float)			= 2.0
		_Glossyness		("Gloss", Range(0.0, 1.0))				= 0.25
		_MainTex		("Dissolve Map (A)", 2D)				= "white"{}
		_DisColor		("Dissolve Color", Color)				= (1.0, 1.0, 1.0, 1.0)
		_Cutoff			("Dissolve Status", Range(0.0, 1.0))	= 0.5
		_Illumination	("Illumination (A)", 2D)				= "gray" {}
		_Emission		("Emission (RGBA)", Color)				= (1.0, 1.0, 1.0, 1.0)
	}
	
	SubShader{
		Tags{
			"Queue"				= "AlphaTest"
			"RenderType"		= "TransparentCutout"
			"IgnoreProjector"	= "True"
		}
    	
		CGPROGRAM
		#pragma surface surf SmoothFresnel alphatest:_Cutoff fullforwardshadows noambient
		#pragma target 3.0
		
		struct Input{
			float4 color : COLOR;
			half2 uv_Diffuse, uv_NormalMap, uv_SpecMap, uv_MainTex, uv_Illumination;
			half3 viewDir;
		};
		
		sampler2D	_Diffuse, _NormalMap, _SpecMap, _MainTex, _Illumination;
		half4		_FColor, _DisColor, _Emission;
		half		_Factor, _FPow, _SpecK, _Strength;
		fixed		_Wrapping, _Glossyness;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			half spec = _Strength * s.Specular * pow(saturate(dot(normalize(lightDir + viewDir), s.Normal)), _SpecK);
			
			half4 c;
			c.rgb = wrap * ((s.Albedo * _LightColor0.rgb * diff) + (spec * lerp(s.Albedo, _LightColor0.rgb, _Glossyness)));
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			o.Specular = tex2D(_SpecMap, IN.uv_SpecMap).a;
			if(_DisColor.a == 0.0)	{ o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb * IN.color.rgb; }
			else					{ o.Albedo = lerp(tex2D(_Diffuse, IN.uv_Diffuse).rgb * IN.color.rgb, 1.0 / tex2D(_MainTex, IN.uv_MainTex).rgb * _DisColor.rgb * lerp(1.0, 4.0, _DisColor.a), _DisColor.a); }
			o.Emission = lerp(o.Albedo, _FColor.rgb, _FColor.a) * fresnel + (tex2D(_Illumination, IN.uv_Illumination).a * _Emission.rgb * _Emission.a);
			o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
		}
		
		ENDCG
	}
	Fallback "Transparent/Cutout/Diffuse"
}