Shader "Custom/BeatInnerRing"
{
      Properties {
        _OuterRadius ("Outer Radius", Range (0, 1)) = 0.3
        _InnerRadius ("Inner Radius", Range (0, 1)) = 0.25
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _OuterRadius;
            float _InnerRadius;
            float4 _Color;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv - 0.5;
                float dist = length(uv);

                // Check if the pixel is inside or outside the circular ring
                if (dist < _InnerRadius || dist > _OuterRadius) {
                    discard;
                }

                float4 color = _Color;
                color.a = 1.0 - smoothstep(_InnerRadius, _OuterRadius, dist);
                return color;
            }
            ENDCG
        }
    }
}
