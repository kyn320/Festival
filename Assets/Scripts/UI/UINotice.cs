using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotice : MonoBehaviour
{ 
    public Text noticeText;

    public void View(string _notice, bool _isView)
    {
        noticeText.text = _notice;
        gameObject.SetActive(_isView);
    }
    
}
