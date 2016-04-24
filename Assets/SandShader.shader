﻿Shader "Custom/SandShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Bump("Bump", 2D) = "bump" {}
		_Snow("Sand Level", Range(0,1)) = 0
		_SnowTex("Sand Texture", 2D) = "white" {}
		_SnowDirection("Sand Direction", Vector) = (0,1,0)
		_SnowDepth("Vertex Displacement", Range(0,0.2)) = 0.1
		_Wetness("Soft blending", Range(0, 0.5)) = 0.3
		_SnowColor("Snow Color", Color) = (1.0,1.0,1.0,1.0)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
	sampler2D _Bump;
	float _Snow;
	sampler2D _SnowTex;
	float4 _SnowDirection;
	float _SnowDepth;
	float _Wetness;
	float4 _SnowColor;

	struct Input {
		float2 uv_MainTex;
		float2 uv_Bump;
		float3 worldNormal;
		INTERNAL_DATA
	};

	void vert(inout appdata_full v) {
		//Convert the normal to world coortinates
		float4 sn = mul(UNITY_MATRIX_IT_MV, _SnowDirection);

		if (dot(v.normal, sn.xyz) >= lerp(1,-1, (_Snow * 2) / 3))
		{
			v.vertex.xyz += (sn.xyz + v.normal) * _SnowDepth * _Snow;
		}
	}

	void surf(Input IN, inout SurfaceOutput o) {
		half3 snow = _SnowColor * tex2D(_SnowTex, IN.uv_MainTex);
		half4 c = tex2D(_MainTex, IN.uv_MainTex);

		o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
		half difference = dot(WorldNormalVector(IN, o.Normal), _SnowDirection.xyz) - lerp(1,-1,_Snow);;
		difference = saturate(difference / _Wetness);
		o.Albedo = lerp(c, snow, difference);
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}