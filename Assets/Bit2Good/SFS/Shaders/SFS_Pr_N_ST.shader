//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Prototype/Normal/Self-Illumination Transparency"{
	Properties{
		_Diffuse		("Diffuse Texture (RGBA)", 2D)		= "white"{}
		_Wrapping		("Light Wrapping", Range(0.0, 1.0))	= 0.5
		_Color			("Fresnel Color (RGBA)", Color)		= (1.0, 1.0, 1.0, 1.0)
		_Factor			("Fresnel Factor", float)			= 0.5
		_FPow			("Fresnel Power", float)			= 2.0
		_NormalMap		("Normal Map", 2D)					= "bump"{}
		_Illumination	("Illumination (A)", 2D)			= "gray" {}
		_Emission		("Emission (RGBA)", Color)			= (1.0, 1.0, 1.0, 1.0)
		_Transparency	("Transparency", float)				= 0.5
	}
	
	SubShader{
		Tags{
			"Queue" 			= "Transparent"
			"RenderType" 		= "Transparent"
			"IgnoreProjector"	= "True"
		}
		
		Pass{
			Cull Off
			ZWrite On
        	ColorMask 0
    	}
    	
		CGPROGRAM
		#pragma surface surf SmoothFresnel alpha fullforwardshadows noambient
		
		struct Input{
			half4 color : COLOR;
			half2 uv_Diffuse, uv_NormalMap, uv_Illumination;
			half3 viewDir;
		};
		
		sampler2D	_Diffuse, _NormalMap, _Illumination;
		half4		_Color, _Emission;
		half		_Factor, _FPow;
		fixed		_Wrapping, _Transparency;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			
			half4 c;
			c.rgb = wrap * s.Albedo * _LightColor0.rgb * diff;
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb * IN.color.rgb;
			o.Emission = lerp(o.Albedo, _Color.rgb, _Color.a) * fresnel + (tex2D(_Illumination, IN.uv_Illumination).a * _Emission.rgb * _Emission.a);
			o.Alpha = tex2D(_Diffuse, IN.uv_Diffuse).a * _Transparency;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}