Shader "Car Crashing 3D/Diffuse dirt"
{
  Properties
  {
    [PerRendererData] _BaseColor ("Base color", Color) = (1,1,1,1)
    [PerRendererData] _Texture ("Texture", 2D) = "black" {}
    [PerRendererData] _Dirt ("Dirt", 2D) = "black" {}
    [PerRendererData] _DirtColor ("Dirt Color", Color) = (0.88,0.62,0.3,0)
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
      
      
      #define CODE_BLOCK_VERTEX
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
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _Texture_ST;
      uniform float4 _Dirt_ST;
      uniform sampler2D _Texture;
      uniform sampler2D _Dirt;
      uniform float4 _DirtColor;
      uniform float4 _BaseColor;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD1 :TEXCOORD1;
          float2 xlv_TEXCOORD2 :TEXCOORD2;
          float3 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD1 :TEXCOORD1;
          float2 xlv_TEXCOORD2 :TEXCOORD2;
          float3 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 tmpvar_1;
          tmpvar_1 = in_v.normal;
          float3 worldN_2;
          float2 tmpvar_3;
          float2 tmpvar_4;
          float3 tmpvar_5;
          tmpvar_3 = TRANSFORM_TEX(in_v.texcoord.xy, _Texture);
          tmpvar_4 = TRANSFORM_TEX(in_v.texcoord1.xy, _Dirt);
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = float3(in_v.vertex.xyz);
          float3 norm_7;
          norm_7 = tmpvar_1;
          float3x3 tmpvar_8;
          tmpvar_8[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_8[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_8[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_9;
          tmpvar_9 = normalize(mul(norm_7, tmpvar_8));
          worldN_2 = tmpvar_9;
          float4 tmpvar_10;
          tmpvar_10.w = 1;
          tmpvar_10.xyz = float3(worldN_2);
          float3 res_11;
          float3 x_12;
          x_12.x = dot(unity_SHAr, tmpvar_10);
          x_12.y = dot(unity_SHAg, tmpvar_10);
          x_12.z = dot(unity_SHAb, tmpvar_10);
          float3 x1_13;
          float4 tmpvar_14;
          tmpvar_14 = (worldN_2.xyzz * worldN_2.yzzx);
          x1_13.x = dot(unity_SHBr, tmpvar_14);
          x1_13.y = dot(unity_SHBg, tmpvar_14);
          x1_13.z = dot(unity_SHBb, tmpvar_14);
          res_11 = (x_12 + (x1_13 + (unity_SHC.xyz * ((worldN_2.x * worldN_2.x) - (worldN_2.y * worldN_2.y)))));
          float3 tmpvar_15;
          tmpvar_15 = max(((1.055 * pow(max(res_11, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          res_11 = tmpvar_15;
          float tmpvar_16;
          tmpvar_16 = max(0, dot(worldN_2, _WorldSpaceLightPos0.xyz));
          tmpvar_5 = (tmpvar_15 + (_LightColor0.xyz * tmpvar_16));
          out_v.xlv_TEXCOORD1 = tmpvar_3;
          out_v.xlv_TEXCOORD2 = tmpvar_4;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_COLOR0 = tmpvar_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float dirtFactor_2;
          float4 dirtColor_3;
          float4 dirtTex_4;
          float3 finalColor_5;
          float4 tmpvar_6;
          tmpvar_6 = tex2D(_Texture, in_f.xlv_TEXCOORD1);
          float3 tmpvar_7;
          tmpvar_7 = (_BaseColor * tmpvar_6).xyz.xyz;
          finalColor_5 = tmpvar_7;
          float4 tmpvar_8;
          tmpvar_8 = tex2D(_Dirt, in_f.xlv_TEXCOORD2);
          dirtTex_4 = tmpvar_8;
          float4 tmpvar_9;
          tmpvar_9 = (dirtTex_4 * _DirtColor);
          dirtColor_3 = tmpvar_9;
          float tmpvar_10;
          if(float((dirtTex_4.w>=_DirtColor.w)))
          {
              tmpvar_10 = 1;
          }
          else
          {
              tmpvar_10 = 0;
          }
          dirtFactor_2 = tmpvar_10;
          float _tmp_dvx_2 = (dirtTex_4.w * dirtFactor_2);
          finalColor_5 = (lerp(finalColor_5, dirtColor_3.xyz, float3(_tmp_dvx_2, _tmp_dvx_2, _tmp_dvx_2)) * in_f.xlv_COLOR0);
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(finalColor_5);
          tmpvar_1 = tmpvar_11;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "VertexLit"
}
