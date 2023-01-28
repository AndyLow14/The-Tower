Shader "Custom/TowerShader" {
    Properties {
        _MainTex ("Fluid Texture", 2D) = "white" {}

        _MainCol ("Main Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Pass {
            Tags { "RenderType"="Opaque" }
       
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _MainCol;
 
            sampler2D _MainTex;
            struct v2f {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                v.vertex.x += sign(v.vertex.x) * sin(_Time.w)/10;
                v.vertex.y += sign(v.vertex.y) * cos(_Time.w)/12;
                v.vertex.z += sign(v.vertex.z) * sin(_Time.w)/10;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
 
            float4 frag(v2f i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv - float2(0, cos(_Time.w / 10)));
                c.rgb = _MainCol.rgb * c * 1.5;
                return c;
            }
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}