// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-3482-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32199,y:32893,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5,c3:0.5,c4:0;n:type:ShaderForge.SFN_Noise,id:239,x:32015,y:32526,varname:node_239,prsc:2|XY-5253-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1906,x:31671,y:32669,varname:node_1906,prsc:2,uv:0;n:type:ShaderForge.SFN_ScreenPos,id:5253,x:31790,y:32477,varname:node_5253,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:3482,x:32425,y:32767,varname:node_3482,prsc:2|A-3793-OUT,B-7241-RGB,C-1906-V;n:type:ShaderForge.SFN_RemapRange,id:3793,x:32204,y:32583,varname:node_3793,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:0.75|IN-239-OUT;proporder:7241;pass:END;sub:END;*/

Shader "Redcliffe/RC_background" {
    Properties {
        _Color ("Color", Color) = (0,0.5,0.5,0)
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
////// Lighting:
////// Emissive:
                float2 node_239_skew = i.screenPos.rg + 0.2127+i.screenPos.rg.x*0.3713*i.screenPos.rg.y;
                float2 node_239_rnd = 4.789*sin(489.123*(node_239_skew));
                float node_239 = frac(node_239_rnd.x*node_239_rnd.y*(1+node_239_skew.x));
                float3 emissive = ((node_239*0.25+0.5)*_Color.rgb*i.uv0.g);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
