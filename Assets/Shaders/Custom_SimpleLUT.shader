// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_ShadowFadeCenterAndType', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_DynamicLightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable

Shader "Custom/SimpleLUT"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "" {}
    _Amount ("Amount of Color Filter (0 - 1)", float) = 1
    _Tint ("Tint (RGB)", Color) = (1,1,1,1)
    _Hue ("Hue (0 - 360)", float) = 0
    _Saturation ("Saturation (0 - 2)", float) = 1
    _Brightness ("Brightness (0 - 3)", float) = 1
    _Contrast ("Contrast (0 - 2)", float) = 1
    _Sharpness ("Sharpness (-4 - 4)", float) = 0
  }
  SubShader
  {
    Tags
    { 
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
      }
      ZTest Always
      ZWrite Off
      Cull Off
      // m_ProgramMask = 6
      CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f_vertex_lit members uv,diff,spec)
#pragma exclude_renderers d3d11
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _Time;
      //uniform float4 _SinTime;
      //uniform float4 _CosTime;
      //uniform float4 unity_DeltaTime;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _ProjectionParams;
      //uniform float4 _ScreenParams;
      //uniform float4 _ZBufferParams;
      //uniform float4 unity_OrthoParams;
      //uniform float4 unity_CameraWorldClipPlanes[6];
      //uniform float4x4 unity_CameraProjection;
      //uniform float4x4 unity_CameraInvProjection;
      //uniform float4x4 unity_WorldToCamera;
      //uniform float4x4 unity_CameraToWorld;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 _LightPositionRange;
      //uniform float4 unity_4LightPosX0;
      //uniform float4 unity_4LightPosY0;
      //uniform float4 unity_4LightPosZ0;
      //uniform float4 unity_4LightAtten0;
      //uniform float4 unity_LightColor[8];
      //uniform float4 unity_LightPosition[8];
      //uniform float4 unity_LightAtten[8];
      //uniform float4 unity_SpotDirection[8];
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4 unity_OcclusionMaskSelector;
      uniform float4 unity_ProbesOcclusion;
      uniform float3 unity_LightColor0;
      uniform float3 unity_LightColor1;
      uniform float3 unity_LightColor2;
      uniform float3 unity_LightColor3;
      uniform float4x4 unity_ShadowSplitSpheres;
      uniform float4 unity_ShadowSplitSqRadii;
      //uniform float4 unity_LightShadowBias;
      //uniform float4 _LightSplitsNear;
      //uniform float4 _LightSplitsFar;
      //uniform float4x4x4 unity_WorldToShadow;
      //uniform float4 _LightShadowData;
      // uniform float4 unity_ShadowFadeCenterAndType;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_LODFade;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 glstate_matrix_transpose_modelview0;
      //uniform float4 glstate_lightmodel_ambient;
      //uniform float4 unity_AmbientSky;
      //uniform float4 unity_AmbientEquator;
      //uniform float4 unity_AmbientGround;
      uniform float4 unity_IndirectSpecColor;
      //uniform float4x4 UNITY_MATRIX_P;
      //uniform float4x4 unity_MatrixV;
      //uniform float4x4 unity_MatrixInvV;
      //uniform float4x4 unity_MatrixVP;
      //uniform int unity_StereoEyeIndex;
      uniform float4 unity_ShadowColor;
      //uniform float4 unity_FogColor;
      //uniform float4 unity_FogParams;
      // uniform sampler2D unity_Lightmap;
      // uniform sampler2D unity_LightmapInd;
      // uniform sampler2D unity_DynamicLightmap;
      uniform sampler2D unity_DynamicDirectionality;
      uniform sampler2D unity_DynamicNormal;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      //uniform samplerCUBE unity_SpecCube0;
      //uniform samplerCUBE unity_SpecCube1;
      SamplerState samplerunity_SpecCube1;
      //uniform float4 unity_SpecCube0_BoxMax;
      //uniform float4 unity_SpecCube0_BoxMin;
      //uniform float4 unity_SpecCube0_ProbePosition;
      //uniform float4 unity_SpecCube0_HDR;
      //uniform float4 unity_SpecCube1_BoxMax;
      //uniform float4 unity_SpecCube1_BoxMin;
      //uniform float4 unity_SpecCube1_ProbePosition;
      //uniform float4 unity_SpecCube1_HDR;
      //uniform float4 unity_Lightmap_HDR;
      uniform float4 unity_DynamicLightmap_HDR;
      uniform sampler2D _MainTex;
      uniform sampler3D _ClutTex;
      uniform float _Amount;
      uniform float4 _TintColor;
      uniform float _Hue;
      uniform float _Saturation;
      uniform float _Brightness;
      uniform float _Contrast;
      uniform float _Scale;
      uniform float _Offset;
      uniform float2 _ImageWidthFactor;
      uniform float2 _ImageHeightFactor;
      uniform float _SharpnessCenterMultiplier;
      uniform float _SharpnessEdgeMultiplier;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float2x2 xll_transpose_mf2x2(float2x2 m)
      {
      }
      
      float3x3 xll_transpose_mf3x3(float3x3 m)
      {
      }
      
      float4x4 xll_transpose_mf4x4(float4x4 m)
      {
      }
      
      struct v2f_vertex_lit
      {
          float2 uv;
          float4 diff;
          float4 spec;
      };
      
      struct v2f_img
      {
          float4 pos;
          float2 uv;
      };
      
      struct appdata_img
      {
          float4 vertex;
          float2 texcoord;
      };
      
      struct v2f
      {
          float4 pos;
          float2 uv;
      };
      
      //float4x4 unity_MatrixMVP;
      //float4x4 unity_MatrixMV;
      //float4x4 unity_MatrixTMV;
      float4x4 unity_MatrixITMV;
      float4 UnityObjectToClipPos(in float3 pos )
      {
          return {mul(unity_MatrixVP, mul(unity_ObjectToWorld, float4(pos, 1)))};
      }
      
      float4 UnityObjectToClipPos(in float4 pos )
      {
          return UnityObjectToClipPos(pos.xyz);
      }
      
      v2f vert(in appdata_img v )
      {
          OUT_Data_Vert out_v;
          v2f o;
          o.pos = UnityObjectToClipPos(v.vertex);
          o.uv = v.texcoord.xy;
          //return o;
          return out_v;
      }
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          unity_MatrixMVP = mul(unity_MatrixVP, unity_ObjectToWorld);
          unity_MatrixMV = mul(unity_MatrixV, unity_ObjectToWorld);
          unity_MatrixTMV = xll_transpose_mf4x4(unity_MatrixMV);
          unity_MatrixITMV = xll_transpose_mf4x4(mul(unity_WorldToObject, unity_MatrixInvV));
          v2f xl_retval;
          appdata_img xlt_v;
          xlt_v.vertex = float4(in_v.vertex);
          xlt_v.texcoord = float2(in_v.texcoord.xy);
          xl_retval = vert(xlt_v);
          out_v.vertex = float4(xl_retval.pos);
          out_v.xlv_TEXCOORD0 = float2(xl_retval.uv);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2x2 xll_transpose_mf2x2(float2x2 m)
      {
      }
      
      float3x3 xll_transpose_mf3x3(float3x3 m)
      {
      }
      
      float4x4 xll_transpose_mf4x4(float4x4 m)
      {
      }
      
      struct v2f_vertex_lit
      {
          float2 uv;
          float4 diff;
          float4 spec;
      };
      
      struct v2f_img
      {
          float4 pos;
          float2 uv;
      };
      
      struct appdata_img
      {
          float4 vertex;
          float2 texcoord;
      };
      
      struct v2f
      {
          float4 pos;
          float2 uv;
      };
      
      //float4x4 unity_MatrixMVP_d;
      //float4x4 unity_MatrixMV_d;
      //float4x4 unity_MatrixTMV_d;
      float4x4 unity_MatrixITMV_d;
      float3 applyHue(in float3 aColor )
      {
          float angle = radians(_Hue);
          float3 k = float3(0.57735, 0.57735, 0.57735);
          float cosAngle = cos(angle);
          return {(((aColor * cosAngle) + (cross(k, aColor) * sin(angle))) + ((k * dot(k, aColor)) * (1 - cosAngle)))};
      }
      
      float3 applyHSBEffect(in float3 c )
      {
          c.xyz = float3(applyHue(c.xyz));
          c.xyz = float3((((c.xyz - 0.5) * _Contrast) + 0.5));
          c.xyz = float3((c.xyz * _Brightness));
          float _tmp_dvx_6 = dot(c.xyz, float3(0.299, 0.587, 0.114));
          float3 intensity = float3(_tmp_dvx_6, _tmp_dvx_6, _tmp_dvx_6);
          c.xyz = float3(lerp(intensity, c.xyz, float3(_Saturation, _Saturation, _Saturation)));
          return c;
      }
      
      float3 applySharpness(in float3 c, in float2 uv )
      {
          return {((c * _SharpnessCenterMultiplier) - ((((tex2D(_MainTex, (uv - _ImageWidthFactor)).xyz * _SharpnessEdgeMultiplier) + (tex2D(_MainTex, (uv + _ImageWidthFactor)).xyz * _SharpnessEdgeMultiplier)) + (tex2D(_MainTex, (uv + _ImageHeightFactor)).xyz * _SharpnessEdgeMultiplier)) + (tex2D(_MainTex, (uv - _ImageHeightFactor)).xyz * _SharpnessEdgeMultiplier)))};
      }
      
      float4 frag(in v2f i )
      {
          OUT_Data_Frag out_f;
          float4 c = tex2D(_MainTex, i.uv);
          c.xyz = float3(applySharpness(c.xyz, i.uv));
          c.xyz = float3(applyHSBEffect(c.xyz));
          float3 correctedColor = tex3D(_ClutTex, ((c.xyz * _Scale) + _Offset)).xyz;
          c.xyz = float3((lerp(c.xyz, correctedColor, float3(_Amount, _Amount, _Amount)) * float3(_TintColor.xyz)));
          //return c;
          return out_f;
      }
      
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          unity_MatrixMVP_d = mul(unity_MatrixVP, unity_ObjectToWorld);
          unity_MatrixMV_d = mul(unity_MatrixV, unity_ObjectToWorld);
          unity_MatrixTMV_d = xll_transpose_mf4x4(unity_MatrixMV_d);
          unity_MatrixITMV_d = xll_transpose_mf4x4(mul(unity_WorldToObject, unity_MatrixInvV));
          float4 xl_retval;
          v2f xlt_i;
          xlt_i.pos = float4(0, 0, 0, 0);
          xlt_i.uv = float2(in_f.xlv_TEXCOORD0);
          xl_retval = frag(xlt_i);
          out_f.color = float4(xl_retval);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: 
    {
      Tags
      { 
      }
      ZTest Always
      ZWrite Off
      Cull Off
      // m_ProgramMask = 6
      CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f_vertex_lit members uv,diff,spec)
#pragma exclude_renderers d3d11
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _Time;
      //uniform float4 _SinTime;
      //uniform float4 _CosTime;
      //uniform float4 unity_DeltaTime;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _ProjectionParams;
      //uniform float4 _ScreenParams;
      //uniform float4 _ZBufferParams;
      //uniform float4 unity_OrthoParams;
      //uniform float4 unity_CameraWorldClipPlanes[6];
      //uniform float4x4 unity_CameraProjection;
      //uniform float4x4 unity_CameraInvProjection;
      //uniform float4x4 unity_WorldToCamera;
      //uniform float4x4 unity_CameraToWorld;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 _LightPositionRange;
      //uniform float4 unity_4LightPosX0;
      //uniform float4 unity_4LightPosY0;
      //uniform float4 unity_4LightPosZ0;
      //uniform float4 unity_4LightAtten0;
      //uniform float4 unity_LightColor[8];
      //uniform float4 unity_LightPosition[8];
      //uniform float4 unity_LightAtten[8];
      //uniform float4 unity_SpotDirection[8];
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4 unity_OcclusionMaskSelector;
      uniform float4 unity_ProbesOcclusion;
      uniform float3 unity_LightColor0;
      uniform float3 unity_LightColor1;
      uniform float3 unity_LightColor2;
      uniform float3 unity_LightColor3;
      uniform float4x4 unity_ShadowSplitSpheres;
      uniform float4 unity_ShadowSplitSqRadii;
      //uniform float4 unity_LightShadowBias;
      //uniform float4 _LightSplitsNear;
      //uniform float4 _LightSplitsFar;
      //uniform float4x4x4 unity_WorldToShadow;
      //uniform float4 _LightShadowData;
      // uniform float4 unity_ShadowFadeCenterAndType;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_LODFade;
      //uniform float4 unity_WorldTransformParams;
      //uniform float4x4 glstate_matrix_transpose_modelview0;
      //uniform float4 glstate_lightmodel_ambient;
      //uniform float4 unity_AmbientSky;
      //uniform float4 unity_AmbientEquator;
      //uniform float4 unity_AmbientGround;
      uniform float4 unity_IndirectSpecColor;
      //uniform float4x4 UNITY_MATRIX_P;
      //uniform float4x4 unity_MatrixV;
      //uniform float4x4 unity_MatrixInvV;
      //uniform float4x4 unity_MatrixVP;
      //uniform int unity_StereoEyeIndex;
      uniform float4 unity_ShadowColor;
      //uniform float4 unity_FogColor;
      //uniform float4 unity_FogParams;
      // uniform sampler2D unity_Lightmap;
      // uniform sampler2D unity_LightmapInd;
      // uniform sampler2D unity_DynamicLightmap;
      uniform sampler2D unity_DynamicDirectionality;
      uniform sampler2D unity_DynamicNormal;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      //uniform samplerCUBE unity_SpecCube0;
      //uniform samplerCUBE unity_SpecCube1;
      SamplerState samplerunity_SpecCube1;
      //uniform float4 unity_SpecCube0_BoxMax;
      //uniform float4 unity_SpecCube0_BoxMin;
      //uniform float4 unity_SpecCube0_ProbePosition;
      //uniform float4 unity_SpecCube0_HDR;
      //uniform float4 unity_SpecCube1_BoxMax;
      //uniform float4 unity_SpecCube1_BoxMin;
      //uniform float4 unity_SpecCube1_ProbePosition;
      //uniform float4 unity_SpecCube1_HDR;
      //uniform float4 unity_Lightmap_HDR;
      uniform float4 unity_DynamicLightmap_HDR;
      uniform sampler2D _MainTex;
      uniform sampler3D _ClutTex;
      uniform float _Amount;
      uniform float4 _TintColor;
      uniform float _Hue;
      uniform float _Saturation;
      uniform float _Brightness;
      uniform float _Contrast;
      uniform float _Scale;
      uniform float _Offset;
      uniform float2 _ImageWidthFactor;
      uniform float2 _ImageHeightFactor;
      uniform float _SharpnessCenterMultiplier;
      uniform float _SharpnessEdgeMultiplier;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float2x2 xll_transpose_mf2x2(float2x2 m)
      {
      }
      
      float3x3 xll_transpose_mf3x3(float3x3 m)
      {
      }
      
      float4x4 xll_transpose_mf4x4(float4x4 m)
      {
      }
      
      struct v2f_vertex_lit
      {
          float2 uv;
          float4 diff;
          float4 spec;
      };
      
      struct v2f_img
      {
          float4 pos;
          float2 uv;
      };
      
      struct appdata_img
      {
          float4 vertex;
          float2 texcoord;
      };
      
      struct v2f
      {
          float4 pos;
          float2 uv;
      };
      
      //float4x4 unity_MatrixMVP;
      //float4x4 unity_MatrixMV;
      //float4x4 unity_MatrixTMV;
      float4x4 unity_MatrixITMV;
      float4 UnityObjectToClipPos(in float3 pos )
      {
          return {mul(unity_MatrixVP, mul(unity_ObjectToWorld, float4(pos, 1)))};
      }
      
      float4 UnityObjectToClipPos(in float4 pos )
      {
          return UnityObjectToClipPos(pos.xyz);
      }
      
      v2f vert(in appdata_img v )
      {
          OUT_Data_Vert out_v;
          v2f o;
          o.pos = UnityObjectToClipPos(v.vertex);
          o.uv = v.texcoord.xy;
          //return o;
          return out_v;
      }
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          unity_MatrixMVP = mul(unity_MatrixVP, unity_ObjectToWorld);
          unity_MatrixMV = mul(unity_MatrixV, unity_ObjectToWorld);
          unity_MatrixTMV = xll_transpose_mf4x4(unity_MatrixMV);
          unity_MatrixITMV = xll_transpose_mf4x4(mul(unity_WorldToObject, unity_MatrixInvV));
          v2f xl_retval;
          appdata_img xlt_v;
          xlt_v.vertex = float4(in_v.vertex);
          xlt_v.texcoord = float2(in_v.texcoord.xy);
          xl_retval = vert(xlt_v);
          out_v.vertex = float4(xl_retval.pos);
          out_v.xlv_TEXCOORD0 = float2(xl_retval.uv);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2x2 xll_transpose_mf2x2(float2x2 m)
      {
      }
      
      float3x3 xll_transpose_mf3x3(float3x3 m)
      {
      }
      
      float4x4 xll_transpose_mf4x4(float4x4 m)
      {
      }
      
      struct v2f_vertex_lit
      {
          float2 uv;
          float4 diff;
          float4 spec;
      };
      
      struct v2f_img
      {
          float4 pos;
          float2 uv;
      };
      
      struct appdata_img
      {
          float4 vertex;
          float2 texcoord;
      };
      
      struct v2f
      {
          float4 pos;
          float2 uv;
      };
      
      //float4x4 unity_MatrixMVP_d;
      //float4x4 unity_MatrixMV_d;
      //float4x4 unity_MatrixTMV_d;
      float4x4 unity_MatrixITMV_d;
      float3 applyHue(in float3 aColor )
      {
          float angle = radians(_Hue);
          float3 k = float3(0.57735, 0.57735, 0.57735);
          float cosAngle = cos(angle);
          return {(((aColor * cosAngle) + (cross(k, aColor) * sin(angle))) + ((k * dot(k, aColor)) * (1 - cosAngle)))};
      }
      
      float3 applyHSBEffect(in float3 c )
      {
          c.xyz = float3(applyHue(c.xyz));
          c.xyz = float3((((c.xyz - 0.5) * _Contrast) + 0.5));
          c.xyz = float3((c.xyz * _Brightness));
          float _tmp_dvx_7 = dot(c.xyz, float3(0.299, 0.587, 0.114));
          float3 intensity = float3(_tmp_dvx_7, _tmp_dvx_7, _tmp_dvx_7);
          c.xyz = float3(lerp(intensity, c.xyz, float3(_Saturation, _Saturation, _Saturation)));
          return c;
      }
      
      float3 applySharpness(in float3 c, in float2 uv )
      {
          return {((c * _SharpnessCenterMultiplier) - ((((tex2D(_MainTex, (uv - _ImageWidthFactor)).xyz * _SharpnessEdgeMultiplier) + (tex2D(_MainTex, (uv + _ImageWidthFactor)).xyz * _SharpnessEdgeMultiplier)) + (tex2D(_MainTex, (uv + _ImageHeightFactor)).xyz * _SharpnessEdgeMultiplier)) + (tex2D(_MainTex, (uv - _ImageHeightFactor)).xyz * _SharpnessEdgeMultiplier)))};
      }
      
      float4 fragLinear(in v2f i )
      {
          float4 c = tex2D(_MainTex, i.uv);
          c.xyz = float3(applySharpness(c.xyz, i.uv));
          float _tmp_dvx_8 = sqrt(c.xyz);
          c.xyz = float3(_tmp_dvx_8, _tmp_dvx_8, _tmp_dvx_8);
          c.xyz = float3(applyHSBEffect(c.xyz));
          float3 correctedColor = tex3D(_ClutTex, ((c.xyz * _Scale) + _Offset)).xyz;
          c.xyz = float3((lerp(c.xyz, correctedColor, float3(_Amount, _Amount, _Amount)) * float3(_TintColor.xyz)));
          c.xyz = float3((c.xyz * c.xyz));
          return c;
      }
      
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          unity_MatrixMVP_d = mul(unity_MatrixVP, unity_ObjectToWorld);
          unity_MatrixMV_d = mul(unity_MatrixV, unity_ObjectToWorld);
          unity_MatrixTMV_d = xll_transpose_mf4x4(unity_MatrixMV_d);
          unity_MatrixITMV_d = xll_transpose_mf4x4(mul(unity_WorldToObject, unity_MatrixInvV));
          float4 xl_retval;
          v2f xlt_i;
          xlt_i.pos = float4(0, 0, 0, 0);
          xlt_i.uv = float2(in_f.xlv_TEXCOORD0);
          xl_retval = fragLinear(xlt_i);
          out_f.color = float4(xl_retval);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
