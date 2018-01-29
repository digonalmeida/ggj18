using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraAspectRatioController : MonoBehaviour {
    public float aspectW = 16;
    public float aspectH = 9;
    Camera _camera;
	// Use this for initialization
	void Start () {
        _camera = GetComponent<Camera>();
	}

    void AdjustViewport()
    {
        float aspectRatio = (float)aspectW / (float)aspectH;
        int h = Screen.height;
        int w = Screen.width;
        float desiredW = (float) h * aspectRatio;

        if (w <= desiredW || true)
        {
            float desiredH = w / aspectRatio;
            AdjustHeight(h, desiredH);
        }
        if(w > desiredW || true)
        {
            AdjustWidth(w, desiredW);
        }
    }

    void AdjustWidth(float w, float desiredW)
    {
        Rect cameraRect = _camera.rect;
        float newSize = desiredW / w;
        float borderSize = (1 - newSize) / 2;
        float xMin = borderSize;
        float xMax = 1 - borderSize;
        cameraRect.xMin = xMin;
        cameraRect.xMax = xMax;
        _camera.rect = cameraRect;
    }

    void AdjustHeight(float h, float desiredH)
    {
        Rect cameraRect = _camera.rect;
        float newSize = desiredH / h;
        float borderSize = (1 - newSize) / 2;
        float yMin = borderSize;
        float yMax = 1 - borderSize;
        cameraRect.yMin = yMin;
        cameraRect.yMax = yMax;
        _camera.rect = cameraRect;
    }
    int _oldW = 0;
    int _oldH = 0;
    // Update is called once per frame
    void Update () {
        if (Screen.width != _oldW ||
            Screen.height != _oldH)
        {
            _oldW = Screen.width;
            _oldH = Screen.height;
            AdjustViewport();
        }
    }
}
