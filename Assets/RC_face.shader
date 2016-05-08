// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-7344-OUT,spec-694-OUT,gloss-1560-OUT,normal-5591-RGB;n:type:ShaderForge.SFN_Tex2d,id:5827,x:32132,y:32676,ptovrint:False,ptlb:lid,ptin:_lid,varname:node_5827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6670da0949ba6c142b843b88a9b76fd5,ntxv:0,isnm:False|UVIN-2181-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9861,x:32219,y:33091,ptovrint:False,ptlb:pupil,ptin:_pupil,varname:node_9861,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:845ef1fd3ce87e344bacd096abdc5a68,ntxv:0,isnm:False|UVIN-3095-OUT;n:type:ShaderForge.SFN_TexCoord,id:3357,x:31739,y:32668,varname:node_3357,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:41,x:30823,y:33022,varname:node_41,prsc:2,uv:0;n:type:ShaderForge.SFN_UVTile,id:2181,x:31932,y:32791,varname:node_2181,prsc:2|UVIN-3357-UVOUT,WDT-3413-OUT,HGT-3413-OUT,TILE-324-OUT;n:type:ShaderForge.SFN_Vector1,id:3413,x:31720,y:32825,varname:node_3413,prsc:2,v1:2;n:type:ShaderForge.SFN_Slider,id:1340,x:30666,y:33192,ptovrint:False,ptlb:x offset,ptin:_xoffset,varname:node_1340,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.3,cur:0,max:0.3;n:type:ShaderForge.SFN_Slider,id:9460,x:30666,y:33293,ptovrint:False,ptlb:y offset,ptin:_yoffset,varname:node_9460,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.15,cur:0,max:0.15;n:type:ShaderForge.SFN_Add,id:3699,x:31070,y:33022,varname:node_3699,prsc:2|A-9067-R,B-1340-OUT;n:type:ShaderForge.SFN_Add,id:9239,x:31070,y:33205,varname:node_9239,prsc:2|A-9067-G,B-9460-OUT;n:type:ShaderForge.SFN_Append,id:3095,x:31878,y:33066,varname:node_3095,prsc:2|A-3699-OUT,B-9239-OUT;n:type:ShaderForge.SFN_Lerp,id:7344,x:32502,y:32875,varname:node_7344,prsc:2|A-5827-RGB,B-9861-RGB,T-2838-OUT;n:type:ShaderForge.SFN_Vector1,id:1560,x:32334,y:33374,varname:node_1560,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:4722,x:32134,y:32873,ptovrint:False,ptlb:alpha mask,ptin:_alphamask,varname:node_4722,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b80f361539bfb0e4a8d481a972702d8d,ntxv:0,isnm:False|UVIN-2181-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:2838,x:32308,y:32890,varname:node_2838,prsc:2|IN-4722-R;n:type:ShaderForge.SFN_ValueProperty,id:324,x:31720,y:33016,ptovrint:False,ptlb:lid pos,ptin:_lidpos,varname:node_324,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:9067,x:30823,y:32863,ptovrint:False,ptlb:uv spherical,ptin:_uvspherical,varname:node_9067,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9bc68db6ac8d9f847bcd86a9949d81d9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5591,x:31786,y:33303,ptovrint:False,ptlb:normal,ptin:_normal,varname:node_5591,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cad21de636d3f4344863b6df4e8825aa,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector1,id:9786,x:32298,y:32661,varname:node_9786,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:694,x:32466,y:32744,varname:node_694,prsc:2|A-9786-OUT,B-2838-OUT;proporder:5827-9861-1340-9460-4722-324-9067-5591;pass:END;sub:END;*/

Shader "Shader Forge/RC_face" {
    Properties {
        _lid ("lid", 2D) = "white" {}
        _pupil ("pupil", 2D) = "white" {}
        _xoffset ("x offset", Range(-0.3, 0.3)) = 0
        _yoffset ("y offset", Range(-0.15, 0.15)) = 0
        _alphamask ("alpha mask", 2D) = "white" {}
        _lidpos ("lid pos", Float ) = 0
        _uvspherical ("uv spherical", 2D) = "white" {}
        _normal ("normal", 2D) = "bump" {}
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
            uniform sampler2D _lid; uniform float4 _lid_ST;
            uniform sampler2D _pupil; uniform float4 _pupil_ST;
            uniform float _xoffset;
            uniform float _yoffset;
            uniform sampler2D _alphamask; uniform float4 _alphamask_ST;
            uniform float _lidpos;
            uniform sampler2D _uvspherical; uniform float4 _uvspherical_ST;
            uniform sampler2D _normal; uniform float4 _normal_ST;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(i.uv0, _normal)));
                float3 normalLocal = _normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_3413 = 2.0;
                float2 node_2181_tc_rcp = float2(1.0,1.0)/float2( node_3413, node_3413 );
                float node_2181_ty = floor(_lidpos * node_2181_tc_rcp.x);
                float node_2181_tx = _lidpos - node_3413 * node_2181_ty;
                float2 node_2181 = (i.uv0 + float2(node_2181_tx, node_2181_ty)) * node_2181_tc_rcp;
                float4 _alphamask_var = tex2D(_alphamask,TRANSFORM_TEX(node_2181, _alphamask));
                float node_2838 = (1.0 - _alphamask_var.r);
                float node_694 = (2.0*node_2838);
                float3 specularColor = float3(node_694,node_694,node_694);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _lid_var = tex2D(_lid,TRANSFORM_TEX(node_2181, _lid));
                float4 _uvspherical_var = tex2D(_uvspherical,TRANSFORM_TEX(i.uv0, _uvspherical));
                float2 node_3095 = float2((_uvspherical_var.r+_xoffset),(_uvspherical_var.g+_yoffset));
                float4 _pupil_var = tex2D(_pupil,TRANSFORM_TEX(node_3095, _pupil));
                float3 diffuseColor = lerp(_lid_var.rgb,_pupil_var.rgb,node_2838);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            uniform sampler2D _lid; uniform float4 _lid_ST;
            uniform sampler2D _pupil; uniform float4 _pupil_ST;
            uniform float _xoffset;
            uniform float _yoffset;
            uniform sampler2D _alphamask; uniform float4 _alphamask_ST;
            uniform float _lidpos;
            uniform sampler2D _uvspherical; uniform float4 _uvspherical_ST;
            uniform sampler2D _normal; uniform float4 _normal_ST;
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
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
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
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(i.uv0, _normal)));
                float3 normalLocal = _normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_3413 = 2.0;
                float2 node_2181_tc_rcp = float2(1.0,1.0)/float2( node_3413, node_3413 );
                float node_2181_ty = floor(_lidpos * node_2181_tc_rcp.x);
                float node_2181_tx = _lidpos - node_3413 * node_2181_ty;
                float2 node_2181 = (i.uv0 + float2(node_2181_tx, node_2181_ty)) * node_2181_tc_rcp;
                float4 _alphamask_var = tex2D(_alphamask,TRANSFORM_TEX(node_2181, _alphamask));
                float node_2838 = (1.0 - _alphamask_var.r);
                float node_694 = (2.0*node_2838);
                float3 specularColor = float3(node_694,node_694,node_694);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _lid_var = tex2D(_lid,TRANSFORM_TEX(node_2181, _lid));
                float4 _uvspherical_var = tex2D(_uvspherical,TRANSFORM_TEX(i.uv0, _uvspherical));
                float2 node_3095 = float2((_uvspherical_var.r+_xoffset),(_uvspherical_var.g+_yoffset));
                float4 _pupil_var = tex2D(_pupil,TRANSFORM_TEX(node_3095, _pupil));
                float3 diffuseColor = lerp(_lid_var.rgb,_pupil_var.rgb,node_2838);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
