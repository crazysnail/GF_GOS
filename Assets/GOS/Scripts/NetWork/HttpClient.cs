using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EEvent
{
    EE_Default = 0
}

public class Http : MonoBehaviour
{

    private string _serverUrl = "";
    private WWW _request;
    private ArrayList _cache = new ArrayList();
    private bool _isRequesting = false;
    private int _errorTime = 0;

    /**
     * 初始化
     */
    public void Awake()
    {

    }

    /**
     * Http错误处理函数
     */
    public void OnError()
    {
        if (this._errorTime > 10)
        {
            Debug.LogError("连接异常");
            return;
        }
        this._errorTime++;
        this.nextPost();
    }

    /**
     * Http超时处理函数
     */
    public void OnTimeOut()
    {
        Debug.LogError("您的请求已超时,请检查您的网络环境并重试");
    }

    /**
     * 请求数据
     * @param   url     请求地址
     * @param   type    请求消息传递类型
     * @param   data   传递的参数
     */
    public void Send(string url,   EEvent type, WWWForm data = null, bool hasMask = false)
    {

        List<object> _dict = new List<object>(4);
        _dict.Add(url);
        _dict.Add(type);
        _dict.Add(data);
        _dict.Add(hasMask);
        this._cache.Add(_dict);
        this.Post();
    }

    /**
     * 请求服务器
     */
    public void Post()
    {
        if (this._isRequesting)
        {
            return;
        }
        if (this._cache.Count <= 0)
        {
            return;
        }
        List<object> arr = this._cache[this._cache.Count - 1] as List<object>;
        string _url = (string)arr[0];
        int _type = (int)arr[1];
        WWWForm _data = (WWWForm)arr[2];
        this._isRequesting = true;
        this._cache.RemoveAt(this._cache.Count - 1);
        StartCoroutine(PostHttp(_url, _type, _data, (bool)arr[3]));
    }

    IEnumerator PostHttp(string url, int type, WWWForm data = null, bool hasMask = false)
    {
        Debug.Log(url);
        WWW www;
        if (data != null)
        {
            www = new WWW(url, data);
            yield return www;
        }
        else
        {
            www = new WWW(url);
            yield return www;
        }

        Debug.Log(www.text);
        if (www.error == null)
        {
            this._errorTime = 0;
            Dictionary<string, object> arr = null;
            if (www.text != null && www.text != "")
            {
                //do what
            }
            this.nextPost();
        }
        else
        {
            Debug.LogError("Http错误代码:" + www.error);
            this.nextPost();
        }
        //Debug.Log(transform.name);

        //////PoolCenter.GetInstance().clearObject("HttpPool", transform);
    }

    /**
     * 开始下一个请求
     */
    public void nextPost()
    {
        this._isRequesting = false;
        this.Post();
    }
}
