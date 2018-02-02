using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISpriteMaker : MonoBehaviour
{
    public SpriteMakerBrush brush;

    public UIColorPicker colorPicker;

    public Text modeText;

    public Button[] buttons;
    public bool[] buttonWorks;

    public Text brushSizeText;
    public Slider brushSizeSlider;

    public GameObject background;
    public UISpriteListView listView;

    public InputField nameSetField;
    public GameObject createNameSet;

    public Image spoidColor;

    void Awake()
    {
        UISpriteListView.ui = this;
    }

    public void OnViewList(bool _isView)
    {
        ViewList(_isView, true);
    }

    public void ViewList(bool _isView, bool _isExit)
    {
        background.SetActive(_isView);
        listView.ViewList(_isView, _isExit);
    }

    public void OnColorPicker()
    {
        buttonWorks[2] = !buttonWorks[2];
        colorPicker.Color = brush.brushColor;
        colorPicker.SetOnValueChangeCallback(ChangeColor);
        colorPicker.gameObject.SetActive(buttonWorks[2]);
    }

    public void ChangeColor(Color _color)
    {
        brush.ChangeColor(_color);
    }

    public void OnBrushSize()
    {
        buttonWorks[1] = !buttonWorks[1];
        brushSizeSlider.gameObject.SetActive(buttonWorks[1]);
        brushSizeSlider.value = brush.burshSize;
        brushSizeText.text = brushSizeSlider.value.ToString();
    }

    public void ChangeBrushSize(float size)
    {
        brushSizeText.text = size.ToString();
        brush.ChangeSize(size);
    }

    public void OnEraser()
    {
        if (brush.eraserMode)
        {
            modeText.text = "Pen Mode";
            brush.eraserMode = false;
            buttons[0].gameObject.transform.GetChild(0).GetComponent<Text>().text = "Eraser";
        }
        else {
            modeText.text = "Eraser Mode";
            brush.eraserMode = true;
            buttons[0].gameObject.transform.GetChild(0).GetComponent<Text>().text = "Pen";
        }
    }

    public void OnSpoid()
    {
        if (brush.spoidMode)
        {
            spoidColor.gameObject.SetActive(false);
            brush.spoidMode = false;
        }
        else
        {
            brush.spoidMode = true;
            spoidColor.gameObject.SetActive(true);
            brush.spoidAction = ChangeSpoidColor;
        }
    }

    public void ChangeSpoidColor(Color _color)
    {
        spoidColor.color = _color;
    }

    public void OnExit()
    {
        SceneManager.LoadScene("scene", LoadSceneMode.Single);
    }

    public void OnNewFile(bool _isView)
    {
        createNameSet.SetActive(_isView);

        if (SpriteMaker.instance.signTex != null)
            nameSetField.text = SpriteMaker.instance.signTex.name;
    }

    public void OnCreateFile()
    {
        if (nameSetField.text == "")
            return;

        if (SpriteMaker.instance.signTex != null)
        {
            print("save");
            SpriteMaker.instance.SaveFile(nameSetField.text);
        }
        else
        {
            print("create");
            SpriteMaker.instance.CreateFile(nameSetField.text);
        }

        OnViewList(false);
        OnNewFile(false);
    }



}
