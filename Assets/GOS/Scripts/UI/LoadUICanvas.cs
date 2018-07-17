using System.Collections;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using GameFramework.Event;
//using GameFramework.DataTable;
using GameFramework.WebRequest;
using LitJson;

public class LoadUICanvas : UIFormLogic
{
    public Text mText;
    public Image mLoadBar;
    public Image mAddaptMask;
    public Transform mActor;

    private const string mUIName = "Assets/GOS/Prefabs/UI/LoadUICanvas.prefab";
    private const string mConfigName = "Assets/GOS/Configs/Hero.txt";
    private const string mActorName = "Assets/GOS/Prefabs/Spine/FootMan.prefab";


    // 加载框架Event组件
    EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

    private bool mAnimation = false;
    private void OnEnable()
    {
        OnEnableMask(false);

        Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

        Event.Subscribe(UnityGameFramework.Runtime.WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        Event.Subscribe(UnityGameFramework.Runtime.WebRequestFailureEventArgs.EventId, OnWebRequestFailure);

        Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
    }

    private void OnDisable()
    {
        //取消订阅成功事件
        Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

        Event.Unsubscribe(UnityGameFramework.Runtime.WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        Event.Unsubscribe(UnityGameFramework.Runtime.WebRequestFailureEventArgs.EventId, OnWebRequestFailure);

        Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(UpdateProcess());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator UpdateProcess()
    {
        for (int i = 0; i < 120; i++)
        {
            OnUpdateText(i, 120);
            yield return new WaitForEndOfFrame();
        }

        //OnEnableMask(true);
        OnEnterGame();
    }

    public void OnUpdateText(int step, int all)
    {
        mText.text = ((step + 1) * 100 / 120).ToString() + "%";
        mLoadBar.fillAmount += (float)1 / (float)all;
    }

    public void OnEnableMask(bool enable)
    {
        mLoadBar.fillAmount = 0;
        mAddaptMask.gameObject.SetActive(enable);
    }

    public void OnEnableActor(bool enable)
    {
        mActor.gameObject.SetActive(enable);
    }

    public void OnEnterGame()
    {
        ProcedureComponent procedureComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
        if (procedureComponent != null)
        {
            GameFrameworkGOS.ProcedureLoad p = procedureComponent.CurrentProcedure as GameFrameworkGOS.ProcedureLoad;
            if (p != null)
            {
                p.OnChange();
            }
        }
    }

    public void OnConfigClick()
    {
        // 获取框架数据表组件
        DataTableComponent DataTable = GameEntry.GetComponent<DataTableComponent>();
        // 加载配置表
        DataTable.LoadDataTable<ConfigHero>("Hero", mConfigName, this);
        DataTable.LoadDataTable<ConfigHero>("Hero", "ddddd", this);
    }

    public void OnAnimationClick()
    {
        EntityComponent Entity = GameEntry.GetComponent<EntityComponent>();

        if (!mAnimation){
            // 加载Entity
            Entity.ShowEntity<FootManLogic>(1, mActorName, "EntityGroup", this);
            Entity.ShowEntity<FootManLogic>(2, mActorName, "EntityGroup", this);
            mAnimation = true;
        }
        else{
            Entity.HideEntity(1);
            Entity.HideEntity(2);
            mAnimation = false;
        }

    }

    public void OnNetClick()
    {
        
        // 开了代理会失败！
        WebRequestComponent WebRequest = GameEntry.GetComponent<WebRequestComponent>();

        //get
        //string url = "http://www.gameframework.cn/starforce/version.txt";
        //string url = "http://localhost:9091/main.go";           
        //string url = "http://localhost:9091/";
        //WebRequest.AddWebRequest(url, this);

        string url = "http://localhost:18810/";
        //string str = "{\"UserName\":{\"kitty\"}}";
        //byte[] content = System.Text.Encoding.ASCII.GetBytes(str);
        //WebRequest.AddWebRequest(url, content, this);

        for (int i = 0; i < 1000; i++)
        {
            JsonData data = new JsonData();
            data["UserName"] = "seed"+i;
            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(data.ToJson());
            WWWForm Form = new WWWForm();
            WebRequest.AddWebRequest(url, postBytes, Form, 1, this);
        }
    }


    public void OnNet2Click()
    {
        // 开了代理会失败！
        WebRequestComponent WebRequest = GameEntry.GetComponent<WebRequestComponent>();
        string url = "http://localhost:18810/";
        JsonData data = new JsonData();
        data["UserName"] = "seed9999999999999999999999";
        byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(data.ToJson());
        WWWForm Form = new WWWForm();
        WebRequest.AddWebRequest(url, postBytes, Form, 1, this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
        // 判断userData是否为自己
        if (ne.UserData != this)
        {
            return;
        }
        if (ne.Entity.Id == 1)
        {
            ne.Entity.gameObject.transform.SetLocalPositionX(-3);
        }
        else if (ne.Entity.Id == 2)
        {
            ne.Entity.gameObject.transform.SetLocalPositionX(3);
        }
        else
        {
            Log.Error("OnShowEntitySuccess Entity id is Unkown ! " + ne.Entity.Id);
        }
    }
    private void OnShowEntityFailure(object sender, GameEventArgs e)
    {
        Log.Error("OnShowEntityFailure :" + e.ToString());
    }

    /// <summary>
    /// //////
    /// </summary>
    private void OnWebRequestSuccess(object sender, GameEventArgs e)
    {
        UnityGameFramework.Runtime.WebRequestSuccessEventArgs ne = (UnityGameFramework.Runtime.WebRequestSuccessEventArgs)e;
        // 获取回应的数据
        string responseJson = GameFramework.Utility.Converter.GetString(ne.GetWebResponseBytes());
        Log.Warning("OnWebRequestSuccess：" + responseJson);

        ////post
        //if (responseJson != "404 Not found")
        //{
        //    WebRequestComponent WebRequest = UnityGameFramework.Runtime.GameEntry.GetComponent<WebRequestComponent>();
        //    string url = "http://localhost:9091/";
        //    responseJson = "{\"UserName\": \"kitty\"}";
        //    byte[] content = System.Text.Encoding.Default.GetBytes(responseJson);
        //    WebRequest.AddWebRequest(url, content, this);
        //}
        //else
        //{
        //    //
        //}
    }
    private void OnWebRequestFailure(object sender, GameEventArgs e)
    {
        Log.Error("OnWebRequestFailure :" + e.ToString());
    }


    /// <summary>
    /// /////////////////
    /// </summary>

    private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
    {
        // 获取框架数据表组件
        DataTableComponent DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
        // 获得数据表
        GameFramework.DataTable.IDataTable<ConfigHero> table = DataTable.GetDataTable<ConfigHero>();

        // 获得所有行
        ConfigHero[] rows = table.GetAllDataRows();
        Log.Debug("ConfigHeros:" + rows.Length);

        if (table != null)
        {
            // 根据行号获得某一行
            ConfigHero row = table.GetDataRow(1); // 或直接使用 dtScene[1]
                                                  // 此行存在，可以获取内容了
            string name = table.Name;
            int hp = row.Hp;
            Log.Debug("name:" + name + ", hp:" + hp);
        }
        else
        {
            // 此行不存在
        }
        // 获得满足条件的所有行
        ConfigHero[] drScenesWithCondition = table.GetAllDataRows(x => x.Id > 0);

        // 获得满足条件的第一行
        ConfigHero drSceneWithCondition = table.GetDataRow(x => x.Name == "mutou");

    }
    private void OnLoadDataTableFailure(object sender, GameEventArgs e)
    {
        Log.Error("OnLoadDataTableFailure :" + e.ToString());
    }

}