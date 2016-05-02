// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1291,x:32719,y:32712,varname:node_1291,prsc:2|diff-848-RGB,normal-6896-RGB,emission-3877-RGB,clip-4192-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:37,x:31265,y:32276,varname:node_37,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:848,x:31692,y:32276,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_848,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:156b3be5d1d342e49b47174800352b1e,ntxv:0,isnm:False|UVIN-7786-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7786,x:31456,y:32276,varname:node_7786,prsc:2,cc1:0,cc2:2,cc3:-1,cc4:-1|IN-37-XYZ;n:type:ShaderForge.SFN_Tex2d,id:8485,x:30944,y:33318,ptovrint:False,ptlb:Dissolve Pattern,ptin:_DissolvePattern,varname:node_8485,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3b5cb736a5c870a46b3f9e629e2e0e09,ntxv:0,isnm:False|UVIN-7786-OUT;n:type:ShaderForge.SFN_Tex2d,id:6896,x:31692,y:32470,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74d03a97f9b9a1b4d8210e81c0fddd90,ntxv:3,isnm:True|UVIN-7786-OUT;n:type:ShaderForge.SFN_Add,id:4192,x:31554,y:33180,varname:node_4192,prsc:2|A-9669-OUT,B-2768-OUT;n:type:ShaderForge.SFN_Clamp01,id:1439,x:31956,y:33042,varname:node_1439,prsc:2|IN-8992-OUT;n:type:ShaderForge.SFN_RemapRange,id:7191,x:31554,y:32926,varname:node_7191,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4527-OUT;n:type:ShaderForge.SFN_OneMinus,id:4527,x:31391,y:32926,varname:node_4527,prsc:2|IN-10-OUT;n:type:ShaderForge.SFN_Append,id:4892,x:32323,y:33054,varname:node_4892,prsc:2|A-1069-OUT,B-5543-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:558,x:32193,y:33351,ptovrint:False,ptlb:Dissolve Ramp,ptin:_DissolveRamp,varname:node_558,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:02077c4992aa0344195b669796beb23f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3877,x:32500,y:33054,varname:node_3877,prsc:2,tex:02077c4992aa0344195b669796beb23f,ntxv:0,isnm:False|UVIN-4892-OUT,TEX-558-TEX;n:type:ShaderForge.SFN_Vector1,id:5543,x:32169,y:33209,varname:node_5543,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:1069,x:32148,y:33054,varname:node_1069,prsc:2|IN-1439-OUT;n:type:ShaderForge.SFN_Multiply,id:2768,x:31380,y:33315,varname:node_2768,prsc:2|A-9669-OUT,B-5625-OUT;n:type:ShaderForge.SFN_Slider,id:7840,x:30839,y:33125,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_7840,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:8992,x:31771,y:33042,varname:node_8992,prsc:2|A-7191-OUT,B-4192-OUT;n:type:ShaderForge.SFN_Vector1,id:10,x:31215,y:32926,varname:node_10,prsc:2,v1:1;n:type:ShaderForge.SFN_OneMinus,id:9669,x:31215,y:33128,varname:node_9669,prsc:2|IN-7840-OUT;n:type:ShaderForge.SFN_RemapRange,id:5625,x:31162,y:33335,varname:node_5625,prsc:2,frmn:0,frmx:1,tomn:0.1,tomx:1|IN-8485-R;proporder:848-6896-8485-558-7840;pass:END;sub:END;*/

Shader "Custom/RC_ground" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _DissolvePattern ("Dissolve Pattern", 2D) = "white" {}
        _DissolveRamp ("Dissolve Ramp", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 200
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DissolvePattern; uniform float4 _DissolvePattern_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float _Dissolve;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_7786 = i.posWorld.rgb.rb;
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_7786, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_9669 = (1.0 - _Dissolve);
                float4 _DissolvePattern_var = tex2D(_DissolvePattern,TRANSFORM_TEX(node_7786, _DissolvePattern));
                float node_4192 = (node_9669+(node_9669*(_DissolvePattern_var.r*0.9+0.1)));
                clip(node_4192 - 0.5);
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7786, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_4892 = float2((1.0 - saturate((((1.0 - 1.0)*1.0+-0.5)+node_4192))),0.0);
                float4 node_3877 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_4892, _DissolveRamp));
                float3 emissive = node_3877.rgb;
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _DissolvePattern; uniform float4 _DissolvePattern_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float _Dissolve;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_7786 = i.posWorld.rgb.rb;
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_7786, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_9669 = (1.0 - _Dissolve);
                float4 _DissolvePattern_var = tex2D(_DissolvePattern,TRANSFORM_TEX(node_7786, _DissolvePattern));
                float node_4192 = (node_9669+(node_9669*(_DissolvePattern_var.r*0.9+0.1)));
                clip(node_4192 - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7786, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _DissolvePattern; uniform float4 _DissolvePattern_ST;
            uniform float _Dissolve;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_9669 = (1.0 - _Dissolve);
                float2 node_7786 = i.posWorld.rgb.rb;
                float4 _DissolvePattern_var = tex2D(_DissolvePattern,TRANSFORM_TEX(node_7786, _DissolvePattern));
                float node_4192 = (node_9669+(node_9669*(_DissolvePattern_var.r*0.9+0.1)));
                clip(node_4192 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
