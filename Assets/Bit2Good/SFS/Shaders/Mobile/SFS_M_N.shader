//	Bit2Good Smooth Fresnel Shader
//	Author: Sebastian Erben | erben.sebastian@bit2good.com
//	http://bit2good.com
Shader "Bit2Good/SFS/Mobile/Normal"{
	Properties{
		_Diffuse	("Diffuse Texture (RGB)", 2D)		= "white"{}
		_Color		("Fresnel Color (RGB)", Color)		= (1.0, 1.0, 1.0, 1.0)
		_Factor		("Fresnel Factor", float)			= 0.5
		_NormalMap	("Normal Map", 2D)					= "bump"{}
	}
	
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		
		CGPROGRAM
		#pragma surface surf MobileFresnel noambient exclude_path:prepass noforwardadd
		#pragma target 2.0
		
		struct Input{
			half2 uv_Diffuse, uv_NormalMap;
		};
		
		sampler2D	_Diffuse, _NormalMap;
		half4		_Color;
		half		_Factor;
		fixed		_Wrapping;
		
		fixed4 LightingMobileFresnel(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten){
			fixed lvDot = dot(s.Normal, normalize(lightDir + viewDir));
			fixed wrap = atten * lvDot * lvDot;
			fixed fres = _Factor * (1.0 - dot(viewDir, s.Normal));
			
			fixed4 c;
			c.rgb = fres + (wrap * s.Albedo * _LightColor0.rgb);
			c.a = s.Alpha;
			return c;
		}
		
		void surf(Input IN, inout SurfaceOutput o){
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			o.Albedo = tex2D(_Diffuse, IN.uv_Diffuse).rgb;
		}
		
		ENDCG
	}
	FallBack "Mobile/Bumped Diffuse"
}