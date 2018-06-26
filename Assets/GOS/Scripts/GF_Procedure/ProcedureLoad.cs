using GameFramework;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameFrameworkGOS
{
    public class ProcedureLoad : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);


            SceneComponent scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
            // 切换场景
            scene.LoadScene("LoadScene", this);
            // 切换流程
            //ChangeState<ProcedureLoad>(procedureOwner);

            // 加载框架UI组件
            UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();

            // 加载UI
            UI.OpenUIForm("Assets/Prefabs/UI/AdaptMask.prefab", "DefaultGroup");
        }
    }
}
