Shader "Custom/PlayerDeath" {

	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[PerRendererData] _DissolveVal ("Dissolve Value", Range(0,1)) = 0.5
		_DissolveTex ("Dissolve Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "RenderType"="Opaque" }

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_DissolveVal

		sampler2D _MainTex;
		sampler2D _DissolveTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_DissolveTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 color = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = color.rgb;
			fixed dissolveTexVal = tex2D(_DissolveTex, IN.uv_DissolveTex).r;
			o.Alpha = dissolveTexVal;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
