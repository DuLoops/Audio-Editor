# Audio and Image Editor Project

## User Guide

1. Clone the current repository.
2. Click the .exe shortcut of the desired program (audio editor or image editor).
3. Use the functionalities provided in the program according to your requirement.

This project is an audio and image editor developed in C# using WPF framework. The project provides the following functionalities for audio and image editing:

## Audio Editor

- Custom wave analyzer
- Fourier transformation
- Audio crop

## Image Editor

- JPEG Compression (using custom compression technique)
- Entropy Encoding
- Image Morpher

### JPEG Compression

The following steps are used in custom JPEG compression:

1. Translate RGB to YCrCb
2. Reduce color components CrCb Subsample by taking every other pixel value in the color component
3. Break the pixels into 8x8 blocks (values between -128->127). Perform DCT on each block
4. Quantize: Divide each DCT value by it's corresponding table value and round to nearest int
5. Entropy Encoding

### Image Morpher

The Image Morpher functionality allows users to morph two images by drawing lines on the source frame, changing the lines of the destination frame, and clicking the "Run" button. The morphing technique used is warping on the source frame to the destination shape(s) using reverse mapping technique.


## Technologies Used

The project is developed in C# using the WPF framework.

Thank you for using our audio and image editor project.
