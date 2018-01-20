using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//bug sprite save >> load work, rgba format error
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMaker : MonoBehaviour
{
    public Transform cam;

    public Texture2D signTex;
    SpriteRenderer sprRender;

    Texture2D tex;
    Color[] colorArray;

    public GameObject pixelGridPrefab;
    SpritePixelGrid[] pixelGridArray;

    void Awake()
    {
        sprRender = GetComponent<SpriteRenderer>();
        SpritePixelGrid.spriteMaker = this;
    }

    // Use this for initialization
    void Start()
    {
        MakeTexture();

        MakeSprite();
    }

    void MakeTexture()
    {
        bool setWhite;

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

        cam.position = new Vector2(tex.width * 0.5f, tex.height * 0.5f);

        print("total size = " + tex.width * tex.height);
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

                pixelGridArray[pixelIndex] = Instantiate(pixelGridPrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity, transform).GetComponent<SpritePixelGrid>();

                pixelGridArray[pixelIndex].SetColor(pixelIndex, colorArray[pixelIndex]);
            }
        }

        tex.SetPixels(colorArray);
        tex.Apply();

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;
    }

    void MakeSprite()
    {
        Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f, 32);
        sprRender.sprite = spr;
        spr.name = "test1";
        SaveSpriteToEditorPath(spr, "Assets/Resource/" + spr.name + ".png");
    }

    public void UpdateColor(int _index, Color _color)
    {
        colorArray[_index] = _color;
    }

    public void Save()
    {
        tex.SetPixels(colorArray);
        tex.Apply();

        MakeSprite();
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
