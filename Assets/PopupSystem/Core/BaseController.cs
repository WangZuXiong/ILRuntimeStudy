using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseUI
{

}


public struct UICofig
{
    /// <summary>
    /// 将使用遮罩背景
    /// </summary>
    public bool UseMask;
    /// <summary>
    /// 点击背景遮罩也能关闭弹窗
    /// </summary>
    public bool CloseOnClickMask;
    /// <summary>
    /// 打开改弹窗的前会关闭所有弹窗
    /// </summary>
    public bool ClearBeforeOpenWindow;
    /// <summary>
    /// 使用静态背景
    /// </summary>
    public bool UseStaticBg;
    /// <summary>
    /// 遮罩背景的颜色
    /// </summary>
    public Color MaskColor;

    public bool LifeCycle;
}


public abstract class BasePopup : BaseUI
{
    public UICofig UICofig;

    public Transform transform;

    public GameObject gameObject;

    protected Button _btnClose;

    public virtual async Task InitView0(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;

        _btnClose = GetBtnClose();
        if (_btnClose != null)
        {
            _btnClose.onClick.AddListener(OnBtnCloseClick);
        }
    }

    public virtual async Task InitData()
    {

    }

    public virtual async Task InitView1()
    {

    }



    protected virtual void Show()
    {

    }

    protected async virtual void OnBtnCloseClick()
    {
        var tweenDuration = PlayFadeOutTween();
        await Task.Delay(tweenDuration * 1000);
        Close();
    }

    protected virtual void Close()
    {
        PopupSystem.Instance.CloseCurrentPopup();

    }

    protected virtual Button GetBtnClose()
    {
        return transform.Find("BtnClose").GetComponent<Button>();
    }

    /// <summary>
    /// 淡入动画
    /// </summary>
    /// <returns>动画时长 secondsDelay</returns>
    protected virtual int PlayFadeInTween()
    {
        //tween
        return 0;
    }

    /// <summary>
    /// 淡出动画
    /// </summary>
    /// <returns>动画时长 secondsDelay</returns>
    protected virtual int PlayFadeOutTween()
    {
        //tween
        return 0;
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected abstract void AddEvent();

    /// <summary>
    /// 注销事件监听
    /// </summary>
    protected abstract void RemoveEvent();

    public virtual void Awake()
    {
        Debug.LogError("BasePopup Awake");
    }

    public virtual void OnDestroy()
    {

    }
}

