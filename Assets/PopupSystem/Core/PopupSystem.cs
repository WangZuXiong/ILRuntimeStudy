using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PopupSystem : MonoBehaviour
{
    [SerializeField]
    private int _index = 0;
    [SerializeField]
    private int _popupMaxCount = 3;

    private readonly Dictionary<int, (GameObject, Image, Button)> _parentDict = new Dictionary<int, (GameObject, Image, Button)>();
    private readonly Dictionary<int, BasePopup> _popupDict = new Dictionary<int, BasePopup>();
    private RawImage _rawImg;
    private RenderTexture _renderTexture;
    private Camera _camera;

    private PopupSystem() { }

    public static PopupSystem Instance { get; private set; }

    /// <summary>
    /// 当前置顶的弹窗
    /// </summary>
    public BasePopup ToppingPopup
    {
        get
        {
            if (_popupDict.TryGetValue(_index - 1, out BasePopup baseWindowController))
            {
                return baseWindowController;
            }

            return null;
        }
    }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < _popupMaxCount; i++)
        {
            var parent = transform.Find("Level_" + i.ToString());
            var img = parent.GetComponent<Image>();
            var btn = parent.GetComponent<Button>();
            _parentDict.Add(i, (parent.gameObject, img, btn));
        }
        _rawImg = transform.Find("RawImage").GetComponent<RawImage>();
        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        _parentDict.Clear();
        _popupDict.Clear();
        ClearStaticBg();
    }

    public async Task<T> ShowPopup<T>(string path) where T : BasePopup
    {
        if (_index >= _popupMaxCount)
        {
            throw new Exception("弹窗过多，建议从设计上减负");
        }

        var original = Resources.Load<GameObject>(path);
        if (original == null)
        {
            throw new Exception(string.Format("path::{0} type::{1}", path, typeof(T).ToString()));
        }

        //var popup = Activator.CreateInstance<T>();

        var popup = (T)Activator.CreateInstance(typeof(T));

        var entity = Instantiate(original, transform);

        await popup.InitView0(entity.gameObject);
        await popup.InitData();
        await popup.InitView1();

        if (popup.ClearBeforeOpenWindow)
        {
            CloseAllPopup();
        }

        _parentDict[_index].Item1.SetActive(true);
        _parentDict[_index].Item2.color = popup.UseMask ? popup.MaskColor : Color.clear;
        _parentDict[_index].Item3.onClick.RemoveAllListeners();
        if (popup.CloseOnClickMask)
        {
            _parentDict[_index].Item3.onClick.AddListener(() =>
            {
                CloseCurrentPopup();
            });
        }

        if (popup.UseStaticBg && _renderTexture == null)
        {
            CreateStaticBg();
            //关闭场景相机
            if (_camera.enabled)
                _camera.enabled = false;
        }
        else
        {
            if (!_camera.enabled)
                _camera.enabled = true;
        }

        entity.transform.SetParent(_parentDict[_index].Item1.transform);
        _popupDict.Add(_index++, popup);
        return popup;
    }

    public void CloseCurrentPopup()
    {
        if (ToppingPopup != null)
        {
            if (ToppingPopup.UseStaticBg)
            {
                ClearStaticBg();
                if (!_camera.enabled)
                    _camera.enabled = true;
            }
            Destroy(ToppingPopup.gameObject);
            var temp = _index - 1;
            _parentDict[temp].Item1.SetActive(false);
            _popupDict.Remove(temp);
            _index--;
            Resources.UnloadUnusedAssets();
        }
    }

    private void CreateStaticBg()
    {
        //_renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        //_renderTexture.filterMode = FilterMode.Bilinear;
        //RenderTexture.active = _renderTexture;
        //_camera.targetTexture = _renderTexture;
        //_camera.Render();
        //_rawImg.texture = _renderTexture;
        //_rawImg.gameObject.SetActive(true);
        //RenderTexture.active = null;
        //_camera.targetTexture = null;



        _renderTexture = new RenderTexture(Screen.width / 2, Screen.height / 2, 16);
        _renderTexture.filterMode = FilterMode.Bilinear;
        RenderTexture.active = _renderTexture;
        _camera.targetTexture = _renderTexture;
        _camera.Render();
        _rawImg.texture = _renderTexture;
        _rawImg.gameObject.SetActive(true);
        RenderTexture.active = null;
        _camera.targetTexture = null;
    }

    private void ClearStaticBg()
    {
        //RenderTexture.GetTemporary这个api要和RenderTexture.ReleaseTemporary 配套使用否则会内存泄漏      
        //RenderTexture.ReleaseTemporary(_renderTexture);

        if (_renderTexture != null)
        {
            _renderTexture.Release();
            _renderTexture = null;
        }
        _renderTexture = null;
        _rawImg.texture = null;
        _rawImg.gameObject.SetActive(false);
    }

    public void CloseAllPopup()
    {
        ClearStaticBg();
        foreach (var item in _popupDict)
        {
            Destroy(item.Value.gameObject);
        }
        _popupDict.Clear();
        foreach (var item in _parentDict)
        {
            item.Value.Item1.SetActive(false);
        }
        _index = 0;
        Resources.UnloadUnusedAssets();
        if (!_camera.enabled)
            _camera.enabled = true;
    }
}
