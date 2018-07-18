using GameFramework;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Event;
using UnityEngine.SceneManagement;

namespace GameFrameworkGOS
{
    public class ProcedureGame : ProcedureBase
    {
        private const string mUIName = "Assets/GOS/Prefabs/UI/GameUICanvas.prefab";
        private const string mSceneName = "GameScene";

        // 加载框架Event组件
        EventComponent Event = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

        ProcedureOwner mProcedureOwner = null;
        private int mUIId = -1;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            mProcedureOwner = procedureOwner;

            // 切换场景           
            SceneComponent sceneComp = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
            if( sceneComp.SceneIsLoaded(mSceneName)){
                Scene scene = SceneManager.GetSceneByName(SceneComponent.GetSceneName(mSceneName));
                if (!scene.IsValid())
                {
                    Log.Error("Loaded scene '{0}' is invalid.", mSceneName);
                    return;
                }
                SceneManager.SetActiveScene(scene);
            }else{
                sceneComp.LoadScene(mSceneName, this);
            }

            // 订阅成功事件
            Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);

            UILoadTest();

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
            UI.CloseUIForm(mUIId);

            //取消订阅成功事件
            Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            Event.Unsubscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);

        }

        public void OnChange()
        {
            ChangeState<ProcedureLogin>(mProcedureOwner);
        }


     
        /// <summary>
        /// //////
        /// </summary>
        private void UILoadTest()
        {
            UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
            mUIId = UI.OpenUIForm(mUIName, "DefaultGroup", this);
        }


        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            // 判断userData是否为自己
            if (ne.UserData != this)
            {
                return;
            }
            if (sender != null)
            {
                UIComponent UI = sender as UIComponent;
                if (UI != null)
                {
                    UIForm form = UI.GetUIForm(mUIName);
                    if (form != null)
                    {
                        GameUICanvas panel = (GameUICanvas)(form.Logic);
                        if (panel != null)
                        {
                            //panel.OnEnableMask(true);
                            //panel.OnEnableActor(true);
                        }
                    }
                }
            }

        }
        private void OnOpenUIFormFailure(object sender, GameEventArgs e)
        {
            Log.Error("OnOpenUIFormFailure :" + e.ToString());
        }
    }
}


