/*
	TODO:
	= przerobi� na DLL
	= Port do ASM.

	  PRAWDOPODOBNIE WSZYSTKIEMU JEST WINNY GENERATOR LICZB LOSOWYCH
	  PROPONOWANE ROZWI�ZANIE: ZAIMPLEMENTOWA� MERSENNE TWISTER
	  WWW: http://en.wikipedia.org/wiki/Mersenne_twister
*/

#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#include "Globals.h"
#include "PerlinNoise.h"
#include "Bitmap.h"
#include "MyMath.h"

/*********************************************
				MAIN PROGRAM
**********************************************/
int main(char* argv[], __int32 argc)
{
	printf("*** Perlin Noise -- 2D textures ***\n");
	printf("*******  Bartlomiej Szostek *******\n\n");

	srand(time(NULL));

	printf(" ->\tCalculating values using Perlin Noise");
	PerlinNoise_2D();
	printf(" - DONE.\n");

	printf(" ->\tGenerating bitmap");
	CreateBMP(NoiseArray, outputBitmapFileName);
	printf(" - DONE.\n");
	PrintBMPInfo(outputBitmapFileName);

	//SaveArrayToFile(Noise2D, "array_per_5_00.txt");

	return 0;
}