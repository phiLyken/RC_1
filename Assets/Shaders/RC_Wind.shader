// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-2393-OUT,alpha-798-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32440,y:32814,varname:node_2393,prsc:2|A-797-RGB,B-2053-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32180,y:32793,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32180,y:32951,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7764706,c2:0.3137255,c3:0.0627451,c4:1;n:type:ShaderForge.SFN_Multiply,id:798,x:32440,y:32944,varname:node_798,prsc:2|A-5189-OUT,B-2053-A,C-7507-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:7961,x:30878,y:32576,ptovrint:False,ptlb:node_7961,ptin:_node_7961,varname:node_7961,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7317,x:31444,y:32351,varname:node_7317,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-9600-UVOUT,TEX-7961-TEX;n:type:ShaderForge.SFN_Add,id:8336,x:31861,y:32476,varname:node_8336,prsc:2|A-8061-OUT,B-1461-OUT;n:type:ShaderForge.SFN_Panner,id:9029,x:31216,y:32310,varname:node_9029,prsc:2,spu:0,spv:-0.75|UVIN-3824-OUT;n:type:ShaderForge.SFN_TexCoord,id:904,x:30794,y:32265,varname:node_904,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:5894,x:31444,y:32639,varname:node_5894,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-904-UVOUT,TEX-7961-TEX;n:type:ShaderForge.SFN_Multiply,id:7681,x:32083,y:32560,varname:node_7681,prsc:2|A-8336-OUT,B-5894-B,C-3544-OUT,D-5580-OUT;n:type:ShaderForge.SFN_Panner,id:9600,x:31216,y:32163,varname:node_9600,prsc:2,spu:0.5,spv:-1|UVIN-904-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9702,x:31444,y:32487,varname:node_9702,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-9029-UVOUT,TEX-7961-TEX;n:type:ShaderForge.SFN_Multiply,id:1461,x:31668,y:32518,varname:node_1461,prsc:2|A-9702-G,B-1993-OUT;n:type:ShaderForge.SFN_Vector1,id:1993,x:31444,y:32783,varname:node_1993,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:8061,x:31664,y:32326,varname:node_8061,prsc:2|A-7317-R,B-8995-OUT;n:type:ShaderForge.SFN_Clamp01,id:5189,x:32250,y:32560,varname:node_5189,prsc:2|IN-7681-OUT;n:type:ShaderForge.SFN_Multiply,id:3824,x:31019,y:32410,varname:node_3824,prsc:2|A-904-UVOUT,B-9691-OUT;n:type:ShaderForge.SFN_Subtract,id:9069,x:32064,y:32691,varname:node_9069,prsc:2|A-8336-OUT,B-5121-OUT;n:type:ShaderForge.SFN_OneMinus,id:5121,x:31870,y:32823,varname:node_5121,prsc:2|IN-5894-B;n:type:ShaderForge.SFN_Vector1,id:9691,x:30794,y:32444,varname:node_9691,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Vector1,id:8995,x:31444,y:32851,varname:node_8995,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:595,x:31021,y:32041,varname:node_595,prsc:2|A-904-UVOUT,B-9691-OUT;n:type:ShaderForge.SFN_Panner,id:4672,x:31216,y:32011,varname:node_4672,prsc:2,spu:0.5,spv:-1|UVIN-595-OUT;n:type:ShaderForge.SFN_Tex2d,id:1104,x:31523,y:32095,varname:node_1104,prsc:2,tex:63257f0c5865f554e8163d33abd962d4,ntxv:0,isnm:False|UVIN-4672-UVOUT,TEX-7961-TEX;n:type:ShaderForge.SFN_Slider,id:5580,x:30799,y:32847,ptovrint:False,ptlb:Wind Strenght,ptin:_WindStrenght,varname:node_5580,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:3544,x:31797,y:32154,varname:node_3544,prsc:2|A-1104-R,B-8995-OUT;n:type:ShaderForge.SFN_DepthBlend,id:7507,x:32514,y:32556,varname:node_7507,prsc:2|DIST-5292-OUT;n:type:ShaderForge.SFN_Slider,id:5292,x:32511,y:32427,ptovrint:False,ptlb:DepthBlend Softness,ptin:_DepthBlendSoftness,varname:node_5292,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:7961-797-5580-5292;pass:END;sub:END;*/

Shader "Redcliffe/RC_Wind" {
    Properties {
        _node_7961 ("node_7961", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.7764706,0.3137255,0.0627451,1)
        _WindStrenght ("Wind Strenght", Range(0, 1)) = 1
        _DepthBlendSoftness ("DepthBlend Softness", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
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
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform sampler2D _node_7961; uniform float4 _node_7961_ST;
            uniform float _WindStrenght;
            uniform float _DepthBlendSoftness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float3 emissive = (_TintColor.rgb*i.vertexColor.rgb);
                float3 finalColor = emissive;
                float4 node_1925 = _Time + _TimeEditor;
                float2 node_9600 = (i.uv0+node_1925.g*float2(0.5,-1));
                float4 node_7317 = tex2D(_node_7961,TRANSFORM_TEX(node_9600, _node_7961));
                float node_8995 = 0.25;
                float node_9691 = 0.25;
                float2 node_9029 = ((i.uv0*node_9691)+node_1925.g*float2(0,-0.75));
                float4 node_9702 = tex2D(_node_7961,TRANSFORM_TEX(node_9029, _node_7961));
                float node_8336 = ((node_7317.r*node_8995)+(node_9702.g*0.5));
                float4 node_5894 = tex2D(_node_7961,TRANSFORM_TEX(i.uv0, _node_7961));
                float2 node_4672 = ((i.uv0*node_9691)+node_1925.g*float2(0.5,-1));
                float4 node_1104 = tex2D(_node_7961,TRANSFORM_TEX(node_4672, _node_7961));
                fixed4 finalRGBA = fixed4(finalColor,(saturate((node_8336*node_5894.b*(node_1104.r+node_8995)*_WindStrenght))*i.vertexColor.a*saturate((sceneZ-partZ)/_DepthBlendSoftness)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
