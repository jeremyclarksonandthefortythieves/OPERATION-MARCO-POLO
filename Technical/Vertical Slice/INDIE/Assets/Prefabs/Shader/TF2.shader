Shader "TF2"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}	
		_BumpMap("Bump Map", 2D) = "normal" {}
		_Ramp("Ramp", 2D) = "white" {}
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_DiffuseScale("Diffuse Scale", Float) = 1
		_DiffuseBias("Diffuse Bias", Float) = 0
		_DiffuseExponent("Diffuse Exponent", Float) = 1
		_RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Rim Power", Range(0.5, 10.0)) = 3.0
		_SpecColor("Specular Color", Color) = (0.0, 0.0, 0.0, 1.0)
		_SpecPower("Specular Power", Range(0.5, 126.0)) = 3.0
		_OutlineWidth ("Outline Width", Float ) = 0.05
        _OutlineColor ("Outline Color", Color) = (0.5,0.5,0.5,1)
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		
		Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            uniform float _OutlineWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_OutlineWidth,1));
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
        }
		LOD 200
 
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma surface surf NPR noambient
 
		// These match the shader properties
		uniform sampler2D _MainTex, _BumpMap, _Ramp;
		uniform float4 _Color, _RimColor;
		uniform float _DiffuseScale, _DiffuseBias, _DiffuseExponent;
		uniform float _RimPower, _SpecPower;
 
		half4 LightingNPR(SurfaceOutput o, half3 lightdir, half3 viewdir, half atten)
		{
			// Half Lambert
			float lambert = saturate(dot(o.Normal, lightdir));
			lambert = pow(lambert*_DiffuseScale + _DiffuseBias, _DiffuseExponent);
			half4 diffuse = half4(_LightColor0.rgb * atten * o.Albedo.rgb, 1.0);
			diffuse *= tex2D(_Ramp, float2(lambert, 0.0));
			
			// Crazy Specularlity
			half3 r = reflect(-lightdir, o.Normal);
			float phong = pow(saturate(dot(r, viewdir)), _SpecPower);
			half4 specular = half4(phong * _SpecColor * atten);
 
			// Rim Lighting based on Normals (bump)
			float rim_term = 1.0 - saturate(dot(viewdir, o.Normal));
			rim_term = pow(rim_term, _RimPower);
			half4 rim = half4(_RimColor.rgb * rim_term, 1.0); 
 
			return diffuse + rim + specular;
		}
 
		struct Input
		{
			float2 uv_MainTex;
		};
 
		void surf(Input IN, inout SurfaceOutput o)
		{
			
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = _Color.rgb * c;					
			o.Alpha = 1.0;								
 
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		}
		ENDCG
	}
	FallBack "Diffuse"	// Shader to use if the user's hardware cannot incorporate this one
}

//fragmentInput vert(vertexInput i){
//               	fragmentInput o;
//                
//            	float4x4 modelMatrix = _Object2World;
//            	float4x4 modelMatrixInverse = _World2Object; 
//               	
//            	o.posWorld = mul(modelMatrix, i.vertex);
//            	o.normalDir = normalize(mul(float4(i.normal, 0.0), modelMatrixInverse).xyz);
//            	o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
//            	return o;
//            }