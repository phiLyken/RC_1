// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-779-OUT,emission-4796-OUT;n:type:ShaderForge.SFN_Slider,id:2263,x:31549,y:32945,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_1189,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:2844,x:31841,y:32837,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_7709,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3644,x:31841,y:32649,ptovrint:False,ptlb:mask,ptin:_mask,varname:node_8199,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:669a8e33671439944b4558b26e4c7153,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5173,x:31841,y:32435,ptovrint:False,ptlb:Main_Tex,ptin:_Main_Tex,varname:node_680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:59af91f589e3d9045a25a29d517813bb,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Color,id:6052,x:31841,y:33023,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9779412,c2:0.6759299,c3:0.6759299,c4:1;n:type:ShaderForge.SFN_OneMinus,id:3805,x:32123,y:32524,varname:node_3805,prsc:2|IN-3644-R;n:type:ShaderForge.SFN_Blend,id:817,x:32190,y:32747,varname:node_817,prsc:2,blmd:1,clmp:True|SRC-5173-RGB,DST-6065-RGB;n:type:ShaderForge.SFN_Multiply,id:4796,x:32429,y:32873,varname:node_4796,prsc:2|A-2844-RGB,B-2263-OUT;n:type:ShaderForge.SFN_Multiply,id:165,x:32438,y:32613,varname:node_165,prsc:2|A-817-OUT,B-3644-R;n:type:ShaderForge.SFN_Multiply,id:1827,x:32438,y:32470,varname:node_1827,prsc:2|A-5173-RGB,B-3805-OUT;n:type:ShaderForge.SFN_Add,id:779,x:32686,y:32517,varname:node_779,prsc:2|A-1827-OUT,B-165-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9118,x:30441,y:32631,ptovrint:False,ptlb:columns,ptin:_columns,varname:node_799,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:3835,x:30441,y:32716,ptovrint:False,ptlb:rows,ptin:_rows,varname:node_3758,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:383,x:30445,y:33030,ptovrint:False,ptlb:Animation Speed,ptin:_AnimationSpeed,varname:node_8288,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Time,id:3713,x:30445,y:33106,varname:node_3713,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8451,x:30622,y:33043,varname:node_8451,prsc:2|A-383-OUT,B-3713-T;n:type:ShaderForge.SFN_Multiply,id:9015,x:30798,y:32816,cmnt:Total number of posions,varname:node_9015,prsc:2|A-9118-OUT,B-3835-OUT;n:type:ShaderForge.SFN_Frac,id:9301,x:30798,y:33043,cmnt:use decimal to create saw form,varname:node_9301,prsc:2|IN-8451-OUT;n:type:ShaderForge.SFN_Multiply,id:2909,x:31012,y:32890,cmnt:Current Frame,varname:node_2909,prsc:2|A-9015-OUT,B-9301-OUT;n:type:ShaderForge.SFN_Round,id:1525,x:31179,y:32890,cmnt:Current Index,varname:node_1525,prsc:2|IN-2909-OUT;n:type:ShaderForge.SFN_UVTile,id:1316,x:31335,y:32602,varname:node_1316,prsc:2|WDT-9118-OUT,HGT-3835-OUT,TILE-9695-OUT;n:type:ShaderForge.SFN_Tex2d,id:6065,x:31492,y:32602,ptovrint:False,ptlb:SkinColors,ptin:_SkinColors,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:853eeceb5a66b5b4783875b7eeb122ab,ntxv:0,isnm:False|UVIN-1316-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:9695,x:31081,y:33055,ptovrint:False,ptlb:skincolor,ptin:_skincolor,varname:node_9695,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:3644-5173-6052-2263-2844-9118-3835-383-6065-9695;pass:END;sub:END;*/

Shader "Redcliffe/RC_Unitskin_flipbook" {
    Properties {
        _mask ("mask", 2D) = "white" {}
        _Main_Tex ("Main_Tex", 2D) = "black" {}
        _Color_copy ("Color_copy", Color) = (0.9779412,0.6759299,0.6759299,1)
        _Brightness ("Brightness", Range(0, 1)) = 0
        _Emission ("Emission", 2D) = "white" {}
        _columns ("columns", Float ) = 2
        _rows ("rows", Float ) = 2
        _AnimationSpeed ("Animation Speed", Float ) = 0
        _SkinColors ("SkinColors", 2D) = "white" {}
        _skincolor ("skincolor", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Brightness;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform sampler2D _mask; uniform float4 _mask_ST;
            uniform sampler2D _Main_Tex; uniform float4 _Main_Tex_ST;
            uniform float _columns;
            uniform float _rows;
            uniform sampler2D _SkinColors; uniform float4 _SkinColors_ST;
            uniform float _skincolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Main_Tex_var = tex2D(_Main_Tex,TRANSFORM_TEX(i.uv0, _Main_Tex));
                float4 _mask_var = tex2D(_mask,TRANSFORM_TEX(i.uv0, _mask));
                float2 node_1316_tc_rcp = float2(1.0,1.0)/float2( _columns, _rows );
                float node_1316_ty = floor(_skincolor * node_1316_tc_rcp.x);
                float node_1316_tx = _skincolor - _columns * node_1316_ty;
                float2 node_1316 = (i.uv0 + float2(node_1316_tx, node_1316_ty)) * node_1316_tc_rcp;
                float4 _SkinColors_var = tex2D(_SkinColors,TRANSFORM_TEX(node_1316, _SkinColors));
                float3 diffuseColor = ((_Main_Tex_var.rgb*(1.0 - _mask_var.r))+(saturate((_Main_Tex_var.rgb*_SkinColors_var.rgb))*_mask_var.r));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float3 emissive = (_Emission_var.rgb*_Brightness);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Brightness;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform sampler2D _mask; uniform float4 _mask_ST;
            uniform sampler2D _Main_Tex; uniform float4 _Main_Tex_ST;
            uniform float _columns;
            uniform float _rows;
            uniform sampler2D _SkinColors; uniform float4 _SkinColors_ST;
            uniform float _skincolor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Main_Tex_var = tex2D(_Main_Tex,TRANSFORM_TEX(i.uv0, _Main_Tex));
                float4 _mask_var = tex2D(_mask,TRANSFORM_TEX(i.uv0, _mask));
                float2 node_1316_tc_rcp = float2(1.0,1.0)/float2( _columns, _rows );
                float node_1316_ty = floor(_skincolor * node_1316_tc_rcp.x);
                float node_1316_tx = _skincolor - _columns * node_1316_ty;
                float2 node_1316 = (i.uv0 + float2(node_1316_tx, node_1316_ty)) * node_1316_tc_rcp;
                float4 _SkinColors_var = tex2D(_SkinColors,TRANSFORM_TEX(node_1316, _SkinColors));
                float3 diffuseColor = ((_Main_Tex_var.rgb*(1.0 - _mask_var.r))+(saturate((_Main_Tex_var.rgb*_SkinColors_var.rgb))*_mask_var.r));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
