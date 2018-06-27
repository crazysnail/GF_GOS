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
        private int mUIID = -1;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // 加载框架Event组件
            EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

            // 加载框架UI组件
            UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();

            // 订阅UI加载成功事件
            Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess );

            // 加载UI
            mUIID = UI.OpenUIForm("Assets/GOS/Prefabs/UI/LoadUICanvas.prefab", "DefaultGroup",this );

            Log.Debug("UI load over sid=d%", mUIID);
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
                if( UI != null ){
                    
                }
                UIForm form = UI.GetUIForm(mUIID);
                if (form != null){
                    LoadUICanvas panel = (LoadUICanvas)(form.Logic);
                    if (panel != null)
                    {
                        //panel.OnEnableMask(true);
                        panel.OnEnableActor(true);
                    }
                }

            }

        }
    }
}
