using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public abstract class BaseUI
{
    public Transform transform { get; protected set; }

    public GameObject gameObject { get; protected set; }

    public string Path { get; protected set; }

    public BaseUI(string path)
    {
        Path = path;
    }

    //private BaseUI() { }

    //public async Task Create()
    //{
    //    var original = Resources.Load<GameObject>(_path);
    //    gameObject = GameObject.Instantiate(original);
    //    await Task.Delay(1);
    //}
}


public struct UICofig
{

}


public abstract class BasePopup : BaseUI
{
    protected BasePopup(string path) : base(path)
    {
        Path = path;
    }


    //public UICofig UICofig;

    protected Button _btnClose;
    /// <summary>
    /// 将使用遮罩背景
    /// </summary>
    public bool UseMask { get; private set; }
    /// <summary>
    /// 点击背景遮罩也能关闭弹窗
    /// </summary>
    public bool CloseOnClickMask { get; private set; }
    /// <summary>
    /// 打开改弹窗的前会关闭所有弹窗
    /// </summary>
    public bool ClearBeforeOpenWindow { get; private set; }
    /// <summary>
    /// 使用静态背景
    /// </summary>
    public bool UseStaticBg { get; private set; }
    /// <summary>
    /// 遮罩背景的颜色
    /// </summary>
    public Color MaskColor { get; private set; }

    public bool UseLifeCycle { get; private set; }


    public BasePopup SetUseMask(bool useMask)
    {
        UseMask = useMask;
        return this;
    }

    public BasePopup SetCloseOnClickMask(bool closeOnClickMask)
    {
        CloseOnClickMask = closeOnClickMask;
        return this;
    }

    public BasePopup SetClearBeforeOpenWindow(bool clearBeforeOpenWindow)
    {
        ClearBeforeOpenWindow = clearBeforeOpenWindow;
        return this;
    }

    public BasePopup SetLifeCycle(bool useLifeCycle)
    {
        UseLifeCycle = useLifeCycle;
        return this;
    }

    public void SetEntity(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }


    public virtual async Task InitView0(GameObject gameObject)
    {
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

