Shader "Unlit/ColorByRadius" {
	Properties{
		_DefaultColor("Default Color", Color) = (0, 0, 1, 1)
		_AugmentedColor("Alternative Color", Color) = (1, 0, 0, 1)
		_AmpulePos("Ampule World Position", Vector) = (0, 0, 0)
		_Radius("Radius", float) = 1.55
	}
		SubShader{
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent"}

			// Alow transparency
			ZWrite off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass{

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform float4 _DefaultColor;
				uniform float4 _AugmentedColor;
				uniform float4 _AmpulePos;
				uniform float  _Radius;

				struct vertexInput {
					float4 vertex : POSITION;
				};

				struct vertexOutput {
					float4 pos : SV_POSITION;
					float4 worldPos : TEXCOORD0;
				};

				vertexOutput vert(vertexInput input) {
					vertexOutput output;

					float4x4 modelMatrix = unity_ObjectToWorld;

					output.pos = UnityObjectToClipPos(input.vertex);
					output.worldPos = mul(modelMatrix, input.vertex);

					return output;
				}

				float4 frag(vertexOutput input) : COLOR{
					float4 color = _DefaultColor;
					if (distance(input.worldPos, _AmpulePos) > _Radius)
						color = _AugmentedColor;
					return float4 (color);
				}
				ENDCG
			}
	}
}