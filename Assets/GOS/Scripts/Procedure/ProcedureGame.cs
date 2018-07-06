using GameFramework;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace GameFrameworkGOS
{
    public class ProcedureGame : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

        }
    }
}


