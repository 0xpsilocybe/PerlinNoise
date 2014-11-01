#include "stdafx.h"

// Print information about current thread parameters
void printThreadParamInfo(ThreadParameters params);

// Print information about BMP file
void PrintBMPInfo(const char* bmpName);

// Saves data from 2D array to file
void SaveArrayToFile(double array2D[SIZE][SIZE], const char* fileName);

// Allocs 2-dimensional array
double** alloc2DArray(int width, int height);

// Frees 2-dimensional array
void free2DArray(double** pointer, int width, int height);