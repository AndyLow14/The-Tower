Shader "Custom/EnemyShader"
{
    Properties {
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_GradientTex("Gradient Texture", 2D) = "white" {}
 
		_BrighterCol("Brighter Color", Color) = (1, 1, 1, 1)
		_MiddleCol("Middle Color", Color) = (.6, .6, .6, 1)
		_DarkerCol("Darker Color", Color) = (.3, .3, .3, 1)
	}
 
	SubShader {
		Pass {
			Tags { "RenderType"="Opaque" }
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
 
 
			sampler2D _NoiseTex;
			sampler2D _GradientTex;
 
			float4 _BrighterCol;
			float4 _MiddleCol;
			float4 _DarkerCol;
 
			struct appdata {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
 
			v2f vert(appdata v) {
				v2f o;
				// Change size to create more vivid enemies
				v.vertex.x += sign(v.vertex.x) * cos(_Time.w)/20;
                v.vertex.y += sign(v.vertex.y) * sin(_Time.w)/5;
                v.vertex.z += sign(v.vertex.z) * cos(_Time.w)/20;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
 
				return o;
			}
 
			float4 frag(v2f IN) : SV_Target {
				
				float noiseValue = tex2D(_NoiseTex, IN.uv - float2(0, _Time.x * 10)).x;
				float gradientValue = tex2D(_GradientTex, IN.uv).x;
				
				float fullFlame = step(noiseValue, gradientValue);
				float midFlame = step(noiseValue, gradientValue-0.25);
				float botFlame = step(noiseValue, gradientValue-0.4);
 
				float4 c = float4 (
					lerp (
						_BrighterCol.rgb,
						_DarkerCol.rgb,
						fullFlame - midFlame // Top flame color
					),

					fullFlame
				);
 
				c.rgb = lerp ( // Color of the middle flame's position
					c.rgb,
					_MiddleCol.rgb,
					midFlame - botFlame // Mid flame color
				);
 
				return c * 1.5;
			}
			ENDCG
		}
	
	}
	FallBack "Diffuse"
}