using System;  
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Event;




namespace GameFrameworkGOS
{
    public class ProcedureLoad : ProcedureBase
    {
        //private const string mUIName = "Assets/GOS/Prefabs/UI/LoadUICanvas.prefab";
        private const string mUIName = "Assets/GOS/Prefabs/UI/TestUICanvas.prefab";
        // 加载框架Event组件
        EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // 订阅成功事件
            Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);


            //EventManager.StartListening("LoadSuccess", OnLoadSuccess);
            // UI加载测速
            UILoadTest();

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner,isShutdown);


            //取消订阅成功事件
            Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            Event.Unsubscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);

            //EventManager.StopListening("LoadSuccess", OnLoadSuccess);
        }

     
     
        /// <summary>
        /// //////
        /// </summary>
        private void UILoadTest()
        {
         
            // 加载框架UI组件
            UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
            // 加载UI
            UI.OpenUIForm(mUIName, "DefaultGroup", this);

            //UI.OpenUIForm("test"+mUIName, "DefaultGroup", this);
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
                        TestUICanvas panel = (TestUICanvas)(form.Logic);
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


    }
}
