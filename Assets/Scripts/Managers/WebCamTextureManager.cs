﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using OpenCV;

public class WebCamTextureManager : MonoBehaviour
{
	// Video parameters
	public MeshRenderer webCamTextureRenderer;
	public int deviceNumber;
	private WebCamTexture _webCamTexture;

	// Webcam size
	private const int webCamWidth = 640;
	private const int webCamHeight = 360;

	// OpenCV Image Setting Object
	private OpenCVImage openCVImage;

	void Start() {
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length < 0) {
			Debug.Log ("카메라가 없습니다.");
		} else {
			_webCamTexture = new WebCamTexture (devices [deviceNumber].name,
				webCamWidth, webCamHeight);
			webCamTextureRenderer.material.mainTexture = _webCamTexture;
			_webCamTexture.Play ();

			openCVImage = new OpenCVImage (webCamHeight, webCamWidth);
		}
	}

	void Update() {
		if (_webCamTexture.isPlaying && _webCamTexture.didUpdateThisFrame) {
			openCVImage.TextureToMat (_webCamTexture.GetPixels32());
		}
	}
}