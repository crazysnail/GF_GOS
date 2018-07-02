using System;  
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Event;
using GameFramework.DataTable;
using UnityEngine.Networking;



namespace GameFrameworkGOS
{
    public class ProcedureLoad : ProcedureBase
    {
        private const string mUIName = "Assets/GOS/Prefabs/UI/LoadUICanvas.prefab";
        private const string mConfigName = "Assets/GOS/Configs/Hero.txt";
        private const string mActorName = "Assets/GOS/Prefabs/Spine/FootMan.prefab";

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // UI加载测速
            UILoadTest();
            // 加载表格
            ConfigLoadTest();
            // 加载实体
            EntityLoadTest();
            // web消息
            WebRequestTest();

        }
        /// <summary>
        /// //////
        /// </summary>
        private void WebRequestTest()
        {

            EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            Event.Subscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
            Event.Subscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
			//
            // 开了代理会失败！
            // 获取框架网络组件
            WebRequestComponent WebRequest = UnityGameFramework.Runtime.GameEntry.GetComponent<WebRequestComponent>();
            string url = "http://www.gameframework.cn/starforce/version.txt";
           
            WebRequest.AddWebRequest(url, this);
        }


        private void OnWebRequestSuccess(object sender, GameEventArgs e)
        {
            WebRequestSuccessEventArgs ne = (WebRequestSuccessEventArgs)e;
            // 获取回应的数据
            string responseJson = GameFramework.Utility.Converter.GetString(ne.GetWebResponseBytes());
            Log.Warning("OnWebRequestSuccess：" + responseJson);
        }
        private void OnWebRequestFailure(object sender, GameEventArgs e)
        {
            Log.Error("OnWebRequestFailure :" + e.ToString());
        }

        /// <summary>
        /// //////
        /// </summary>
        private void EntityLoadTest()
        {
            // 加载框架Event组件
            EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

            // 加载框架UI组件
            EntityComponent Entity = UnityGameFramework.Runtime.GameEntry.GetComponent<EntityComponent>();

            // 订阅UI加载成功事件
            Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

            // 加载Entity
            Entity.ShowEntity<FootManLogic>(1, mActorName, "EntityGroup", this);
            Entity.ShowEntity<FootManLogic>(2, mActorName, "EntityGroup", this);
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            // 判断userData是否为自己
            if (ne.UserData != this){
                return;
            }
            if (ne.Entity.Id == 1){
                ne.Entity.gameObject.transform.SetLocalPositionX(-3);
            }
            else if (ne.Entity.Id == 2){
                ne.Entity.gameObject.transform.SetLocalPositionX(3);
            }
            else{
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
        private void UILoadTest()
        {
            // 加载框架Event组件
            EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

            // 加载框架UI组件
            UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();

            // 订阅UI加载成功事件
            Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);

            // 加载UI
            UI.OpenUIForm(mUIName, "DefaultGroup", this);

            //Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            //Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);
            UI.OpenUIForm("test"+mUIName, "DefaultGroup", this);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            // 判断userData是否为自己
            if (ne.UserData != this){
                return;
            }
            if( sender != null ){
                UIComponent UI = sender as UIComponent;
                if (UI != null){
                    UIForm form = UI.GetUIForm(mUIName);
                    if (form != null){
                        LoadUICanvas panel = (LoadUICanvas)(form.Logic);
                        if (panel != null){
                            //panel.OnEnableMask(true);
                            //panel.OnEnableActor(true);
                        }
                    }
                }
            }

        }
        private void OnOpenUIFormFailure(object sender, GameEventArgs e)
        {
            Log.Error("OnOpenUIFormFailure :"+e.ToString());
        }

        /// <summary>
        /// /////////////////
        /// </summary>

        private void ConfigLoadTest()
        {
            // 获取框架事件组件
            EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            // 获取框架数据表组件
            DataTableComponent DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
            // 订阅加载成功事件
            Event.Subscribe(UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            Event.Subscribe(UnityGameFramework.Runtime.LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            // 加载配置表
            DataTable.LoadDataTable<ConfigHero>("Hero", mConfigName,this);
            DataTable.LoadDataTable<ConfigHero>("Hero", "ddddd", this);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            // 获取框架数据表组件
            DataTableComponent DataTable = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();
            // 获得数据表
            IDataTable<ConfigHero> table = DataTable.GetDataTable<ConfigHero>();

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
}
