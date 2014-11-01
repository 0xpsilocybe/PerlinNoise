﻿using System;
using System.IO;
using Perlin.GUI.Helpers;
using Perlin.GUI.Kernel;
using Perlin.GUI.Models;
using System.Windows;

namespace Perlin.GUI.ViewModel
{
    public sealed partial class ConfigurationViewModel : ViewModelBase
    {
        #region Fields
        private PerlinDllManager _perlinDllManager { get; set; }
        private readonly Stopwatch _stopwatch;
        #endregion // Fields

        #region Properties
        #region Program state
        private GeneratorState _programState;

        public GeneratorState ProgramState
        {
            get { return _programState; }
            set 
            { 
                if(value == _programState) return;
                _programState = value;
                OnPropertyChanged();
            }
        }
        #endregion // Program state

        #region Generated image array
        private byte[] _generatedFileArray;
        public byte[] GeneratedFileArray
        {
            get { return _generatedFileArray; }
            set
            {
                if(value == _generatedFileArray) return;
                _generatedFileArray = value;
                OnPropertyChanged();
            }
        }
        #endregion // Generated image array

        #region Main
        #region Generating library

        private Library _generatingLibrary;
        public Library GeneratingLibrary
        {
            get { return _generatingLibrary; }
            set
            {
                _generatingLibrary = value; 
                OnPropertyChanged();
            }
        }
        #endregion // Generating library

        #region File type

        private FileType _fileType;
        public FileType GeneratedFileType
        {
            get { return _fileType; }
            set
            {
                _fileType = value;
                OnPropertyChanged();
            }
        }
        #endregion // File type

        #region Number of threads
        private int _numberOfThreads;
        public int NumberOfThreads
        {
            get { return _numberOfThreads; }
            set
            {
                _numberOfThreads = value;
                OnPropertyChanged();
            }
        }
        #endregion // Number of threads

        #region Size
        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                if (value > 10000) value = 10000;
                if (value < 10) value = 10;
                _height = value;
                OnPropertyChanged();
            }
        }
        private int _width;
        public int Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                if (value > 10000) value = 10000;
                if (value < 10) value = 10;
                _width = value;
                OnPropertyChanged();
            }
        }
        #endregion // Size
        #endregion // Main

        #region Generator
        #region Number of octaves
        private int _numberOfOctaves;
        public int NumberOfOctaves
        {
            get { return _numberOfOctaves; }
            set
            {
                if (value < 1) value = 1;
                if (value > 50) value = 50;
                _numberOfOctaves = value;
                OnPropertyChanged();
            }
        }
        #endregion // Number of octaves

        // TODO: dodać persystancję

        #endregion // Generator

        #region Bitmap options
        #region Noise effect bitmap
        NoiseEffects _noiseEffectBmp;
        public NoiseEffects NoiseEffectBmp
        {
            get { return _noiseEffectBmp; }
            set
            {
                _noiseEffectBmp = value;
                OnPropertyChanged();
            }
        }
        #endregion // Noise effect bitmap

        #region Noise color bitmap
        NoiseColor _noiseColorBmp;
        public NoiseColor CurrentNoiseColorBmp
        {
            get { return _noiseColorBmp; }
            set
            {
                _noiseColorBmp = value;
                OnPropertyChanged();
            }
        }

        #endregion // Noise color bitmap
        #endregion // Bitmap options

        #region GIF options
        #region Noise effect GIF
        NoiseEffects _noiseEffectGif;
        public NoiseEffects NoiseEffectGif
        {
            get { return _noiseEffectGif; }
            set
            {
                _noiseEffectGif = value;
                OnPropertyChanged();
            }
        }
        #endregion // Noise effect GIF

        #region Noise color GIF
        NoiseColor _noiseColorGif;
        public NoiseColor CurrentNoiseColorGif
        {
            get { return _noiseColorGif; }
            set
            {
                _noiseColorGif = value;
                OnPropertyChanged();
            }
        }

        #endregion // Noise color GIF

        #endregion // GIF options
        #endregion // Properties

        #region Constructor
        public ConfigurationViewModel()
        {
            InitializeCommands();
            InitializeProperties();


            _stopwatch = new Stopwatch(TimeSpan.FromMilliseconds(10));
            //_stopwatch.Updated += updatedTime =>{Stopwatch} 
        }

        private void InitializeProperties()
        {
            GeneratingLibrary = Library.PureC;
            GeneratedFileType = FileType.Bitmap;
            Width = Height = 10;

            NumberOfThreads = 4;
            NumberOfOctaves = 5;

            NoiseEffectBmp = NoiseEffects.SinOfNoise;
            CurrentNoiseColorBmp = NoiseColor.Green;
        }

        #endregion // Constructor

        #region User interface handling
        private void SaveFile(string path)
        {
            try
            {
                File.WriteAllBytes(path, GeneratedFileArray);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion // User interface handling
    }
}