//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Mobile/Normal Specular Self-Illumination"{
	Properties{
		_Diffuse		("Diffuse Texture (RGB)", 2D)		= "white"{}
		_Color			("Fresnel Color (RGB)", Color)		= (1.0, 1.0, 1.0, 1.0)
		_Factor			("Fresnel Factor", float)			= 0.5
		_NormalMap		("Normal Map", 2D)					= "bump"{}
		_SpecK			("Specularity", float)				= 46.0
		_Strength		("Specular Strength", float)		= 2.0
		_Illumination	("Illumination (RGBA)", 2D)			= "gray" {}
		_Emission		("Emission", Range(0.0, 1.0))		= 1.0
	}
	
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf MobileFresnel noambient exclude_path:prepass noforwardadd
		#pragma target 2.0
		
		struct Input{
			half2 uv_Diffuse, uv_NormalMap, uv_Illumination;
		};
		
		sampler2D	_Diffuse, _NormalMap, _Illumination;
		half4		_Color;
		half		_Factor, _SpecK, _Strength, _Glossyness;
		fixed		_Wrapping, _Emission;
		
		fixed4 LightingMobileFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			fixed nlvDot = dot(s.Normal, normalize(lightDir + viewDir));
			fixed wrap = atten * nlvDot * nlvDot;
			fixed fres = _Factor * (1.0 - dot(viewDir, s.Normal));
			half spec = _Strength * pow(saturate(nlvDot), _SpecK);
			
			fixed4 c;
			c.rgb = fres + (wrap * s.Albedo * _LightColor0.rgb) + (spec * _LightColor0.rgb);
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			fixed4 illum = tex2D(_Illumination, IN.uv_Illumination);
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb;
			o.Emission = illum.a * illum.rgb * _Emission;
		}
		
		ENDCG
	}
	FallBack "Mobile/Bumped Specular"
}