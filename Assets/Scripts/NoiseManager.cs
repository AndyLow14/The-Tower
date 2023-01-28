using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoiseManager : MonoBehaviour
{
    public RawImage noiseTextureImage;
    public Terrain noiseTerrain;
    private float _scale;
    private float _lastScale;
    private int _seed, _lastSeed;
    public int width = 256;
    public int height = 256;
    
    private Noise _noise;
    private void Awake()
    {
        _scale = 0.1f;
        _seed = 0;
        _noise = new PerlinNoise();
        _RecomputeNoise();
    }

    private void _RecomputeNoise()
    {
        _noise.seed = _seed;
        float[,] noise = new float[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                noise[y, x] = _noise.GetNoiseMap(x, y, _scale);
            }
        }
        _SetNoiseTexture(noise);
    }

    private void _SetNoiseTexture(float[,] noise)
    {
        Color[] pixels = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                pixels[x + width * y] = Color.Lerp(Color.black, Color.white, noise[y, x]);
            }
        }
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();
        noiseTextureImage.texture = texture;

        noiseTerrain.terrainData.SetHeights(0, 0, noise);
    }
    private void _UpdateUI()
    {
        if (_scale == _lastScale && _seed == _lastSeed)
            return;

        _RecomputeNoise();

        _lastScale = _scale;
        _lastSeed = _seed;
    }

    private void OnGUI()
    {
        _scale = GUI.HorizontalSlider(new Rect(10f, 0f, 100f, 20f), _scale, 0.01f, 0.3f);
        _seed = (int) GUI.HorizontalSlider(new Rect(10f, 30f, 100f, 20f), _seed, 0, 100);

        if (GUI.changed)
            _UpdateUI();
    }

}