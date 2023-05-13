// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NOT_Lonely/NL_VertexAnim_tess"
{
	Properties
	{
		[Enum(UnityEngine.Rendering.CullMode)]_Cull("Culling", Int) = 2
		_TessPhongStrength( "Phong Tess Strength", Range( 0, 1 ) ) = 0.5
		_Color("Color", Color) = (1,1,1,0)
		_Cutoff("Alpha Clip Threshold", Range( 0 , 1)) = 0.333
		_MainTex("Base Color", 2D) = "white" {}
		_MetallicGlossMap("Metallic (R) AO (G) Height (B) Gloss (A)", 2D) = "black" {}
		_GlossMapScale("Smoothness", Range( 0 , 1)) = 1
		_OcclusionStrength("Occlusion Strength", Range( 0 , 1)) = 1
		_BumpMap("Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Float) = 1
		_Noise("Noise", 2D) = "white" {}
		_PrimaryNoiseTiling("Primary Noise Tiling", Float) = 1
		_SecondaryNoiseTiling("Secondary Noise Tiling", Float) = 8
		_PrimarySpeed("Primary Speed", Float) = 0.5
		_SecondarySpeed("Secondary Speed", Float) = 0.5
		_PrimaryAmplitude("Primary Amplitude", Float) = 0
		_SecondaryAmplitude("Secondary Amplitude", Float) = 0
		_Height("Height", Range( 0 , 0.5)) = 1
		_ZeroHeightOffset("Zero Height Offset", Range( -0.2 , 0.2)) = 0
		_MaxTess("Max Tess", Range( 1 , 32)) = 16
		_TessDistanceMin("Tess Distance Min", Float) = 2
		_TessDistanceMax("Tess Distance Max", Float) = 10
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull [_Cull]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction tessphong:_TessPhongStrength 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform int _Cull;
		uniform float _PrimaryAmplitude;
		uniform sampler2D _Noise;
		uniform float _PrimaryNoiseTiling;
		uniform float _PrimarySpeed;
		uniform float _SecondaryNoiseTiling;
		uniform float _SecondarySpeed;
		uniform float _SecondaryAmplitude;
		uniform sampler2D _MetallicGlossMap;
		uniform float4 _MetallicGlossMap_ST;
		uniform float _Height;
		uniform float _ZeroHeightOffset;
		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform float _NormalScale;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float4 _Color;
		uniform float _GlossMapScale;
		uniform float _OcclusionStrength;
		uniform float _TessDistanceMin;
		uniform float _TessDistanceMax;
		uniform float _MaxTess;
		uniform float _Cutoff;
		uniform float _TessPhongStrength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, _TessDistanceMin,_TessDistanceMax,_MaxTess);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 appendResult140 = (float2(ase_worldPos.x , ase_worldPos.z));
			float3 ase_vertexNormal = v.normal.xyz;
			float2 uv_MetallicGlossMap = v.texcoord * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
			float4 tex2DNode5 = tex2Dlod( _MetallicGlossMap, float4( uv_MetallicGlossMap, 0, 1.0) );
			float Height188 = ( tex2DNode5.b * _Height );
			float3 FinalHeight184 = ( ase_vertexNormal * ( Height188 - _ZeroHeightOffset ) );
			v.vertex.xyz += ( ( ( ( _PrimaryAmplitude * tex2Dlod( _Noise, float4( frac( ( ( ( _PrimaryNoiseTiling * appendResult140 * 0.01 ) + ( _Time.y * _PrimarySpeed ) ) + v.color.r ) ), 0, 0.0) ) ) + ( tex2Dlod( _Noise, float4( frac( ( ( ( 0.01 * appendResult140 * _SecondaryNoiseTiling ) + ( _Time.y * _SecondarySpeed * -1.0 ) ) + v.color.r ) ), 0, 0.0) ) * _SecondaryAmplitude ) ) * v.color.a ) + float4( FinalHeight184 , 0.0 ) ).rgb;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _BumpMap, uv_BumpMap ), _NormalScale );
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
			o.Albedo = ( tex2DNode1 * _Color ).rgb;
			float2 uv_MetallicGlossMap = i.uv_texcoord * _MetallicGlossMap_ST.xy + _MetallicGlossMap_ST.zw;
			float4 tex2DNode5 = tex2D( _MetallicGlossMap, uv_MetallicGlossMap );
			o.Metallic = tex2DNode5.r;
			o.Smoothness = ( tex2DNode5.a * _GlossMapScale );
			o.Occlusion = saturate( ( tex2DNode5.g + ( 1.0 - _OcclusionStrength ) ) );
			o.Alpha = 1;
			clip( tex2DNode1.a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Standard"
}
/*ASEBEGIN
Version=18912
0;73;1920;1047;177.4951;73.97749;1.400639;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;8;-2523.294,76.65894;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;98;-1846.482,1026.223;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-1871.421,921.6698;Float;False;Property;_SecondarySpeed;Secondary Speed;13;0;Create;True;0;0;0;False;0;False;0.5;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-1799.095,136.514;Inherit;False;Constant;_Float2;Float 2;7;0;Create;True;0;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-1854.104,390.8951;Inherit;False;Property;_SecondaryNoiseTiling;Secondary Noise Tiling;11;0;Create;True;0;0;0;False;0;False;8;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-1892.086,631.7811;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;140;-2239.64,120.5775;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1885.804,734.047;Float;False;Property;_PrimarySpeed;Primary Speed;12;0;Create;True;0;0;0;False;0;False;0.5;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;-1849.567,-54.93539;Inherit;False;Property;_PrimaryNoiseTiling;Primary Noise Tiling;10;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-1639.954,876.4976;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;66.23623,12.89378;Inherit;True;Property;_MetallicGlossMap;Metallic (R) AO (G) Height (B) Gloss (A);4;0;Create;False;0;0;0;False;0;False;-1;None;d08bb9a485a8e464c8ca00304befc821;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;-1631.287,655.3101;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1602.297,-11.58723;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;186;319.3386,740.6695;Inherit;False;Property;_Height;Height;16;0;Create;True;0;0;0;False;0;False;1;0.063;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-1605.142,262.2765;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;26;-1517.093,1094.881;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;136;-1410.569,183.1738;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;187;640.3298,724.1414;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;90;-1385.528,664.1245;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;161;-1220.393,608.0311;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;160;-1260.635,223.6041;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;188;813.5112,722.9744;Inherit;False;Height;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;181;451.1513,1372.554;Inherit;False;1050.367;762.4164;Comment;6;184;193;192;183;185;182;Displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.FractNode;162;-1090.883,622.9433;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FractNode;159;-1131.125,238.5163;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;192;501.1051,1978.792;Inherit;False;Property;_ZeroHeightOffset;Zero Height Offset;17;0;Create;True;0;0;0;False;0;False;0;0;-0.2;0.2;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;185;496.0362,1740.694;Inherit;False;188;Height;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;156;-1345.197,-309.6911;Inherit;True;Property;_Noise;Noise;9;0;Create;True;0;0;0;False;0;False;None;92af8dd565922d548ab972123eb023e2;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleSubtractOpNode;193;835.3017,1722.641;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;157;-932.5194,345.2434;Inherit;True;Property;_Noise_B;Noise_B;9;0;Create;True;0;0;0;False;0;False;-1;None;92af8dd565922d548ab972123eb023e2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;154;-921.3699,14.42983;Inherit;True;Property;_Noise_A;Noise_A;9;0;Create;True;0;0;0;False;0;False;-1;None;92af8dd565922d548ab972123eb023e2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;111;-900.8907,637.7394;Inherit;False;Property;_SecondaryAmplitude;Secondary Amplitude;15;0;Create;True;0;0;0;False;0;False;0;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;107;-778.231,-356.0507;Inherit;False;Property;_PrimaryAmplitude;Primary Amplitude;14;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;182;479.1469,1430.545;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;1013.706,1649.766;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;-500.1419,372.7982;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-486.2622,106.9066;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;173;793.9244,293.9795;Inherit;False;Property;_OcclusionStrength;Occlusion Strength;6;0;Create;False;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;113;-155.4874,317.03;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;174;1086.833,298.0251;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;184;1215.101,1668.097;Inherit;False;FinalHeight;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;189;783.7054,546.1057;Inherit;False;184;FinalHeight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;206;886.4741,1079.905;Inherit;False;Property;_TessDistanceMax;Tess Distance Max;20;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;408.7283,461.497;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;179;1276.068,236.8428;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;163;793.2899,194.9346;Inherit;False;Property;_GlossMapScale;Smoothness;5;0;Create;False;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;204;824.4741,851.905;Inherit;False;Property;_MaxTess;Max Tess;18;0;Create;True;0;0;0;False;0;False;16;16;1;32;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;165;-196.0023,-382.6207;Inherit;False;Property;_Color;Color;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;167;-173.8853,-117.8056;Inherit;False;Property;_NormalScale;Normal Scale;8;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;205;888.4741,978.905;Inherit;False;Property;_TessDistanceMin;Tess Distance Min;19;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-261.3856,-605.2586;Inherit;True;Property;_MainTex;Base Color;3;0;Create;False;0;0;0;False;0;False;-1;None;53253ae5a0ccc6d4b8f58c9c9e098f38;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;199;1696.81,342.9568;Inherit;False;Property;_Cull;Culling;0;1;[Enum];Create;False;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;2;0;False;0;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;190;1016.209,446.7078;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;6;62.27474,-196.7095;Inherit;True;Property;_BumpMap;Normal;7;0;Create;False;0;0;0;False;0;False;-1;None;17741de4d07a7a442b08b0d46b9cf119;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;169.6146,-604.2586;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;164;1272.216,127.3794;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;180;1397.068,234.8428;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;201;1191.08,-43.22061;Inherit;False;Property;_Cutoff;Alpha Clip Threshold;2;0;Create;False;0;0;0;False;0;False;0.333;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceBasedTessNode;203;1220.404,857.4391;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;202;1692.215,34.34924;Float;False;True;-1;6;;0;0;Standard;NOT_Lonely/NL_VertexAnim_tess;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;16;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;0;15;10;25;True;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;Standard;-1;-1;-1;0;0;False;0;0;True;199;-1;0;True;201;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;140;0;8;1
WireConnection;140;1;8;3
WireConnection;96;0;10;0
WireConnection;96;1;115;0
WireConnection;96;2;98;0
WireConnection;125;0;10;0
WireConnection;125;1;13;0
WireConnection;77;0;86;0
WireConnection;77;1;140;0
WireConnection;77;2;114;0
WireConnection;93;0;114;0
WireConnection;93;1;140;0
WireConnection;93;2;95;0
WireConnection;136;0;77;0
WireConnection;136;1;125;0
WireConnection;187;0;5;3
WireConnection;187;1;186;0
WireConnection;90;0;93;0
WireConnection;90;1;96;0
WireConnection;161;0;90;0
WireConnection;161;1;26;1
WireConnection;160;0;136;0
WireConnection;160;1;26;1
WireConnection;188;0;187;0
WireConnection;162;0;161;0
WireConnection;159;0;160;0
WireConnection;193;0;185;0
WireConnection;193;1;192;0
WireConnection;157;0;156;0
WireConnection;157;1;162;0
WireConnection;154;0;156;0
WireConnection;154;1;159;0
WireConnection;183;0;182;0
WireConnection;183;1;193;0
WireConnection;110;0;157;0
WireConnection;110;1;111;0
WireConnection;109;0;107;0
WireConnection;109;1;154;0
WireConnection;113;0;109;0
WireConnection;113;1;110;0
WireConnection;174;0;173;0
WireConnection;184;0;183;0
WireConnection;27;0;113;0
WireConnection;27;1;26;4
WireConnection;179;0;5;2
WireConnection;179;1;174;0
WireConnection;190;0;27;0
WireConnection;190;1;189;0
WireConnection;6;5;167;0
WireConnection;2;0;1;0
WireConnection;2;1;165;0
WireConnection;164;0;5;4
WireConnection;164;1;163;0
WireConnection;180;0;179;0
WireConnection;203;0;204;0
WireConnection;203;1;205;0
WireConnection;203;2;206;0
WireConnection;202;0;2;0
WireConnection;202;1;6;0
WireConnection;202;3;5;1
WireConnection;202;4;164;0
WireConnection;202;5;180;0
WireConnection;202;10;1;4
WireConnection;202;11;190;0
WireConnection;202;14;203;0
ASEEND*/
//CHKSM=ED5A0672BD25E90B47850A445CEDF19D4ADFE457