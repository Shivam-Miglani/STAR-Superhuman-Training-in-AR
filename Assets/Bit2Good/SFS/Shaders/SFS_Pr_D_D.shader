//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Prototype/Diffuse/Dissolve"{
	Properties{
		[HideInInspector] _Color	("Cutout", Color)		= (1.0, 1.0, 1.0, 1.0)
		_Diffuse	("Diffuse Texture (RGB)", 2D)			= "white"{}
		_Wrapping	("Light Wrapping", Range(0.0, 1.0))		= 0.5
		_FColor		("Fresnel Color (RGBA)", Color)			= (1.0, 1.0, 1.0, 1.0)
		_Factor		("Fresnel Factor", float)				= 0.5
		_FPow		("Fresnel Power", float)				= 2.0
		_MainTex	("Dissolve Map (A)", 2D)				= "white"{}
		_DisColor	("Dissolve Color", Color)				= (1.0, 1.0, 1.0, 1.0)
		_Cutoff		("Dissolve Status", Range(0.0, 1.0))	= 0.5
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
		#pragma profileoption MaxTexIndirections=5
		
		struct Input{
			half4 color : COlOR;
			half2 uv_Diffuse, uv_MainTex;
			half3 viewDir;
		};
		
		sampler2D	_Diffuse, _MainTex;
		half4		_FColor, _DisColor;
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
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			half3 diffTex = tex2D(_Diffuse, IN.uv_Diffuse).rgb;
			half4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			if(_DisColor.a == 0.0)	{ o.Albedo = diffTex * IN.color.rgb; }
			else					{ o.Albedo = lerp(diffTex * IN.color.rgb, 1.0 / mainTex.rgb * _DisColor.rgb * lerp(1.0, 4.0, _DisColor.a), _DisColor.a); }
			o.Emission = lerp(o.Albedo, _FColor.rgb, _FColor.a) * fresnel;
			o.Alpha = mainTex.a;
		}
		
		ENDCG
	}
	Fallback "Transparent/Cutout/Diffuse"
}