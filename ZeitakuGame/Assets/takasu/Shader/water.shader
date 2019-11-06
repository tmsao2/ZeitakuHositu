Shader "Unlit/Water"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)
		_WaveTex1("NormalTexture1", 2D) = "bump" {}
		_WaveTex2("NormalTexture2", 2D) = "bump"{}
		_WaveTex1H("HightTexture1",2D) = "white"{}
		_WaveTex2H("HightTexture2",2D) = "white"{}
		_WaveTiling("Wave Tiling",vector) = (0,0,0,0)
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_FlowSpeed("FlowSpeed",vector) = (0,0,0,0)
		_Refraction("Refraction",vector) = (0,0,0,0)
		_Displace("Displace", vector) = (0, 0, 0, 0)
		_EdgeLength("Edge length", Range(3,50)) = 10
		_Flesnel("Flesnel",Range(0,1)) = 0.5
    }
    SubShader
    {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

		GrabPass{"_GrabTex"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
			#include "Tessellation.cginc

            struct appdata
            {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 color  : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
            };

            struct v2f
            {
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
				float4 color : COLOR;
				float4 grabPos :TEXCOORD1;
            };

			fixed3 _Color;
			sampler2D _WaveTex1;
			sampler2D _WaveTex2;
			sampler2D _WaveTex1H;
			sampler2D _WaveTex2H;
			half4 _WaveTiling;
			half _Glossiness;
			half4 _FlowSpeed;
			half4 _Refraction;
			half4 _Displace;
			half _EdgeLength;
			half _Flesnel;

			float4 tessEdge(appdata v0, appdata v1, appdata v2) {
				return UnityEdgeLengthBasedTessCull(v0.vertex, v1.vertex, v2.vertex, _EdgeLength, 1.5f);
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _WaveTex1);
				fixed waveTex1 = tex2Dlod(_WaveTex1H, float4(v.texcoord.xy * _WaveTiling.x + float2(0, _Time.x * _FlowSpeed.x), 0, 0)).a * _Displace.x;
				fixed waveTex2 = tex2Dlod(_WaveTex2H, float4(v.texcoord.xy * _WaveTiling.y + float2(0, _Time.x * _FlowSpeed.y), 0, 0)).a * _Displace.y;
				fixed displace = waveTex1 + waveTex2;
				o.
				o.vertex.xyz += v.normal * displace;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
			    float4 waveTex1 = tex2D(_WaveTex1, i.uv * _WaveTiling.x + float2(0,_Time.x * _FlowSpeed.x));
				float4 waveTex2 = tex2D(_WaveTex2, i.uv * _WaveTiling.y + float2(0,_Time.x * _FlowSpeed.x));

				fixed3 normal1 = UnpackNormal(waveTex1);
				fixed3 normal2 = UnpackNormal(waveTex2);
				fixed3 normal = BlendNormals(normal1, normal2);

				fixed3 distortion1 = UnpackScaleNormal(waveTex1, _Refraction.x);
				fixed3 distortion2 = UnpackScaleNormal(waveTex2, _Refraction.y);
				fixed2 distortion = BlendNormals(distortion1, distortion2).rg;

				float2 grabUV = (i.grabPos.xy / i.grabPos.w) * float2(1, -1) + float2(0, 1);
				float4 col = tex2D(_GrabTex, grabUV) * _Color;
                // apply fog
                return col;
            }
            ENDCG
        }
    }
}
