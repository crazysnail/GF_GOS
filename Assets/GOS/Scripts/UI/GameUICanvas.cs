using System.Collections;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using GameFramework.Event;
//using GameFramework.DataTable;
using GameFramework.WebRequest;
using LitJson;

public class GameUICanvas : UIFormLogic
{

    //private const string mUIName = "Assets/GOS/Prefabs/UI/LoadUICanvas.prefab";
    private const string mConfigName = "Assets/GOS/Configs/Hero.txt";
    private const string mActorName = "Assets/GOS/Prefabs/Spine/FootMan.prefab";


    // 加载框架Event组件
    EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

    private bool mAnimation = false;
    private void OnEnable()
    {
        //OnEnableMask(false);

        Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

        Event.Subscribe(UnityGameFramework.Runtime.WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        Event.Subscribe(UnityGameFramework.Runtime.WebRequestFailureEventArgs.EventId, OnWebRequestFailure);

        //Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        //Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
    }

    private void OnDisable()
    {
        //取消订阅成功事件
        Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

        Event.Unsubscribe(UnityGameFramework.Runtime.WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
        Event.Unsubscribe(UnityGameFramework.Runtime.WebRequestFailureEventArgs.EventId, OnWebRequestFailure);

        //Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
        //Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUserInfoClick()
    {
        // 加载框架UI组件
        UIComponent UI
            = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
        //关闭大厅ui
        UIForm hallUI = UI.GetUIForm("Assets/GOS/Prefabs/UI/GameUICanvas.prefab");
        if(hallUI != null)
        {
            UI.CloseUIForm(hallUI);
        }
        
        // 加载UI
        UI.OpenUIForm("Assets/GOS/Prefabs/UI/UserInfoUICanvas.prefab", "DefaultGroup");

       // UI.OpenUIForm("Assets/Demo3/LoadUICanvas.prefab", "DefaultGroup");
    }



    public void OnLogoutClick()
    {
        //OnEnableMask(true);
        ProcedureComponent procedureComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<ProcedureComponent>();
        if (procedureComponent != null)
        {
            GameFrameworkGOS.ProcedureGame p = procedureComponent.CurrentProcedure as GameFrameworkGOS.ProcedureGame;
            if (p != null)
            {
                p.OnChange();
            }
        }
    }

    public void OnRegistClick()
    {
        // 获取框架数据表组件
        DataTableComponent DataTable = GameEntry.GetComponent<DataTableComponent>();
        // 加载配置表
        DataTable.LoadDataTable<ConfigHero>("Hero", mConfigName, this);
    }



    public void OnAnimationClick()
    {
        EntityComponent Entity = GameEntry.GetComponent<EntityComponent>();

        if (!mAnimation)
        {
            // 加载Entity
            Entity.ShowEntity<FootManLogic>(1, mActorName, "EntityGroup", this);
            Entity.ShowEntity<FootManLogic>(2, mActorName, "EntityGroup", this);
            mAnimation = true;
        }
        else
        {
            Entity.HideEntity(1);
            Entity.HideEntity(2);
            mAnimation = false;
        }

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
    }
    private void OnWebRequestFailure(object sender, GameEventArgs e)
    {
        Log.Error("OnWebRequestFailure :" + e.ToString());
    }


}