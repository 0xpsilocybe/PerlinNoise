.686p
.387
.model flat, stdcall
option casemap:none
.xmm

.data
	ALIGN 16

	;;;;;;;;;;;;;;;;;;;;;;;;
	;; Initialization arrays
		
		p		        DWORD  0		; Helper array
		g2		        DWORD  0		; Noise generator initialization array
		NoiseArray		DWORD  0		; Array for generated noise values

	;;;;;;;;;;;;;
	;; IMMEDIATES

		B				DWORD  1000h	; Array size
		BMask			DWORD  0FFFh	; Array size mask

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Includes
;;;;;;;;;;;
;;;; Libs
;;;;;;;;;;;
	include \masm32\include\Windows.inc
	include \masm32\include\Kernel32.inc
	include \masm32\include\MSVCRT.inc
	include \masm32\include\Masm32.inc
	include \masm32\MasmBasic\MasmBasic.inc

	includelib \masm32\lib\Kernel32.lib
	includelib \masm32\lib\MSVCRT.lib
	includelib \masm32\lib\Masm32.lib
	includelib \masm32\MasmBasic\MasmBasic.lib

;;;;;;;;;;;
;;;; Own
;;;;;;;;;;;
	include DataStructures.asm
	include Helpers.asm
	include PerlinNoise.asm
	include Bitmap.asm

.code
	;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
	;; Normalizes REAL8 2D vector 
	Normalize PROC vector : DWORD

		MOVAPS xmm0, [vector]	; Move two values of vector to xmm0
		MOVAPS xmm2, xmm0		; Store vector for later use
		MULPD  xmm0, xmm0		; Calculate square of each value
		MOVAPS xmm1, xmm0		; Store vector for calculations
		ADDPD  xmm0, xmm1		; Add squares of vector components
		SQRTPD xmm0, xmm0		; Calculate square root of added components

		DIVPD xmm2, xmm0		; Calculate v[0]/len(v) v[1]/len(v)
		MOVAPS [vector], xmm2

		MOV eax, vector
		RET
	Normalize ENDP
	
	;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
	;; Initializes program arrays
	_Init PROC FAR w:DWORD, h:DWORD	

		LOCAL s, j, k  :  DWORD

		
		XOR eax, eax
		MOV s, eax
		MOV j, eax
		MOV k, eax
		
		Rand()				; Initialize random number generator

		; Allocate memory for arrays
			MOV eax, B
			INC eax
			SHL eax, 3		; eax  <- eax * 2 * sizeof(DWORD)
			MOV s, eax
			INVOKE crt_malloc, s
			MOV p, eax
			
			SHL s, 2
			INVOKE crt_malloc, s
			MOV g2, eax

			MOV eax, w
			MUL h
			SHL eax, 5
			INVOKE crt_malloc, eax
			MOV NoiseArray, eax

		; Initialize arrays for generating Perlin Noise
			MOV edi, OFFSET p
			MOV ecx, s
			SHR ecx, 2
			InitP_First:
				void Rand(0, 10000h)		; Generate random number
				STOSD
				DEC ecx

				; g2[i][0] = (double)((rand() % (B + B)) - B) / B;
				; g2[i][0] = (double)((rand() % (B + B)) - B) / B;

				;INVOKE Normalize, g2[i]
			JNZ InitP_First




		XOR eax, eax
		RET
	_Init ENDP

	;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
	;; Generate noisy bitmap with applied effect
	_PerlinNoiseBmp PROC params : THREADPARAMS
		
		INVOKE PerlinNoise2D, NoiseArray, params	; Generate noise array with parameters
		INVOKE CreateBMP, NoiseArray, params		; Create bitmap from noise array
		XOR eax, eax
		RET
	_PerlinNoiseBmp ENDP

	;;;;;;;;;;;;;;;;;;;;;;;;;;
	;; Cleans up memory
	_Finalize PROC FAR

		INVOKE crt_free, p
		INVOKE crt_free, g2
		INVOKE crt_free, NoiseArray

		XOR eax, eax
		RET
	_Finalize ENDP

END