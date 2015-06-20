//2012.03.12
//2012.04.05
//2013.06.26
//2013.07.05


Shader "@MGKJ_Base" 
{
	Properties 
	{
	
		_SpecColor0("Specular Color", Color) = (0,0,0,1)
		_Shininess("Shininess", Range(0,1) ) = 0.5
		
		_Color ("Main Color", Color) = (0,0,0,1)
		_MainTex ("DiffuseMap", 2D) = "white" {}
		
		_globalInfluence("Global Influence", Range(0,1) ) = 1
		_brightness("Brightness", Range(1,5) ) = 1
		_contrast("Contrast", Range(1,2.5) ) = 1

		_lightmap_color("Lighting Color", Color) = (0,0,0,1)
		_LightMap ("SecondMap (CompleteMap or LightMap)", 2D) = "white" {}

		_changeType ("<<<HDR Type    ***    NoHDR>>>", Range(0,1)) =0
			
		_SelectColor ("Outline Color", Color) = (0.2,0.6,0.8,0)
		_SelectColorAlpha("Select Color Alpha",Range(0,1) ) = 0

	    
	   
	}
	SubShader {
	

	
		Tags { "RenderType"="Opaque"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong_wjm finalcolor:mycolor
		#pragma multi_compile_fwdadd_fullshadows
		#pragma target 3.0
		#pragma profileoption MaxTexIndirections=64
//		#pragma debug
		

		float4x4 _RotateUVM;
		float _GLOBALBRIGHTNESS;
		float _GLOBALCONTRASR;
		
		float depth_bias;
		float border_clamp;
		float _changeType;
		
		float4 _SpecColor0;
		
		float _Parallax;
		float _Shininess;
		float _brightness;
		float _contrast;
		float4 _Color;
		float4 _lightmap_color;
		float4 _lightmap_color2;
//		float _reflect_blender;
//		float _fresnel_ctrl;
		
		sampler2D _MainTex;
//		sampler2D _BumpMap;
		sampler2D _LightMap;
//		sampler2D _LightMap2;
//		sampler2D _relaxedcone_relief_map;
//		samplerCUBE _Cube;

		float4 _SelectColor;		
		float _SelectColorAlpha;		
		float _globalInfluence;


		struct Input {
			float2 uv_MainTex;
//			float2 uv_BumpMap;
			float2 uv2_LightMap;

		};

		struct SurfaceOutput_wjm 
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Gloss;
			half Specular;
			half Alpha;
		};
		
		
		inline half4 LightingBlinnPhong_wjm_PrePass (SurfaceOutput_wjm s, half4 light)
		{
			half3 spec = light.a * s.Gloss;
			half4 c;
			c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
			c.a = s.Alpha;
			return c;
		}
		
		
		inline half4 LightingBlinnPhong_wjm (SurfaceOutput_wjm s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 h = normalize (lightDir + viewDir);
			
			half diff = max (0, dot ( lightDir, s.Normal ));
				
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular*128.0);
				
			half4 lightPower;
			lightPower.rgb = _LightColor0.rgb * diff;
			lightPower.w = spec * Luminance (_LightColor0.rgb);
			lightPower *= atten * 2.0;
				
			half4 c;
			c.rgb = (s.Albedo * lightPower.rgb + lightPower.rgb * spec*s.Gloss);
			c.a = 1;
			return c;
		}
		

		
		void mycolor (Input IN, SurfaceOutput_wjm o, inout fixed4 color)
		{
//          color *= _lightmap_color;
          color += _SelectColor*_SelectColorAlpha;
		}

		
inline float3 DecodeLightmap2( float4 color )
{
#if defined(SHADER_API_GLES) && defined(SHADER_API_MOBILE)
	return 2.0 * color.rgb;
#else
	// potentially faster to do the scalar multiplication
	// in parenthesis for scalar GPUs
//	return pow((8.0 * color.a),2.2) * color.rgb;
	return pow((_GLOBALBRIGHTNESS*_globalInfluence+_brightness)* lerp(8.0 * color.a,1,_changeType)* color.rgb,_contrast+_GLOBALCONTRASR*_globalInfluence);
	
#endif
}
		
		
		
		
		void surf (Input IN, inout SurfaceOutput_wjm o)
		{			
//			float3 p,v;	
//			setup_ray(IN,p,v);
//			ray_intersect_relaxedcone(_relaxedcone_relief_map,p,v);
			
			IN.uv_MainTex=mul(IN.uv_MainTex.xyxy, _RotateUVM).xy;
			
			o.Normal = float3(0.0,0.0,1.0);
//			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
//			o.Normal  = tex2D(_relaxedcone_relief_map,p.xy).xyz;
			
			float4 Tex2D0=tex2D(_MainTex,IN.uv_MainTex);
			float4 Tex2D1=0;
//			Tex2D1+=_lightmap_color*pow(_brightness.xxxx +tex2D(_LightMap,(IN.uv2_LightMap).xy),_contrast.xxxx)- _brightness.xxxx;
			Tex2D1+=_lightmap_color*float4(DecodeLightmap2(tex2D(_LightMap,(IN.uv2_LightMap).xy)),1);
			
			
//			float3 worldRefl = WorldReflectionVector (IN, o.Normal);
//			fixed4 cubeColor = texCUBE (_Cube, IN.worldRefl);

//			float fresnel_intensity=1-clamp(pow((max(0,dot (normalize(IN.viewDir), o.Normal))),-_fresnel_ctrl).xxxx,0,1);
		
			float3 diffuseColor=Tex2D0;
			
			
			o.Albedo=_Color.rgb*diffuseColor;
//			o.Albedo=_Color.rgb*Tex2D0;
			
			o.Emission=Tex2D1.rgb*diffuseColor;
			
			o.Specular=_Shininess ;
			
			o.Gloss =Tex2D0.a*_SpecColor0;
			
			o.Alpha = Tex2D0.a * _Color.a;
			o.Normal=normalize(o.Normal);

		}
	
		ENDCG
		
	} 
	FallBack "Diffuse"

}
