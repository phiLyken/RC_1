// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-4035-OUT,B-2053-RGB,C-797-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_TexCoord,id:9520,x:31112,y:32404,varname:node_9520,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:5030,x:31794,y:32637,varname:node_5030,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9520-V;n:type:ShaderForge.SFN_Multiply,id:5374,x:31869,y:32430,varname:node_5374,prsc:2|A-8810-OUT,B-290-OUT;n:type:ShaderForge.SFN_Vector1,id:8810,x:31641,y:32279,varname:node_8810,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:4035,x:32422,y:32607,varname:node_4035,prsc:2|A-5030-OUT,B-7677-OUT,C-6933-OUT;n:type:ShaderForge.SFN_Add,id:1836,x:32037,y:32586,varname:node_1836,prsc:2|A-5374-OUT,B-5030-OUT;n:type:ShaderForge.SFN_Slider,id:6933,x:32037,y:32443,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_6933,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Clamp01,id:7677,x:32214,y:32586,varname:node_7677,prsc:2|IN-1836-OUT;n:type:ShaderForge.SFN_Panner,id:4379,x:32356,y:32141,varname:node_4379,prsc:2,spu:0.1,spv:0.1|UVIN-4129-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:3956,x:31925,y:32141,varname:node_3956,prsc:2;n:type:ShaderForge.SFN_ChannelBlend,id:290,x:32977,y:32033,varname:node_290,prsc:2,chbt:0|M-7021-OUT,R-5780-R,G-7053-R,B-2937-R;n:type:ShaderForge.SFN_Abs,id:8383,x:32178,y:31908,varname:node_8383,prsc:2|IN-2443-OUT;n:type:ShaderForge.SFN_NormalVector,id:2443,x:31925,y:31908,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:7021,x:32378,y:31908,varname:node_7021,prsc:2|A-8383-OUT,B-8383-OUT;n:type:ShaderForge.SFN_Append,id:7665,x:32178,y:32035,varname:node_7665,prsc:2|A-3956-X,B-3956-Y;n:type:ShaderForge.SFN_Append,id:4129,x:32178,y:32161,varname:node_4129,prsc:2|A-3956-Z,B-3956-X;n:type:ShaderForge.SFN_Append,id:129,x:32167,y:32285,varname:node_129,prsc:2|A-3956-Y,B-3956-Z;n:type:ShaderForge.SFN_Tex2dAsset,id:7043,x:31554,y:32031,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_7043,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2937,x:32542,y:32013,varname:node_2937,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-3504-UVOUT,TEX-7043-TEX;n:type:ShaderForge.SFN_Tex2d,id:7053,x:32542,y:32183,varname:node_7053,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-4379-UVOUT,TEX-7043-TEX;n:type:ShaderForge.SFN_Tex2d,id:5780,x:32542,y:32356,varname:node_5780,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-9596-UVOUT,TEX-7043-TEX;n:type:ShaderForge.SFN_Panner,id:9596,x:32356,y:32303,varname:node_9596,prsc:2,spu:0.1,spv:0.1|UVIN-129-OUT;n:type:ShaderForge.SFN_Panner,id:3504,x:32356,y:32025,varname:node_3504,prsc:2,spu:0.1,spv:0.1|UVIN-7665-OUT;proporder:797-6933-7043;pass:END;sub:END;*/

Shader "Redcliffe/RC_tileside" {
    Properties {
        _TintColor ("Color", Color) = (0,0.5,0.5,1)
        _Brightness ("Brightness", Range(0, 1)) = 1
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform float _Brightness;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_5030 = i.uv0.g.r;
                float3 node_8383 = abs(i.normalDir);
                float3 node_7021 = (node_8383*node_8383);
                float4 node_5918 = _Time + _TimeEditor;
                float2 node_9596 = (float2(i.posWorld.g,i.posWorld.b)+node_5918.g*float2(0.1,0.1));
                float4 node_5780 = tex2D(_MainTex,TRANSFORM_TEX(node_9596, _MainTex));
                float2 node_4379 = (float2(i.posWorld.b,i.posWorld.r)+node_5918.g*float2(0.1,0.1));
                float4 node_7053 = tex2D(_MainTex,TRANSFORM_TEX(node_4379, _MainTex));
                float2 node_3504 = (float2(i.posWorld.r,i.posWorld.g)+node_5918.g*float2(0.1,0.1));
                float4 node_2937 = tex2D(_MainTex,TRANSFORM_TEX(node_3504, _MainTex));
                float3 emissive = ((node_5030*saturate(((0.25*(node_7021.r*node_5780.r + node_7021.g*node_7053.r + node_7021.b*node_2937.r))+node_5030))*_Brightness)*i.vertexColor.rgb*_TintColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
