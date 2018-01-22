using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRadialMenu : MonoBehaviour
{

    RectTransform tr;
    Image image;

    public List<Button> buttonList;

    public float radiusSize = 50f;
    public float speed = 10f;

    [SerializeField]
    bool isView = false;

    void Awake()
    {
        tr = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public virtual void View(bool _isView)
    {
        if (!isView && GameManager.instance.isOnMenu)
            return;

        if (!_isView)
        {
            HideAnimate();
            return;
        }
        else
        {
            isView = true;
            GameManager.instance.isOnMenu = true;
            PlayDataManager.instance.player.SetStop(true);
        }

        SetRayCast(false);

        for (int i = 0; i < buttonList.Count; ++i)
        {
            float rad = (Mathf.PI * 2 / buttonList.Count) * i;
            float x = Mathf.Sin(rad);
            float y = Mathf.Cos(rad);
            buttonList[i].transform.localPosition = new Vector3(x, y, 0f) * radiusSize;
            buttonList[i].gameObject.SetActive(true);
        }

        ViewAnimate();
    }

    void Animate(int _flow, UnityAction _action)
    {
        if (animateView != null)
        {
            StopCoroutine(animateView);
        }

        animateView = StartCoroutine(AnimateView(_flow, _action));
    }

    Coroutine animateView = null;

    IEnumerator AnimateView(int _flow, UnityAction _action)
    {
        tr.localScale = Vector3.zero;
        float timer = 0f;
        while (timer < (1 / speed))
        {
            timer += Time.deltaTime;
            if (_flow < 0)
                tr.localScale = Vector3.one - Vector3.one * timer * speed;
            else
                tr.localScale = Vector3.one * timer * speed;

            yield return null;
        }

        if (_flow < 0) {
            tr.localScale = Vector3.zero;
        }

        if (_action != null)
        {
            _action.Invoke();
        }

        animateView = null;
    }


    public virtual void ViewAnimate()
    {
        Animate(1, null);
    }

    public virtual void ViewAnimate(UnityAction _actoin)
    {
        Animate(1, _actoin);
    }

    public virtual void HideAnimate()
    {
        Animate(-1, HideButton);
    }

    public virtual void HideAnimate(UnityAction _actoin)
    {
        Animate(-1, _actoin);
    }

    protected virtual void HideButton()
    {
        for (int i = 0; i < buttonList.Count; ++i)
        {
            buttonList[i].gameObject.SetActive(false);
        }
        MenuOut();
    }

    protected virtual void MenuOut() {
        GameManager.instance.isOnMenu = false;
        isView = false;
        PlayDataManager.instance.player.SetStop(false);

        SetRayCast(true);
    }

    public virtual void OnExit()
    {
        View(false);
    }

    public void SetRayCast(bool _isWork)
    {
        if (image == null)
            return;

        image.raycastTarget = _isWork;
    }

    protected void SetScale(Vector3 _scale) {
        tr.localScale = _scale;
    }

}
