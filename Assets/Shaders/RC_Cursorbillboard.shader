// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-2393-OUT,alpha-798-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f9c6d6b282f77f040bf1611fbe762f13,ntxv:0,isnm:False|UVIN-6998-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:798,x:32495,y:32923,varname:node_798,prsc:2|A-6074-R,B-2053-A,C-797-A;n:type:ShaderForge.SFN_UVTile,id:6998,x:32078,y:32601,varname:node_6998,prsc:2|WDT-799-OUT,HGT-3758-OUT,TILE-9901-OUT;n:type:ShaderForge.SFN_Time,id:1107,x:31188,y:33105,varname:node_1107,prsc:2;n:type:ShaderForge.SFN_Frac,id:2898,x:31541,y:33042,cmnt:use decimal to create saw form,varname:node_2898,prsc:2|IN-5233-OUT;n:type:ShaderForge.SFN_Multiply,id:2213,x:31541,y:32815,cmnt:Total number of posions,varname:node_2213,prsc:2|A-799-OUT,B-3758-OUT;n:type:ShaderForge.SFN_Round,id:9901,x:31917,y:32889,cmnt:Current Index,varname:node_9901,prsc:2|IN-6596-OUT;n:type:ShaderForge.SFN_Multiply,id:6596,x:31755,y:32889,cmnt:Current Frame,varname:node_6596,prsc:2|A-2213-OUT,B-2898-OUT;n:type:ShaderForge.SFN_Multiply,id:5233,x:31365,y:33042,varname:node_5233,prsc:2|A-8288-OUT,B-1107-T;n:type:ShaderForge.SFN_ValueProperty,id:799,x:31184,y:32630,ptovrint:False,ptlb:columns,ptin:_columns,varname:node_799,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:3758,x:31184,y:32715,ptovrint:False,ptlb:rows,ptin:_rows,varname:node_3758,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:8288,x:31188,y:33029,ptovrint:False,ptlb:Animation Speed,ptin:_AnimationSpeed,varname:node_8288,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:6074-797-799-3758-8288;pass:END;sub:END;*/

Shader "Redcliffe/RC_Cursorbillboard" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (1,0,0,1)
        _columns ("columns", Float ) = 2
        _rows ("rows", Float ) = 2
        _AnimationSpeed ("Animation Speed", Float ) = 1
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _columns;
            uniform float _rows;
            uniform float _AnimationSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_1107 = _Time + _TimeEditor;
                float node_9901 = round(((_columns*_rows)*frac((_AnimationSpeed*node_1107.g)))); // Current Index
                float2 node_6998_tc_rcp = float2(1.0,1.0)/float2( _columns, _rows );
                float node_6998_ty = floor(node_9901 * node_6998_tc_rcp.x);
                float node_6998_tx = node_9901 - _columns * node_6998_ty;
                float2 node_6998 = (i.uv0 + float2(node_6998_tx, node_6998_ty)) * node_6998_tc_rcp;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_6998, _MainTex));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.r*i.vertexColor.a*_TintColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
