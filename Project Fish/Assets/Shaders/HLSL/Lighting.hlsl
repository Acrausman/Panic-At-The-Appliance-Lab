﻿void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAtten, out float ShadowAtten)
{

	// set the shader graph node previews
	#ifdef SHADERGRAPH_PREVIEW
		Direction = normalize(float3(0.5f,0.5f, 0.25f));
		Color = float3(1.0f, 1.0f, 1.0f);
		DistanceAtten = 1.0f;
		ShadowAtten = 1.0f;

	#else
		// grab the shadow coordinates
		#if SHADOWS_SCREEN
			half4 shadowCoord = ComputeScreenPos(ClipPos);
		#else
			half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
		#endif 

		Light mainLight = GetMainLight(shadowCoord);

		Direction = mainLight.direction;
		Color = mainLight.color
		DistanceAtten = mainLight.DistanceAttenuation;
		ShadowAtten = mainLight.ShadowAttenuation;
	#endif
}