// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Reflect"
{
	Properties
	{
		_Color("Color",Color)=(1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_ReflectionTex("ReflectionTexture", 2D) = "white" {}
		_Flesnel("Flesnel", Range(0, 1)) = 0.02
	}
	SubShader
	{
		Tags
		{
			"RenderType"="Transparent"
			"Queue"="Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 color  : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float3 worldPos : TEXCOORD0;
				half3 tspace0 : TEXCOORD1; 
				half3 tspace1 : TEXCOORD2; 
				half3 tspace2 : TEXCOORD3; 
				float2 uv : TEXCOORD4;
				float4 pos : SV_POSITION;
			};

			
			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				half3 wNormal = UnityObjectToWorldNormal(v.normal);
				half3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 wBitangent = cross(wNormal, wTangent) * tangentSign;
				o.tspace0 = half3(wTangent.x, wBitangent.x, wNormal.x);
				o.tspace1 = half3(wTangent.y, wBitangent.y, wNormal.y);
				o.tspace2 = half3(wTangent.z, wBitangent.z, wNormal.z);
				o.uv = v.uv;
				return o;
			}

			fixd3 _Color;
			sampler2D _MainTex;
			sampler2D _ReflectionTex;
			float _Fresnel;

			fixed4 frag(v2f i) : SV_Target
			{
				half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
				half3 worldNormal;
				worldNormal.x = dot(i.tspace0, tnormal);
				worldNormal.y = dot(i.tspace1, tnormal);
				worldNormal.z = dot(i.tspace2, tnormal);

				half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				half3 worldRefl = reflect(-worldViewDir, worldNormal);
				half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefl);
				half3 skyColor = DecodeHDR(skyData, unity_SpecCube0_HDR);
				fixed4 c = 0;
				c.rgb = skyColor*_Color;
				return c;
			}
			ENDCG
		}
	}
}