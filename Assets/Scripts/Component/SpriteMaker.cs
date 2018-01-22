using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//bug sprite save >> load work, rgba format error
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMaker : MonoBehaviour
{
    public static SpriteMaker instance;

    public UISpriteMaker ui;

    public Transform cam;

    public Texture2D signTex;
    SpriteRenderer sprRender;

    public Vector2 margin;

    Texture2D tex;
    Color[] colorArray;

    public GameObject pixelGridPrefab;
    SpritePixelGrid[] pixelGridArray;

    void Awake()
    {
        instance = this;
        sprRender = GetComponent<SpriteRenderer>();
        SpritePixelGrid.spriteMaker = this;
    }

    // Use this for initialization
    void Start()
    {
        ui.ViewList(true, false);
    }

    public void CreateFile(Sprite _spr)
    {
        signTex = _spr.texture;
        MakeTexture(false);
        MakeSprite(_spr.name);
    }

    public void CreateFile(string _fileName)
    {
        MakeTexture(true);
        MakeSprite(_fileName);
    }

    void MakeTexture(bool _isWhite)
    {
        bool setWhite = _isWhite;

        if (signTex == null)
        {
            tex = new Texture2D(96, 32, TextureFormat.RGBA32, false);
            colorArray = new Color[tex.width * tex.height];
            setWhite = true;
        }
        else
        {
            tex = signTex;
            colorArray = tex.GetPixels();
            setWhite = false;
        }

        cam.position = new Vector2(Mathf.RoundToInt((tex.width + (tex.width * margin.x) - margin.x * 2) * 0.5f), (tex.height + (tex.height * margin.y) - margin.y * 2) * 0.5f);

        if (pixelGridArray == null)
            pixelGridArray = new SpritePixelGrid[tex.width * tex.height];

        for (int x = 0; x < tex.width; ++x)
        {
            for (int y = 0; y < tex.height; ++y)
            {
                int pixelIndex = x + (y * tex.width);
                if (setWhite)
                {
                    colorArray[pixelIndex] = Color.white;
                }

                if (pixelGridArray[pixelIndex] == null)
                    pixelGridArray[pixelIndex] = Instantiate(pixelGridPrefab,
                        new Vector3(x + margin.x * x, y + margin.y * y, 0),
                        Quaternion.identity, transform).GetComponent<SpritePixelGrid>();

                pixelGridArray[pixelIndex].SetColor(pixelIndex, colorArray[pixelIndex]);
            }
        }

        tex.SetPixels(colorArray);
        tex.Apply();

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;
    }

    void MakeSprite(string _fileName)
    {
        Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f, 32);

        if (signTex)
        {
            spr.name = signTex.name;
        }
        else if (_fileName != null)
        {
            spr.name = spr.texture.name = _fileName;
            signTex = spr.texture;
        }
        else
        {
            spr.name = spr.texture.name = "Sign" + Random.Range(0, 10000);
            signTex = spr.texture;
        }

        SaveSpriteToEditorPath(spr, "Assets/Resources/Sign/" + spr.name + ".png");
    }

    public void UpdateColor(int _index, Color _color)
    {
        colorArray[_index] = _color;
    }

    public void Save()
    {
        tex.SetPixels(colorArray);
        tex.Apply();

        MakeSprite(null);
    }

    Sprite SaveSpriteToEditorPath(Sprite sp, string path)
    {
        string dir = Path.GetDirectoryName(path);

        Directory.CreateDirectory(dir);

        File.WriteAllBytes(path, sp.texture.EncodeToPNG());
        AssetDatabase.Refresh();
        AssetDatabase.AddObjectToAsset(sp, path);
        AssetDatabase.SaveAssets();

        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;

        ti.spritePixelsPerUnit = sp.pixelsPerUnit;
        ti.wrapMode = sp.texture.wrapMode;
        ti.filterMode = sp.texture.filterMode;
        ti.textureCompression = TextureImporterCompression.Uncompressed;
        ti.textureFormat = TextureImporterFormat.RGBA32;
        ti.mipmapEnabled = false;
        ti.isReadable = true;

        EditorUtility.SetDirty(ti);
        ti.SaveAndReimport();

        return AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
    }
}
