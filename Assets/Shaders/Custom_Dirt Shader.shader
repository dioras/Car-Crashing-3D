Shader "Custom/Dirt Shader"
{
  Properties
  {
    _BaseColor ("Base Color", Color) = (1,1,1,1)
    _DecalLayer1 ("Decal Layer 1", 2D) = "white" {}
    _DecalLayer1Color ("Decal Layer 1 Color", Color) = (1,1,1,0)
    _DirtLayer1 ("Dirt Layer 1", 2D) = "white" {}
    _DirtLayer1Color ("Dirt Layer 1 Color", Color) = (1,1,1,0)
    _DirtAlphaCutOff ("Dirt Layer 1 Cut Off", float) = 1
    _MatCapLookup ("MatCap Lookup", 2D) = "white" {}
    _UseMatCap ("Use Mat Cap", Range(0, 1)) = 1
    _DiffuseLightModifier ("Diffuse Light Modifier", Range(0, 1)) = 1
    _ReflectionColor ("Reflection Color", Color) = (1,1,1,1)
    _ReflectionMap ("Reflection Map", Cube) = "" {}
    _ReflectionStrength ("Reflection Strength", Range(0, 1)) = 0.5
  }
  SubShader
  {
    Tags
    { 
      "LIGHTMODE" = "FORWARDBASE"
      "QUEUE" = "Geometry"
      "RenderType" = "Opaque"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "FORWARDBASE"
        "QUEUE" = "Geometry"
        "RenderType" = "Opaque"
      }
      Fog
      { 
        Mode  Linear
      } 
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixInvV;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _DecalLayer1_ST;
      uniform float4 _DirtLayer1_ST;
      uniform float4 _BaseColor;
      uniform float4 _DecalLayer1Color;
      uniform float4 _DirtLayer1Color;
      uniform sampler2D _DecalLayer1;
      uniform sampler2D _DirtLayer1;
      uniform sampler2D _MatCapLookup;
      uniform float4 _ReflectionColor;
      uniform samplerCUBE _ReflectionMap;
      uniform float _ReflectionStrength;
      uniform float _DirtAlphaCutOff;
      uniform float _DiffuseLightModifier;
      uniform int _UseMatCap;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float4 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4x4 m_1;
          m_1 = mul(unity_WorldToObject, unity_MatrixInvV);
          float4 tmpvar_2;
          float4 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_2.x = conv_mxt4x4_0(m_1).x;
          tmpvar_2.y = conv_mxt4x4_1(m_1).x;
          tmpvar_2.z = conv_mxt4x4_2(m_1).x;
          tmpvar_2.w = conv_mxt4x4_3(m_1).x;
          tmpvar_3.x = conv_mxt4x4_0(m_1).y.x;
          tmpvar_3.y = conv_mxt4x4_1(m_1).y.x;
          tmpvar_3.z = conv_mxt4x4_2(m_1).y.x;
          tmpvar_3.w = conv_mxt4x4_3(m_1).y.x;
          tmpvar_4.x = conv_mxt4x4_0(m_1).z.x;
          tmpvar_4.y = conv_mxt4x4_1(m_1).z.x;
          tmpvar_4.z = conv_mxt4x4_2(m_1).z.x;
          tmpvar_4.w = conv_mxt4x4_3(m_1).z.x;
          tmpvar_5.x = conv_mxt4x4_0(m_1).w.x;
          tmpvar_5.y = conv_mxt4x4_1(m_1).w.x;
          tmpvar_5.z = conv_mxt4x4_2(m_1).w.x;
          tmpvar_5.w = conv_mxt4x4_3(m_1).w.x;
          float nl_6;
          float3 worldNormal_7;
          float4 tmpvar_8;
          float4 tmpvar_9;
          float4 tmpvar_10;
          tmpvar_8.xy = float2(TRANSFORM_TEX(in_v.texcoord.xy, _DecalLayer1));
          float4 v_11;
          v_11.x = tmpvar_2.x;
          v_11.y = tmpvar_3.x;
          v_11.z = tmpvar_4.x;
          v_11.w = tmpvar_5.x;
          float3 tmpvar_12;
          tmpvar_12 = normalize(in_v.normal);
          tmpvar_8.z = dot(normalize(v_11.xyz), tmpvar_12);
          float4 v_13;
          v_13.x = tmpvar_2.y;
          v_13.y = tmpvar_3.y;
          v_13.z = tmpvar_4.y;
          v_13.w = tmpvar_5.y;
          tmpvar_8.w = dot(normalize(v_13.xyz), tmpvar_12);
          tmpvar_8.zw = ((tmpvar_8.zw * 0.5) + 0.5);
          tmpvar_9.xy = float2(TRANSFORM_TEX(in_v.texcoord1.xy, _DirtLayer1));
          float4 v_14;
          v_14.x = tmpvar_2.x;
          v_14.y = tmpvar_3.x;
          v_14.z = tmpvar_4.x;
          v_14.w = tmpvar_5.x;
          tmpvar_9.z = dot(normalize(v_14.xyz), tmpvar_12);
          float4 v_15;
          v_15.x = tmpvar_2.y;
          v_15.y = tmpvar_3.y;
          v_15.z = tmpvar_4.y;
          v_15.w = tmpvar_5.y;
          tmpvar_9.w = dot(normalize(v_15.xyz), tmpvar_12);
          tmpvar_9.zw = ((tmpvar_9.zw * 0.5) + 0.5);
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = float3(in_v.vertex.xyz);
          float3x3 tmpvar_17;
          tmpvar_17[0] = conv_mxt4x4_0(unity_ObjectToWorld).xyz;
          tmpvar_17[1] = conv_mxt4x4_1(unity_ObjectToWorld).xyz;
          tmpvar_17[2] = conv_mxt4x4_2(unity_ObjectToWorld).xyz;
          float3 tmpvar_18;
          tmpvar_18 = normalize(mul(tmpvar_17, in_v.normal));
          float3 I_19;
          I_19 = (mul(unity_ObjectToWorld, in_v.vertex).xyz - _WorldSpaceCameraPos);
          float3x3 tmpvar_20;
          tmpvar_20[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_20[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_20[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_21;
          tmpvar_21 = normalize(mul(in_v.normal, tmpvar_20));
          worldNormal_7 = tmpvar_21;
          float tmpvar_22;
          tmpvar_22 = max(0, dot(worldNormal_7, _WorldSpaceLightPos0.xyz));
          nl_6 = tmpvar_22;
          tmpvar_10 = (nl_6 * _LightColor0);
          float4 tmpvar_23;
          tmpvar_23.w = 1;
          tmpvar_23.xyz = float3(worldNormal_7);
          float3 res_24;
          float3 x_25;
          x_25.x = dot(unity_SHAr, tmpvar_23);
          x_25.y = dot(unity_SHAg, tmpvar_23);
          x_25.z = dot(unity_SHAb, tmpvar_23);
          float3 x1_26;
          float4 tmpvar_27;
          tmpvar_27 = (worldNormal_7.xyzz * worldNormal_7.yzzx);
          x1_26.x = dot(unity_SHBr, tmpvar_27);
          x1_26.y = dot(unity_SHBg, tmpvar_27);
          x1_26.z = dot(unity_SHBb, tmpvar_27);
          res_24 = (x_25 + (x1_26 + (unity_SHC.xyz * ((worldNormal_7.x * worldNormal_7.x) - (worldNormal_7.y * worldNormal_7.y)))));
          float3 tmpvar_28;
          tmpvar_28 = max(((1.055 * pow(max(res_24, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          res_24 = tmpvar_28;
          tmpvar_10.xyz = float3((tmpvar_10.xyz + tmpvar_28));
          out_v.xlv_TEXCOORD0 = tmpvar_8;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_16));
          out_v.xlv_TEXCOORD1 = (I_19 - (2 * (dot(tmpvar_18, I_19) * tmpvar_18)));
          out_v.xlv_TEXCOORD2 = tmpvar_9;
          out_v.xlv_COLOR0 = tmpvar_10;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          tmpvar_1 = in_f.xlv_COLOR0;
          float3 finalColor_2;
          float4 tmpvar_3;
          tmpvar_3 = texCUBE(_ReflectionMap, in_f.xlv_TEXCOORD1);
          float3 tmpvar_4;
          tmpvar_4 = (tmpvar_3.xyz * _ReflectionColor.xyz);
          float4 tmpvar_5;
          tmpvar_5 = tex2D(_DecalLayer1, in_f.xlv_TEXCOORD0.xy);
          float4 tmpvar_6;
          tmpvar_6 = (tmpvar_5 * _DecalLayer1Color);
          float4 tmpvar_7;
          tmpvar_7 = tex2D(_DirtLayer1, in_f.xlv_TEXCOORD2.xy);
          float4 tmpvar_8;
          tmpvar_8 = (tmpvar_7 * _DirtLayer1Color);
          float3 tmpvar_9;
          tmpvar_9 = lerp(_BaseColor.xyz, tmpvar_6.xyz, tmpvar_6.www);
          finalColor_2 = tmpvar_9;
          if((_UseMatCap>0))
          {
              float3 matCapColor_10;
              float3 tmpvar_11;
              tmpvar_11 = tex2D(_MatCapLookup, in_f.xlv_TEXCOORD0.zw).xyz.xyz;
              matCapColor_10 = tmpvar_11;
              float4 tmpvar_12;
              tmpvar_12.xyz = float3(((tmpvar_9 * matCapColor_10) * 2));
              tmpvar_12.w = _BaseColor.w;
              finalColor_2 = tmpvar_12.xyz;
          }
          float3 tmpvar_13;
          tmpvar_13 = lerp(finalColor_2, tmpvar_4, float3(_ReflectionStrength, _ReflectionStrength, _ReflectionStrength));
          finalColor_2 = tmpvar_13;
          if((tmpvar_8.w>_DirtAlphaCutOff))
          {
              finalColor_2 = lerp(tmpvar_13, tmpvar_8.xyz, tmpvar_8.www);
          }
          tmpvar_1 = (in_f.xlv_COLOR0 * (in_f.xlv_COLOR0 * in_f.xlv_COLOR0));
          finalColor_2 = (((finalColor_2 * tmpvar_1.xyz) + finalColor_2) * _DiffuseLightModifier);
          float4 tmpvar_14;
          tmpvar_14.w = 1;
          tmpvar_14.xyz = float3(finalColor_2);
          out_f.color = tmpvar_14;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "VertexLit"
}
