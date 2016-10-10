// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-9677-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_ScreenPos,id:840,x:31660,y:32646,varname:node_840,prsc:2,sctp:0;n:type:ShaderForge.SFN_Tex2dAsset,id:9246,x:31660,y:32836,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_9246,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9ae351f27b5ce554fb54ba8471821385,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:7454,x:31618,y:33049,ptovrint:False,ptlb:scanline,ptin:_scanline,varname:node_7454,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5a9d2d4253cd67d4ba4fdc9d30810590,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9508,x:31978,y:32844,varname:node_9508,prsc:2,tex:9ae351f27b5ce554fb54ba8471821385,ntxv:0,isnm:False|UVIN-840-UVOUT,TEX-9246-TEX;n:type:ShaderForge.SFN_Add,id:9677,x:32294,y:32588,varname:node_9677,prsc:2|A-9508-RGB,B-6880-RGB,C-4055-OUT;n:type:ShaderForge.SFN_Tex2d,id:6880,x:32035,y:33161,varname:node_6880,prsc:2,tex:5a9d2d4253cd67d4ba4fdc9d30810590,ntxv:0,isnm:False|UVIN-6526-UVOUT,MIP-8818-Y,TEX-7454-TEX;n:type:ShaderForge.SFN_FragmentPosition,id:8818,x:31478,y:33249,varname:node_8818,prsc:2;n:type:ShaderForge.SFN_Append,id:6343,x:31718,y:33218,varname:node_6343,prsc:2|A-8818-Z,B-8818-Y;n:type:ShaderForge.SFN_Panner,id:6526,x:31891,y:33253,varname:node_6526,prsc:2,spu:0,spv:-1|UVIN-6343-OUT,DIST-2367-OUT;n:type:ShaderForge.SFN_Slider,id:5632,x:31292,y:33559,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_5632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4,max:2;n:type:ShaderForge.SFN_Time,id:9377,x:31449,y:33390,varname:node_9377,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2367,x:31703,y:33432,varname:node_2367,prsc:2|A-9377-T,B-5632-OUT;n:type:ShaderForge.SFN_Fresnel,id:4055,x:32045,y:32531,varname:node_4055,prsc:2|EXP-6937-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6937,x:31827,y:32585,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_6937,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;proporder:797-9246-7454-5632-6937;pass:END;sub:END;*/

Shader "Redcliffe/RC_blip" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _noise ("noise", 2D) = "white" {}
        _scanline ("scanline", 2D) = "white" {}
        _speed ("speed", Range(0, 2)) = 0.4
        _Fresnel ("Fresnel", Float ) = 3
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
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform sampler2D _scanline; uniform float4 _scanline_ST;
            uniform float _speed;
            uniform float _Fresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
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
                float4 node_9508 = tex2D(_noise,TRANSFORM_TEX(i.screenPos.rg, _noise));
                float4 node_9377 = _Time + _TimeEditor;
                float2 node_6526 = (float2(i.posWorld.b,i.posWorld.g)+(node_9377.g*_speed)*float2(0,-1));
                float4 node_6880 = tex2Dlod(_scanline,float4(TRANSFORM_TEX(node_6526, _scanline),0.0,i.posWorld.g));
                float3 emissive = ((node_9508.rgb+node_6880.rgb+pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel))*i.vertexColor.rgb*_TintColor.rgb*2.0);
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
