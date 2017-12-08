// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GeroBeam_sh" {
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_BeamLength ("BeamLength", Range (0,1.0)) = 0.0
		_LoopTex ("LoopTex", Float) = 1.0
		_AddTex ("AddTex", Float) = 0.0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One One

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			fixed _BeamLength;
			fixed _LoopTex;
			fixed _AddTex;
			
			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord*half2(_LoopTex,1)+half2(_AddTex,0));
				c.rgb *= c.a;
				c *= IN.color;
				c.rgb += c.rgb*10;
				
				fixed4 c2 = tex2D(_MainTex, IN.texcoord*half2(_LoopTex,1)+half2(_AddTex*2+0.5,0));
				c2.rgb *= pow(c2.a,2);
				c2 *= IN.color;
				c2.rgb += c2.rgb*1;
				
				c.rgb += c2.rgb;
				
				if(IN.texcoord.x > _BeamLength)
				{
					c.rgb = 0;
				}
				return c;
			}
		ENDCG
		}
	}
}
