using GameFramework;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameFrameworkGOS
{
    public class ProcedureMainMenu : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            SceneComponent scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
            // 切换场景
            scene.LoadScene("MainMenu", this);
            // 切换流程
            ChangeState<ProcedureMainMenu>(procedureOwner);
        }
    }
}


