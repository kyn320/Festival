using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//bug sprite save >> load work, rgba format error
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteMaker : MonoBehaviour
{
    public Texture2D signTex;
    SpriteRenderer sprRender;

    Texture2D tex;
    Color32[] colorArray;

    void Awake()
    {
        sprRender = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        MakeTexture();

        MakeSprite();
    }


    void MakeTexture()
    {
        if (signTex == null)
        {
            tex = new Texture2D(96, 32, TextureFormat.RGBA32, false);
            colorArray = new Color32[tex.width * tex.height];

            for (int x = 0; x < tex.width; ++x)
            {
                for (int y = 0; y < tex.height; ++y)
                {
                    int pixelIndex = x + (y * tex.width);
                    colorArray[pixelIndex] = Color.white;
                }
            }
            print(colorArray[0]);
        }
        else
        {
            print("is not null");
            tex = signTex;
            colorArray = tex.GetPixels32();
            print(colorArray[0]);
        }

        tex.SetPixels32(colorArray);
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
        ti.mipmapEnabled = false;
        ti.isReadable = true;

        EditorUtility.SetDirty(ti);
        ti.SaveAndReimport();

        return AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
    }


}
