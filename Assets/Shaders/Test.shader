Shader "Custom/Test" {
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Visibility("Visibility", Range(0,1)) = 1
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		Lighting Off Cull Off ZWrite Off Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha


		Pass{
			

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows alpha:fade
		#pragma target 3.0

			sampler2D _MainTex;
			float _Visibility;

			struct Input {
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = _Visibility;
			}
			ENDCG
			Color[_Color]
			SetTexture[_MainTex]{
				combine primary, texture * primary
			}
		}

		
	}
		FallBack "GUI/3D Text Shader"
}