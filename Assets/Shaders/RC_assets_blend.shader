// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1291,x:32719,y:32712,varname:node_1291,prsc:2|diff-35-OUT,normal-6896-RGB,emission-165-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:37,x:29488,y:32345,varname:node_37,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:848,x:30480,y:31891,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_848,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:156b3be5d1d342e49b47174800352b1e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6896,x:30480,y:32085,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74d03a97f9b9a1b4d8210e81c0fddd90,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Clamp01,id:1439,x:31758,y:32571,varname:node_1439,prsc:2|IN-2606-OUT;n:type:ShaderForge.SFN_RemapRange,id:7191,x:31343,y:32450,varname:node_7191,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4527-OUT;n:type:ShaderForge.SFN_OneMinus,id:4527,x:31187,y:32450,varname:node_4527,prsc:2|IN-799-OUT;n:type:ShaderForge.SFN_Append,id:4892,x:32203,y:32571,varname:node_4892,prsc:2|A-1069-OUT,B-5543-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:558,x:32044,y:32409,ptovrint:False,ptlb:Dissolve Ramp,ptin:_DissolveRamp,varname:node_558,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:02077c4992aa0344195b669796beb23f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3877,x:32387,y:32571,varname:node_3877,prsc:2,tex:02077c4992aa0344195b669796beb23f,ntxv:0,isnm:False|UVIN-4892-OUT,TEX-558-TEX;n:type:ShaderForge.SFN_Vector1,id:5543,x:32044,y:32706,varname:node_5543,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:1069,x:31930,y:32571,varname:node_1069,prsc:2|IN-1439-OUT;n:type:ShaderForge.SFN_Tex2d,id:8602,x:30467,y:32604,varname:node_8602,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-7348-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_Add,id:9618,x:29722,y:32410,varname:node_9618,prsc:2|A-37-X,B-336-OUT;n:type:ShaderForge.SFN_Vector4Property,id:7889,x:29192,y:32538,ptovrint:False,ptlb:Vector,ptin:_Vector,varname:node_7889,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5,v2:0.5,v3:0.5,v4:1;n:type:ShaderForge.SFN_Negate,id:336,x:29488,y:32482,varname:node_336,prsc:2|IN-7889-X;n:type:ShaderForge.SFN_Append,id:4202,x:29982,y:32413,varname:node_4202,prsc:2|A-9618-OUT,B-4093-OUT;n:type:ShaderForge.SFN_Negate,id:9015,x:29485,y:32772,varname:node_9015,prsc:2|IN-7889-Z;n:type:ShaderForge.SFN_Add,id:4093,x:29722,y:32554,varname:node_4093,prsc:2|A-37-Z,B-9015-OUT;n:type:ShaderForge.SFN_Panner,id:7348,x:30198,y:32424,varname:node_7348,prsc:2,spu:0,spv:1|UVIN-4202-OUT,DIST-5857-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5857,x:29981,y:32554,ptovrint:False,ptlb:Z offset,ptin:_Zoffset,varname:node_5857,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Negate,id:6909,x:29485,y:32626,varname:node_6909,prsc:2|IN-7889-Y;n:type:ShaderForge.SFN_Slider,id:8177,x:29837,y:33147,ptovrint:False,ptlb:z,ptin:_z,varname:node_8177,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1.012502,max:2;n:type:ShaderForge.SFN_Append,id:8112,x:29981,y:32664,varname:node_8112,prsc:2|A-4093-OUT,B-37-X;n:type:ShaderForge.SFN_Panner,id:3377,x:30198,y:32664,varname:node_3377,prsc:2,spu:0,spv:1|UVIN-8112-OUT,DIST-4972-OUT;n:type:ShaderForge.SFN_Slider,id:3478,x:29837,y:33243,ptovrint:False,ptlb:x,ptin:_x,varname:node_3478,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.1221517,max:2;n:type:ShaderForge.SFN_Tex2dAsset,id:9443,x:30199,y:33085,ptovrint:False,ptlb:X Clamp,ptin:_XClamp,varname:node_9443,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9449,x:31122,y:32718,varname:node_9449,prsc:2|A-8602-R,B-671-OUT;n:type:ShaderForge.SFN_Tex2d,id:3617,x:30467,y:32794,varname:node_3617,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-3377-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_ValueProperty,id:4972,x:29981,y:32813,ptovrint:False,ptlb:-X offset,ptin:_Xoffset,varname:node_4972,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Panner,id:3991,x:30199,y:32899,varname:node_3991,prsc:2,spu:0,spv:1|UVIN-7174-OUT,DIST-5852-OUT;n:type:ShaderForge.SFN_Tex2d,id:3453,x:30478,y:32975,varname:node_3453,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-3991-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_Negate,id:8795,x:30651,y:32975,varname:node_8795,prsc:2|IN-3453-R;n:type:ShaderForge.SFN_Append,id:7174,x:29981,y:32909,varname:node_7174,prsc:2|A-4093-OUT,B-37-X;n:type:ShaderForge.SFN_ValueProperty,id:5852,x:29981,y:33052,ptovrint:False,ptlb:x offset,ptin:_xoffset,varname:node_5852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Slider,id:8475,x:29837,y:33337,ptovrint:False,ptlb:x neg,ptin:_xneg,varname:node_8475,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.8720234,max:2;n:type:ShaderForge.SFN_Add,id:671,x:30811,y:32868,varname:node_671,prsc:2|A-3617-R,B-8795-OUT;n:type:ShaderForge.SFN_ValueProperty,id:799,x:30972,y:32409,ptovrint:False,ptlb:Dissolve on,ptin:_Dissolveon,varname:node_799,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:2606,x:31556,y:32571,varname:node_2606,prsc:2|A-7191-OUT,B-8602-R;n:type:ShaderForge.SFN_Multiply,id:7371,x:32597,y:32427,varname:node_7371,prsc:2|A-1183-OUT,B-3877-RGB;n:type:ShaderForge.SFN_Slider,id:1183,x:32223,y:32368,ptovrint:False,ptlb:Bloom Intesity,ptin:_BloomIntesity,varname:node_1183,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:2;n:type:ShaderForge.SFN_Lerp,id:35,x:31950,y:32159,varname:node_35,prsc:2|A-1983-RGB,B-848-RGB,T-9449-OUT;n:type:ShaderForge.SFN_Color,id:1983,x:31618,y:31959,ptovrint:False,ptlb:Holo Bacgground color,ptin:_HoloBacggroundcolor,varname:node_1983,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:0;n:type:ShaderForge.SFN_Fresnel,id:4450,x:31927,y:33031,varname:node_4450,prsc:2;n:type:ShaderForge.SFN_Color,id:8444,x:31927,y:33202,ptovrint:False,ptlb:HologramColor,ptin:_HologramColor,varname:node_8444,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5093,x:32209,y:32997,varname:node_5093,prsc:2|A-4450-OUT,B-8444-RGB,C-3124-OUT;n:type:ShaderForge.SFN_Multiply,id:165,x:32398,y:32860,varname:node_165,prsc:2|A-3990-OUT,B-5093-OUT;n:type:ShaderForge.SFN_OneMinus,id:3990,x:31662,y:32814,varname:node_3990,prsc:2|IN-9449-OUT;n:type:ShaderForge.SFN_ScreenPos,id:6694,x:31989,y:33354,varname:node_6694,prsc:2,sctp:0;n:type:ShaderForge.SFN_Noise,id:614,x:32169,y:33342,varname:node_614,prsc:2|XY-6694-UVOUT;n:type:ShaderForge.SFN_RemapRange,id:3124,x:32349,y:33342,varname:node_3124,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:0.75|IN-614-OUT;proporder:848-6896-558-7889-5857-8177-3478-9443-8475-799-4972-5852-1183-1983-8444;pass:END;sub:END;*/

Shader "Redcliffe/RC_assests_blend" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _DissolveRamp ("Dissolve Ramp", 2D) = "white" {}
        [HideInInspector]_Vector ("Vector", Vector) = (0.5,0.5,0.5,1)
        _Zoffset ("Z offset", Float ) = 0
        [HideInInspector]_z ("z", Range(-1, 2)) = 1.012502
        [HideInInspector]_x ("x", Range(-1, 2)) = 0.1221517
        _XClamp ("X Clamp", 2D) = "white" {}
        [HideInInspector]_xneg ("x neg", Range(-1, 2)) = 0.8720234
        [HideInInspector]_Dissolveon ("Dissolve on", Float ) = 1
        _Xoffset ("-X offset", Float ) = 0
        _xoffset ("x offset", Float ) = 0
        _BloomIntesity ("Bloom Intesity", Range(1, 2)) = 1
        _HoloBacggroundcolor ("Holo Bacgground color", Color) = (0,0,0,0)
        _HologramColor ("HologramColor", Color) = (0,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 _Vector;
            uniform float _Zoffset;
            uniform sampler2D _XClamp; uniform float4 _XClamp_ST;
            uniform float _Xoffset;
            uniform float _xoffset;
            uniform float4 _HoloBacggroundcolor;
            uniform float4 _HologramColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_4093 = (i.posWorld.b+(-1*_Vector.b));
                float2 node_7348 = (float2((i.posWorld.r+(-1*_Vector.r)),node_4093)+_Zoffset*float2(0,1));
                float4 node_8602 = tex2D(_XClamp,TRANSFORM_TEX(node_7348, _XClamp));
                float2 node_3377 = (float2(node_4093,i.posWorld.r)+_Xoffset*float2(0,1));
                float4 node_3617 = tex2D(_XClamp,TRANSFORM_TEX(node_3377, _XClamp));
                float2 node_3991 = (float2(node_4093,i.posWorld.r)+_xoffset*float2(0,1));
                float4 node_3453 = tex2D(_XClamp,TRANSFORM_TEX(node_3991, _XClamp));
                float node_9449 = (node_8602.r*(node_3617.r+(-1*node_3453.r)));
                float3 diffuseColor = lerp(_HoloBacggroundcolor.rgb,_MainTex_var.rgb,node_9449);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_614_skew = i.screenPos.rg + 0.2127+i.screenPos.rg.x*0.3713*i.screenPos.rg.y;
                float2 node_614_rnd = 4.789*sin(489.123*(node_614_skew));
                float node_614 = frac(node_614_rnd.x*node_614_rnd.y*(1+node_614_skew.x));
                float3 emissive = ((1.0 - node_9449)*((1.0-max(0,dot(normalDirection, viewDirection)))*_HologramColor.rgb*(node_614*0.25+0.5)));
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
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 _Vector;
            uniform float _Zoffset;
            uniform sampler2D _XClamp; uniform float4 _XClamp_ST;
            uniform float _Xoffset;
            uniform float _xoffset;
            uniform float4 _HoloBacggroundcolor;
            uniform float4 _HologramColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_4093 = (i.posWorld.b+(-1*_Vector.b));
                float2 node_7348 = (float2((i.posWorld.r+(-1*_Vector.r)),node_4093)+_Zoffset*float2(0,1));
                float4 node_8602 = tex2D(_XClamp,TRANSFORM_TEX(node_7348, _XClamp));
                float2 node_3377 = (float2(node_4093,i.posWorld.r)+_Xoffset*float2(0,1));
                float4 node_3617 = tex2D(_XClamp,TRANSFORM_TEX(node_3377, _XClamp));
                float2 node_3991 = (float2(node_4093,i.posWorld.r)+_xoffset*float2(0,1));
                float4 node_3453 = tex2D(_XClamp,TRANSFORM_TEX(node_3991, _XClamp));
                float node_9449 = (node_8602.r*(node_3617.r+(-1*node_3453.r)));
                float3 diffuseColor = lerp(_HoloBacggroundcolor.rgb,_MainTex_var.rgb,node_9449);
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
