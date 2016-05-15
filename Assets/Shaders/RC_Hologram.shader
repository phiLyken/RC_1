// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:6948,x:32719,y:32712,varname:node_6948,prsc:2|emission-4083-OUT;n:type:ShaderForge.SFN_Fresnel,id:6229,x:31916,y:32826,varname:node_6229,prsc:2|EXP-6856-OUT;n:type:ShaderForge.SFN_Multiply,id:8096,x:32117,y:32773,varname:node_8096,prsc:2|A-7336-RGB,B-6229-OUT;n:type:ShaderForge.SFN_Color,id:7336,x:31916,y:32666,ptovrint:False,ptlb:node_7336,ptin:_node_7336,varname:node_7336,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:6856,x:31588,y:32887,ptovrint:False,ptlb:Thickness,ptin:_Thickness,varname:node_6856,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.378157,max:4;n:type:ShaderForge.SFN_ScreenPos,id:4654,x:31725,y:33133,varname:node_4654,prsc:2,sctp:0;n:type:ShaderForge.SFN_Tex2dAsset,id:1952,x:31740,y:33321,ptovrint:False,ptlb:Scanlines,ptin:_Scanlines,varname:node_1952,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7a632f967e8ad42f5bd275898151ab6a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:562,x:31929,y:33157,varname:node_562,prsc:2,tex:7a632f967e8ad42f5bd275898151ab6a,ntxv:0,isnm:False|UVIN-4654-UVOUT,TEX-1952-TEX;n:type:ShaderForge.SFN_Multiply,id:5272,x:32283,y:32834,varname:node_5272,prsc:2|A-8096-OUT,B-4890-OUT;n:type:ShaderForge.SFN_Add,id:4890,x:32135,y:33087,varname:node_4890,prsc:2|A-7873-RGB,B-562-RGB;n:type:ShaderForge.SFN_Tex2d,id:7873,x:31916,y:33004,ptovrint:False,ptlb:node_7873,ptin:_node_7873,varname:node_7873,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:529239097d02f9f42b0ddd436c6fcbb0,ntxv:0,isnm:False|UVIN-9856-UVOUT;n:type:ShaderForge.SFN_Panner,id:9856,x:31666,y:32972,varname:node_9856,prsc:2,spu:0,spv:2|UVIN-4667-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4667,x:31396,y:32909,varname:node_4667,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:4083,x:32498,y:32882,varname:node_4083,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-5272-OUT;proporder:7336-6856-1952-7873;pass:END;sub:END;*/

Shader "Redcliffe/RC_Hologram" {
    Properties {
        _node_7336 ("node_7336", Color) = (0,1,1,1)
        _Thickness ("Thickness", Range(0, 4)) = 3.378157
        _Scanlines ("Scanlines", 2D) = "white" {}
        _node_7873 ("node_7873", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
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
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _node_7336;
            uniform float _Thickness;
            uniform sampler2D _Scanlines; uniform float4 _Scanlines_ST;
            uniform sampler2D _node_7873; uniform float4 _node_7873_ST;
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
                float4 screenPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_5710 = _Time + _TimeEditor;
                float2 node_9856 = (i.uv0+node_5710.g*float2(0,2));
                float4 _node_7873_var = tex2D(_node_7873,TRANSFORM_TEX(node_9856, _node_7873));
                float4 node_562 = tex2D(_Scanlines,TRANSFORM_TEX(i.screenPos.rg, _Scanlines));
                float3 node_4890 = (_node_7873_var.rgb+node_562.rgb);
                float3 node_5272 = ((_node_7336.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Thickness))*node_4890);
                float3 emissive = node_5272.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
