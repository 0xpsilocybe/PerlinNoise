/*
	PRAWDOPODOBNIE WSZYSTKIEMU JEST WINNY GENERATOR LICZB LOSOWYCH
	PROPONOWANE ROZWI�ZANIE: ZAIMPLEMENTOWA� MERSENNE TWISTER
	WWW: http://en.wikipedia.org/wiki/Mersenne_twister
*/

#include "stdafx.h"
#include "Perlin.PureC.h"
#include "Helpers.h"

PERLINPUREC_API void GeneratePerlinNoiseBitmap(ThreadParameters params)
{	
	NoiseArrayDynamic = alloc2DArray(params.width, params.height);

	PerlinNoise_2D(params);
	printf("Tworzenie bitmapy. ID: %d.\n", params.threadId);
	CreateBMP2(params);

	free2DArray(NoiseArrayDynamic, params.width, params.height);
}

PERLINPUREC_API void GeneratePerlinNoiseGif(ThreadParameters params)
{
	printf("Not implemented.\n");
}