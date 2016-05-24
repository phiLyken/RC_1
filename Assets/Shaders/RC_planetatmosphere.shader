// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|diff-7339-OUT,spec-358-OUT,gloss-1813-OUT,alpha-4331-OUT;n:type:ShaderForge.SFN_Color,id:6665,x:32001,y:32633,ptovrint:False,ptlb:Cloud Color,ptin:_CloudColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:358,x:32250,y:32780,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:32250,y:32882,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_RemapRange,id:5589,x:30904,y:33476,varname:node_5589,prsc:2,frmn:0,frmx:0.5,tomn:0.5,tomx:1|IN-3869-B;n:type:ShaderForge.SFN_Tex2d,id:270,x:30554,y:33152,varname:node_270,prsc:2,tex:010c7cc8844d1a54b8339a641a8672aa,ntxv:0,isnm:False|UVIN-7065-UVOUT,TEX-4785-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:4785,x:30090,y:33336,ptovrint:False,ptlb:Clouds,ptin:_Clouds,varname:node_4785,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:010c7cc8844d1a54b8339a641a8672aa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:4320,x:30090,y:33152,varname:node_4320,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:7065,x:30335,y:33152,varname:node_7065,prsc:2,spu:-0.01,spv:0|UVIN-4320-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5153,x:30565,y:33325,varname:node_5153,prsc:2,tex:010c7cc8844d1a54b8339a641a8672aa,ntxv:0,isnm:False|UVIN-8717-UVOUT,TEX-4785-TEX;n:type:ShaderForge.SFN_Panner,id:8717,x:30335,y:33325,varname:node_8717,prsc:2,spu:-0.005,spv:0|UVIN-4320-UVOUT;n:type:ShaderForge.SFN_Add,id:9219,x:31431,y:33181,varname:node_9219,prsc:2|A-6578-OUT,B-3145-OUT;n:type:ShaderForge.SFN_Tex2d,id:3869,x:30565,y:33493,varname:node_3869,prsc:2,tex:010c7cc8844d1a54b8339a641a8672aa,ntxv:0,isnm:False|UVIN-7131-UVOUT,TEX-4785-TEX;n:type:ShaderForge.SFN_Panner,id:7131,x:30335,y:33493,varname:node_7131,prsc:2,spu:0.01,spv:0.01|UVIN-4320-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6578,x:31140,y:33181,varname:node_6578,prsc:2|A-270-R,B-5589-OUT;n:type:ShaderForge.SFN_Clamp01,id:4496,x:31819,y:33221,varname:node_4496,prsc:2|IN-5789-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3274,x:31959,y:33742,varname:node_3274,prsc:2|IN-270-R,IMIN-3263-OUT,IMAX-1539-OUT,OMIN-1901-OUT,OMAX-696-OUT;n:type:ShaderForge.SFN_Slider,id:3263,x:31522,y:33762,ptovrint:False,ptlb:node_3263,ptin:_node_3263,varname:node_3263,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1539,x:31505,y:33836,ptovrint:False,ptlb:node_1539,ptin:_node_1539,varname:node_1539,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:1901,x:31486,y:33953,ptovrint:False,ptlb:floor,ptin:_floor,varname:node_1901,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:696,x:31505,y:34051,ptovrint:False,ptlb:ceiling,ptin:_ceiling,varname:node_696,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Fresnel,id:2333,x:31636,y:32928,varname:node_2333,prsc:2|EXP-8063-OUT;n:type:ShaderForge.SFN_Slider,id:8063,x:31289,y:33009,ptovrint:False,ptlb:Atmo thickness,ptin:_Atmothickness,varname:node_8063,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:4,max:8;n:type:ShaderForge.SFN_Color,id:5306,x:32001,y:32478,ptovrint:False,ptlb:Sky Color,ptin:_SkyColor,varname:node_5306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.6,c3:0.9,c4:1;n:type:ShaderForge.SFN_Lerp,id:7339,x:32463,y:32611,varname:node_7339,prsc:2|A-5306-RGB,B-6665-RGB,T-4496-OUT;n:type:ShaderForge.SFN_Lerp,id:4331,x:32547,y:33063,varname:node_4331,prsc:2|A-4496-OUT,B-2999-OUT,T-7566-OUT;n:type:ShaderForge.SFN_OneMinus,id:7566,x:32254,y:33204,varname:node_7566,prsc:2|IN-4496-OUT;n:type:ShaderForge.SFN_Multiply,id:3145,x:31176,y:33363,varname:node_3145,prsc:2|A-5153-G,B-5589-OUT;n:type:ShaderForge.SFN_Slider,id:2128,x:31303,y:33363,ptovrint:False,ptlb:Clouds thickness,ptin:_Cloudsthickness,varname:node_2128,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1.5;n:type:ShaderForge.SFN_Multiply,id:5789,x:31635,y:33227,varname:node_5789,prsc:2|A-9219-OUT,B-2128-OUT;n:type:ShaderForge.SFN_Multiply,id:2999,x:31927,y:32962,varname:node_2999,prsc:2|A-2333-OUT,B-735-OUT;n:type:ShaderForge.SFN_Slider,id:735,x:31516,y:33102,ptovrint:False,ptlb:Atmo Transparency,ptin:_AtmoTransparency,varname:node_735,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:4785-6665-358-5306-2128-735-1813-8063;pass:END;sub:END;*/

Shader "Redcliffe/RC_planetatmosphere" {
    Properties {
        _Clouds ("Clouds", 2D) = "white" {}
        _CloudColor ("Cloud Color", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0, 1)) = 0
        _SkyColor ("Sky Color", Color) = (0.5,0.6,0.9,1)
        _Cloudsthickness ("Clouds thickness", Range(0, 1.5)) = 1
        _AtmoTransparency ("Atmo Transparency", Range(0, 1)) = 1
        _Gloss ("Gloss", Range(0, 1)) = 0
        _Atmothickness ("Atmo thickness", Range(1, 8)) = 4
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _CloudColor;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform float _Atmothickness;
            uniform float4 _SkyColor;
            uniform float _Cloudsthickness;
            uniform float _AtmoTransparency;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                UNITY_FOG_COORDS(7)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD8;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 node_7111 = _Time + _TimeEditor;
                float2 node_7065 = (i.uv0+node_7111.g*float2(-0.01,0));
                float4 node_270 = tex2D(_Clouds,TRANSFORM_TEX(node_7065, _Clouds));
                float2 node_7131 = (i.uv0+node_7111.g*float2(0.01,0.01));
                float4 node_3869 = tex2D(_Clouds,TRANSFORM_TEX(node_7131, _Clouds));
                float node_5589 = (node_3869.b*1.0+0.5);
                float2 node_8717 = (i.uv0+node_7111.g*float2(-0.005,0));
                float4 node_5153 = tex2D(_Clouds,TRANSFORM_TEX(node_8717, _Clouds));
                float node_4496 = saturate((((node_270.r*node_5589)+(node_5153.g*node_5589))*_Cloudsthickness));
                float3 diffuseColor = lerp(_SkyColor.rgb,_CloudColor.rgb,node_4496); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,lerp(node_4496,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Atmothickness)*_AtmoTransparency),(1.0 - node_4496)));
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _CloudColor;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform float _Atmothickness;
            uniform float4 _SkyColor;
            uniform float _Cloudsthickness;
            uniform float _AtmoTransparency;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
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
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 node_5778 = _Time + _TimeEditor;
                float2 node_7065 = (i.uv0+node_5778.g*float2(-0.01,0));
                float4 node_270 = tex2D(_Clouds,TRANSFORM_TEX(node_7065, _Clouds));
                float2 node_7131 = (i.uv0+node_5778.g*float2(0.01,0.01));
                float4 node_3869 = tex2D(_Clouds,TRANSFORM_TEX(node_7131, _Clouds));
                float node_5589 = (node_3869.b*1.0+0.5);
                float2 node_8717 = (i.uv0+node_5778.g*float2(-0.005,0));
                float4 node_5153 = tex2D(_Clouds,TRANSFORM_TEX(node_8717, _Clouds));
                float node_4496 = saturate((((node_270.r*node_5589)+(node_5153.g*node_5589))*_Cloudsthickness));
                float3 diffuseColor = lerp(_SkyColor.rgb,_CloudColor.rgb,node_4496); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * lerp(node_4496,(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Atmothickness)*_AtmoTransparency),(1.0 - node_4496)),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _CloudColor;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _Clouds; uniform float4 _Clouds_ST;
            uniform float4 _SkyColor;
            uniform float _Cloudsthickness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 node_3766 = _Time + _TimeEditor;
                float2 node_7065 = (i.uv0+node_3766.g*float2(-0.01,0));
                float4 node_270 = tex2D(_Clouds,TRANSFORM_TEX(node_7065, _Clouds));
                float2 node_7131 = (i.uv0+node_3766.g*float2(0.01,0.01));
                float4 node_3869 = tex2D(_Clouds,TRANSFORM_TEX(node_7131, _Clouds));
                float node_5589 = (node_3869.b*1.0+0.5);
                float2 node_8717 = (i.uv0+node_3766.g*float2(-0.005,0));
                float4 node_5153 = tex2D(_Clouds,TRANSFORM_TEX(node_8717, _Clouds));
                float node_4496 = saturate((((node_270.r*node_5589)+(node_5153.g*node_5589))*_Cloudsthickness));
                float3 diffColor = lerp(_SkyColor.rgb,_CloudColor.rgb,node_4496);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
