//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Standard/Normal/Translucent"{
	Properties{
		_Diffuse	("Diffuse Texture (RGB)", 2D)		= "white"{}
		_Wrapping	("Light Wrapping", Range(0.0, 1.0))	= 0.5
		_Color		("Fresnel Color (RGBA)", Color)		= (1.0, 1.0, 1.0, 1.0)
		_Factor		("Fresnel Factor", float)			= 0.5
		_FPow		("Fresnel Power", float)			= 2.0
		_NormalMap	("Normal Map", 2D)					= "bump"{}
		_Distr		("SubSurf Dis.", Range(1.0, -2.0))	= 0.5
		_Power		("SubSurf Pow.", Range(0.5, 3.5))	= 2.0
	}
	
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf SmoothFresnel fullforwardshadows approxview noambient
		#pragma target 3.0
		
		struct Input{
			half2 uv_Diffuse, uv_NormalMap;
			half3 viewDir;
		};
		
		sampler2D	_Diffuse, _NormalMap;
		half4		_Color;
		half		_Factor, _FPow, _Distr, _Power;
		fixed		_Wrapping;
		
		half4 LightingSmoothFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			half3 tLightDir = lightDir + (s.Normal * _Distr);
			half tDot = pow(max(0.0, dot(viewDir, -tLightDir)), _Power) * max(0.0, _Distr * -10.0);
			fixed3 tLight = atten * 2.0 * tDot * s.Alpha * s.Albedo.rgb;
			fixed3 tAlbedo = s.Albedo * _LightColor0.rgb * tLight;
			
			half3 wrap = atten * 2.0 * dot(s.Normal, lightDir + (viewDir * _Wrapping));
			half diff = dot(s.Normal, lightDir) * 0.5 + 0.5;
			
			half4 c;
			c.rgb = tAlbedo + (wrap * s.Albedo * _LightColor0.rgb * diff);
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			half4 norm = tex2D(_NormalMap, IN.uv_NormalMap);
			o.Normal = UnpackNormal(norm);
			half fresnel = _Factor * pow(1.0 - dot(normalize(IN.viewDir), o.Normal), _FPow);
			o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb;
			o.Emission = lerp(o.Albedo, _Color.rgb, _Color.a) * fresnel;
			o.Alpha = norm.r;
		}
		
		ENDCG
	}
	FallBack "Diffuse"
}