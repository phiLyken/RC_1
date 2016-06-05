// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1291,x:32719,y:32712,varname:node_1291,prsc:2|diff-35-OUT,normal-6896-RGB,emission-5237-OUT,clip-3214-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:37,x:29182,y:32269,varname:node_37,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:848,x:31620,y:32176,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_848,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:156b3be5d1d342e49b47174800352b1e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6896,x:31620,y:32370,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74d03a97f9b9a1b4d8210e81c0fddd90,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Clamp01,id:1439,x:31467,y:31576,varname:node_1439,prsc:2|IN-2606-OUT;n:type:ShaderForge.SFN_RemapRange,id:7191,x:30929,y:31262,varname:node_7191,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-4527-OUT;n:type:ShaderForge.SFN_OneMinus,id:4527,x:30731,y:31288,varname:node_4527,prsc:2|IN-799-OUT;n:type:ShaderForge.SFN_Append,id:4892,x:31912,y:31576,varname:node_4892,prsc:2|A-1069-OUT,B-5543-OUT;n:type:ShaderForge.SFN_Tex2d,id:3877,x:32096,y:31576,varname:node_3877,prsc:2,tex:633c206a5ee74c447bfbfcf1002b04c7,ntxv:0,isnm:False|UVIN-4892-OUT,TEX-8394-TEX;n:type:ShaderForge.SFN_Vector1,id:5543,x:31753,y:31711,varname:node_5543,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:1069,x:31639,y:31576,varname:node_1069,prsc:2|IN-1439-OUT;n:type:ShaderForge.SFN_Tex2d,id:8602,x:30467,y:32604,varname:node_8602,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-7348-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_Add,id:9618,x:29722,y:32410,varname:node_9618,prsc:2|A-37-X,B-336-OUT;n:type:ShaderForge.SFN_Vector4Property,id:7889,x:29192,y:32538,ptovrint:False,ptlb:Vector,ptin:_Vector,varname:node_7889,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5,v2:0.5,v3:0.5,v4:1;n:type:ShaderForge.SFN_Negate,id:336,x:29488,y:32482,varname:node_336,prsc:2|IN-7889-X;n:type:ShaderForge.SFN_Append,id:4202,x:29982,y:32413,varname:node_4202,prsc:2|A-9618-OUT,B-4093-OUT;n:type:ShaderForge.SFN_Negate,id:9015,x:29485,y:32772,varname:node_9015,prsc:2|IN-7889-Z;n:type:ShaderForge.SFN_Add,id:4093,x:29722,y:32554,varname:node_4093,prsc:2|A-37-Z,B-9015-OUT;n:type:ShaderForge.SFN_Panner,id:7348,x:30198,y:32424,varname:node_7348,prsc:2,spu:0,spv:1|UVIN-4202-OUT,DIST-5857-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5857,x:29981,y:32554,ptovrint:False,ptlb:Z offset,ptin:_Zoffset,varname:node_5857,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Negate,id:6909,x:29485,y:32626,varname:node_6909,prsc:2|IN-7889-Y;n:type:ShaderForge.SFN_Slider,id:8177,x:29837,y:33147,ptovrint:False,ptlb:z,ptin:_z,varname:node_8177,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.7302879,max:2;n:type:ShaderForge.SFN_Append,id:8112,x:29981,y:32664,varname:node_8112,prsc:2|A-4093-OUT,B-37-X;n:type:ShaderForge.SFN_Panner,id:3377,x:30198,y:32664,varname:node_3377,prsc:2,spu:0,spv:1|UVIN-8112-OUT,DIST-4972-OUT;n:type:ShaderForge.SFN_Slider,id:3478,x:29837,y:33243,ptovrint:False,ptlb:x,ptin:_x,varname:node_3478,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:-0.0006901519,max:2;n:type:ShaderForge.SFN_Tex2dAsset,id:9443,x:30179,y:33077,ptovrint:False,ptlb:z Clamp,ptin:_zClamp,varname:node_9443,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9449,x:31122,y:32718,varname:node_9449,prsc:2|A-8602-R,B-671-OUT;n:type:ShaderForge.SFN_Tex2d,id:3617,x:30467,y:32794,varname:node_3617,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-3377-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_ValueProperty,id:4972,x:29981,y:32813,ptovrint:False,ptlb:-X offset,ptin:_Xoffset,varname:node_4972,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Panner,id:3991,x:30199,y:32899,varname:node_3991,prsc:2,spu:0,spv:1|UVIN-7174-OUT,DIST-5852-OUT;n:type:ShaderForge.SFN_Tex2d,id:3453,x:30478,y:32975,varname:node_3453,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-3991-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_Negate,id:8795,x:30651,y:32975,varname:node_8795,prsc:2|IN-3453-R;n:type:ShaderForge.SFN_Append,id:7174,x:29981,y:32909,varname:node_7174,prsc:2|A-4093-OUT,B-37-X;n:type:ShaderForge.SFN_ValueProperty,id:5852,x:29981,y:33052,ptovrint:False,ptlb:x offset,ptin:_xoffset,varname:node_5852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Slider,id:8475,x:29837,y:33337,ptovrint:False,ptlb:x neg,ptin:_xneg,varname:node_8475,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.7021438,max:2;n:type:ShaderForge.SFN_Add,id:671,x:30811,y:32868,varname:node_671,prsc:2|A-3617-R,B-8795-OUT;n:type:ShaderForge.SFN_ValueProperty,id:799,x:30537,y:31332,ptovrint:False,ptlb:Dissolve on,ptin:_Dissolveon,varname:node_799,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:2606,x:31265,y:31576,varname:node_2606,prsc:2|A-7191-OUT,B-5305-OUT;n:type:ShaderForge.SFN_Multiply,id:7371,x:32675,y:31400,varname:node_7371,prsc:2|A-1183-OUT,B-9566-OUT,C-2040-R;n:type:ShaderForge.SFN_Slider,id:1183,x:31932,y:31373,ptovrint:False,ptlb:Bloom Intesity,ptin:_BloomIntesity,varname:node_1183,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Lerp,id:35,x:31950,y:32159,varname:node_35,prsc:2|A-1983-RGB,B-848-RGB,T-9449-OUT;n:type:ShaderForge.SFN_Color,id:1983,x:31618,y:31959,ptovrint:False,ptlb:Holo Bacgground color,ptin:_HoloBacggroundcolor,varname:node_1983,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:0;n:type:ShaderForge.SFN_Fresnel,id:4450,x:31927,y:32979,varname:node_4450,prsc:2;n:type:ShaderForge.SFN_Color,id:8444,x:31927,y:33202,ptovrint:False,ptlb:HologramColor,ptin:_HologramColor,varname:node_8444,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5093,x:32140,y:32979,varname:node_5093,prsc:2|A-4450-OUT,B-8444-RGB,C-3124-OUT;n:type:ShaderForge.SFN_Multiply,id:165,x:32314,y:32869,varname:node_165,prsc:2|A-3990-OUT,B-5093-OUT;n:type:ShaderForge.SFN_OneMinus,id:3990,x:31662,y:32814,varname:node_3990,prsc:2|IN-9449-OUT;n:type:ShaderForge.SFN_ScreenPos,id:6694,x:31989,y:33354,varname:node_6694,prsc:2,sctp:0;n:type:ShaderForge.SFN_Noise,id:614,x:32169,y:33342,varname:node_614,prsc:2|XY-6694-UVOUT;n:type:ShaderForge.SFN_RemapRange,id:3124,x:32349,y:33342,varname:node_3124,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:0.75|IN-614-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1607,x:30266,y:33277,ptovrint:False,ptlb:x clamp,ptin:_xclamp,varname:node_1607,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f185eb016995a7043aaff3440261ebc8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:8394,x:31552,y:31404,ptovrint:False,ptlb:Borde,ptin:_Borde,varname:node_8394,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:633c206a5ee74c447bfbfcf1002b04c7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_RemapRange,id:8592,x:31065,y:31718,varname:node_8592,prsc:2,frmn:0.4,frmx:0.6,tomn:0,tomx:1|IN-5821-R;n:type:ShaderForge.SFN_RemapRange,id:2722,x:30877,y:31718,varname:node_2722,prsc:2,frmn:0.4,frmx:0.6,tomn:0,tomx:1|IN-6638-R;n:type:ShaderForge.SFN_Add,id:7460,x:31248,y:31186,varname:node_7460,prsc:2|A-7191-OUT,B-9798-OUT;n:type:ShaderForge.SFN_Clamp01,id:5330,x:31450,y:31186,varname:node_5330,prsc:2|IN-7460-OUT;n:type:ShaderForge.SFN_OneMinus,id:9913,x:31622,y:31186,varname:node_9913,prsc:2|IN-5330-OUT;n:type:ShaderForge.SFN_Vector1,id:9095,x:31736,y:31321,varname:node_9095,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:4221,x:31895,y:31186,varname:node_4221,prsc:2|A-9913-OUT,B-9095-OUT;n:type:ShaderForge.SFN_Tex2d,id:1855,x:32089,y:31186,varname:node_1855,prsc:2,tex:633c206a5ee74c447bfbfcf1002b04c7,ntxv:0,isnm:False|UVIN-4221-OUT,TEX-8394-TEX;n:type:ShaderForge.SFN_Add,id:9566,x:32326,y:31419,varname:node_9566,prsc:2|A-1855-RGB,B-3877-RGB;n:type:ShaderForge.SFN_Add,id:5237,x:32503,y:32818,varname:node_5237,prsc:2|A-7371-OUT,B-165-OUT;n:type:ShaderForge.SFN_Tex2d,id:2040,x:30702,y:32039,varname:node_2040,prsc:2,tex:32c43c7d98a6d1042977650f888f319c,ntxv:0,isnm:False|UVIN-7348-UVOUT,TEX-2507-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:2507,x:30251,y:32051,ptovrint:False,ptlb:node_2507,ptin:_node_2507,varname:node_2507,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:32c43c7d98a6d1042977650f888f319c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6638,x:30702,y:32167,varname:node_6638,prsc:2,tex:32c43c7d98a6d1042977650f888f319c,ntxv:0,isnm:False|UVIN-3377-UVOUT,TEX-2507-TEX;n:type:ShaderForge.SFN_Tex2d,id:5821,x:30702,y:32295,varname:node_5821,prsc:2,tex:32c43c7d98a6d1042977650f888f319c,ntxv:0,isnm:False|UVIN-3991-UVOUT,TEX-2507-TEX;n:type:ShaderForge.SFN_Subtract,id:9798,x:30948,y:31447,varname:node_9798,prsc:2|A-2722-OUT,B-8617-OUT;n:type:ShaderForge.SFN_Vector1,id:8617,x:30525,y:31545,cmnt:this offsets the border centered on the cutoff,varname:node_8617,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Subtract,id:5305,x:30948,y:31561,varname:node_5305,prsc:2|A-8592-OUT,B-8617-OUT;n:type:ShaderForge.SFN_Append,id:7206,x:29974,y:32143,varname:node_7206,prsc:2|A-4093-OUT,B-2627-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8011,x:29974,y:32296,ptovrint:False,ptlb:Y offset,ptin:_Yoffset,varname:node_8011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Panner,id:2373,x:30198,y:32252,varname:node_2373,prsc:2,spu:0,spv:1|UVIN-7206-OUT,DIST-8011-OUT;n:type:ShaderForge.SFN_Slider,id:4178,x:29631,y:32086,ptovrint:False,ptlb:y slider,ptin:_yslider,varname:node_4178,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.8127629,max:2;n:type:ShaderForge.SFN_Tex2d,id:399,x:30467,y:32449,varname:node_399,prsc:2,tex:44ed9630c10f0ce44812e8d2beae58fa,ntxv:0,isnm:False|UVIN-2373-UVOUT,TEX-9443-TEX;n:type:ShaderForge.SFN_Add,id:2627,x:29710,y:32250,varname:node_2627,prsc:2|A-37-Y,B-6909-OUT;n:type:ShaderForge.SFN_OneMinus,id:3214,x:31673,y:32583,varname:node_3214,prsc:2|IN-399-R;proporder:848-6896-7889-5857-8177-3478-9443-8475-799-4972-5852-1183-1983-8444-1607-8394-2507-4178-8011;pass:END;sub:END;*/

Shader "Redcliffe/RC_assests_blend" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        [HideInInspector]_Vector ("Vector", Vector) = (0.5,0.5,0.5,1)
        _Zoffset ("Z offset", Float ) = 0
        [HideInInspector]_z ("z", Range(-1, 2)) = 0.7302879
        [HideInInspector]_x ("x", Range(-1, 2)) = -0.0006901519
        _zClamp ("z Clamp", 2D) = "white" {}
        [HideInInspector]_xneg ("x neg", Range(-1, 2)) = 0.7021438
        [HideInInspector]_Dissolveon ("Dissolve on", Float ) = 1
        _Xoffset ("-X offset", Float ) = 0
        _xoffset ("x offset", Float ) = 0
        _BloomIntesity ("Bloom Intesity", Range(0, 2)) = 1
        _HoloBacggroundcolor ("Holo Bacgground color", Color) = (0,0,0,0)
        _HologramColor ("HologramColor", Color) = (0,0.5,0.5,1)
        _xclamp ("x clamp", 2D) = "white" {}
        _Borde ("Borde", 2D) = "white" {}
        _node_2507 ("node_2507", 2D) = "white" {}
        [HideInInspector]_yslider ("y slider", Range(-1, 2)) = 0.8127629
        _Yoffset ("Y offset", Float ) = 0
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 _Vector;
            uniform float _Zoffset;
            uniform sampler2D _zClamp; uniform float4 _zClamp_ST;
            uniform float _Xoffset;
            uniform float _xoffset;
            uniform float _Dissolveon;
            uniform float _BloomIntesity;
            uniform float4 _HoloBacggroundcolor;
            uniform float4 _HologramColor;
            uniform sampler2D _Borde; uniform float4 _Borde_ST;
            uniform sampler2D _node_2507; uniform float4 _node_2507_ST;
            uniform float _Yoffset;
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
                float4 screenPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD11;
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
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_4093 = (i.posWorld.b+(-1*_Vector.b));
                float2 node_2373 = (float2(node_4093,(i.posWorld.g+(-1*_Vector.g)))+_Yoffset*float2(0,1));
                float4 node_399 = tex2D(_zClamp,TRANSFORM_TEX(node_2373, _zClamp));
                clip((1.0 - node_399.r) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
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
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - 0;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_7348 = (float2((i.posWorld.r+(-1*_Vector.r)),node_4093)+_Zoffset*float2(0,1));
                float4 node_8602 = tex2D(_zClamp,TRANSFORM_TEX(node_7348, _zClamp));
                float2 node_3377 = (float2(node_4093,i.posWorld.r)+_Xoffset*float2(0,1));
                float4 node_3617 = tex2D(_zClamp,TRANSFORM_TEX(node_3377, _zClamp));
                float2 node_3991 = (float2(node_4093,i.posWorld.r)+_xoffset*float2(0,1));
                float4 node_3453 = tex2D(_zClamp,TRANSFORM_TEX(node_3991, _zClamp));
                float node_9449 = (node_8602.r*(node_3617.r+(-1*node_3453.r)));
                float3 diffuseColor = lerp(_HoloBacggroundcolor.rgb,_MainTex_var.rgb,node_9449);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_7191 = ((1.0 - _Dissolveon)*1.0+-0.5);
                float4 node_6638 = tex2D(_node_2507,TRANSFORM_TEX(node_3377, _node_2507));
                float node_8617 = (-0.5); // this offsets the border centered on the cutoff
                float2 node_4221 = float2((1.0 - saturate((node_7191+((node_6638.r*5.0+-2.0)-node_8617)))),0.0);
                float4 node_1855 = tex2D(_Borde,TRANSFORM_TEX(node_4221, _Borde));
                float4 node_5821 = tex2D(_node_2507,TRANSFORM_TEX(node_3991, _node_2507));
                float2 node_4892 = float2((1.0 - saturate((node_7191+((node_5821.r*5.0+-2.0)-node_8617)))),0.0);
                float4 node_3877 = tex2D(_Borde,TRANSFORM_TEX(node_4892, _Borde));
                float4 node_2040 = tex2D(_node_2507,TRANSFORM_TEX(node_7348, _node_2507));
                float2 node_614_skew = i.screenPos.rg + 0.2127+i.screenPos.rg.x*0.3713*i.screenPos.rg.y;
                float2 node_614_rnd = 4.789*sin(489.123*(node_614_skew));
                float node_614 = frac(node_614_rnd.x*node_614_rnd.y*(1+node_614_skew.x));
                float3 emissive = ((_BloomIntesity*(node_1855.rgb+node_3877.rgb)*node_2040.r)+((1.0 - node_9449)*((1.0-max(0,dot(normalDirection, viewDirection)))*_HologramColor.rgb*(node_614*0.25+0.5))));
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float4 _Vector;
            uniform float _Zoffset;
            uniform sampler2D _zClamp; uniform float4 _zClamp_ST;
            uniform float _Xoffset;
            uniform float _xoffset;
            uniform float _Dissolveon;
            uniform float _BloomIntesity;
            uniform float4 _HoloBacggroundcolor;
            uniform float4 _HologramColor;
            uniform sampler2D _Borde; uniform float4 _Borde_ST;
            uniform sampler2D _node_2507; uniform float4 _node_2507_ST;
            uniform float _Yoffset;
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
                float4 screenPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
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
                float node_4093 = (i.posWorld.b+(-1*_Vector.b));
                float2 node_2373 = (float2(node_4093,(i.posWorld.g+(-1*_Vector.g)))+_Yoffset*float2(0,1));
                float4 node_399 = tex2D(_zClamp,TRANSFORM_TEX(node_2373, _zClamp));
                clip((1.0 - node_399.r) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_7348 = (float2((i.posWorld.r+(-1*_Vector.r)),node_4093)+_Zoffset*float2(0,1));
                float4 node_8602 = tex2D(_zClamp,TRANSFORM_TEX(node_7348, _zClamp));
                float2 node_3377 = (float2(node_4093,i.posWorld.r)+_Xoffset*float2(0,1));
                float4 node_3617 = tex2D(_zClamp,TRANSFORM_TEX(node_3377, _zClamp));
                float2 node_3991 = (float2(node_4093,i.posWorld.r)+_xoffset*float2(0,1));
                float4 node_3453 = tex2D(_zClamp,TRANSFORM_TEX(node_3991, _zClamp));
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Vector;
            uniform sampler2D _zClamp; uniform float4 _zClamp_ST;
            uniform float _Yoffset;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float node_4093 = (i.posWorld.b+(-1*_Vector.b));
                float2 node_2373 = (float2(node_4093,(i.posWorld.g+(-1*_Vector.g)))+_Yoffset*float2(0,1));
                float4 node_399 = tex2D(_zClamp,TRANSFORM_TEX(node_2373, _zClamp));
                clip((1.0 - node_399.r) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Vector;
            uniform float _Zoffset;
            uniform sampler2D _zClamp; uniform float4 _zClamp_ST;
            uniform float _Xoffset;
            uniform float _xoffset;
            uniform float _Dissolveon;
            uniform float _BloomIntesity;
            uniform float4 _HoloBacggroundcolor;
            uniform float4 _HologramColor;
            uniform sampler2D _Borde; uniform float4 _Borde_ST;
            uniform sampler2D _node_2507; uniform float4 _node_2507_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
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
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_7191 = ((1.0 - _Dissolveon)*1.0+-0.5);
                float node_4093 = (i.posWorld.b+(-1*_Vector.b));
                float2 node_3377 = (float2(node_4093,i.posWorld.r)+_Xoffset*float2(0,1));
                float4 node_6638 = tex2D(_node_2507,TRANSFORM_TEX(node_3377, _node_2507));
                float node_8617 = (-0.5); // this offsets the border centered on the cutoff
                float2 node_4221 = float2((1.0 - saturate((node_7191+((node_6638.r*5.0+-2.0)-node_8617)))),0.0);
                float4 node_1855 = tex2D(_Borde,TRANSFORM_TEX(node_4221, _Borde));
                float2 node_3991 = (float2(node_4093,i.posWorld.r)+_xoffset*float2(0,1));
                float4 node_5821 = tex2D(_node_2507,TRANSFORM_TEX(node_3991, _node_2507));
                float2 node_4892 = float2((1.0 - saturate((node_7191+((node_5821.r*5.0+-2.0)-node_8617)))),0.0);
                float4 node_3877 = tex2D(_Borde,TRANSFORM_TEX(node_4892, _Borde));
                float2 node_7348 = (float2((i.posWorld.r+(-1*_Vector.r)),node_4093)+_Zoffset*float2(0,1));
                float4 node_2040 = tex2D(_node_2507,TRANSFORM_TEX(node_7348, _node_2507));
                float4 node_8602 = tex2D(_zClamp,TRANSFORM_TEX(node_7348, _zClamp));
                float4 node_3617 = tex2D(_zClamp,TRANSFORM_TEX(node_3377, _zClamp));
                float4 node_3453 = tex2D(_zClamp,TRANSFORM_TEX(node_3991, _zClamp));
                float node_9449 = (node_8602.r*(node_3617.r+(-1*node_3453.r)));
                float2 node_614_skew = i.screenPos.rg + 0.2127+i.screenPos.rg.x*0.3713*i.screenPos.rg.y;
                float2 node_614_rnd = 4.789*sin(489.123*(node_614_skew));
                float node_614 = frac(node_614_rnd.x*node_614_rnd.y*(1+node_614_skew.x));
                o.Emission = ((_BloomIntesity*(node_1855.rgb+node_3877.rgb)*node_2040.r)+((1.0 - node_9449)*((1.0-max(0,dot(normalDirection, viewDirection)))*_HologramColor.rgb*(node_614*0.25+0.5))));
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffColor = lerp(_HoloBacggroundcolor.rgb,_MainTex_var.rgb,node_9449);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
