using System.Collections;
using GameFramework;

using UnityGameFramework.Runtime;
using UnityEngine.UI;



public class UserInfoUICanvas : UIFormLogic
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMyWalletClick()
    {
        print(">>>>>>>>>>>>>>>>>>OnMyWalletClick>>>>>>>>>>>>>>>>>>>>>>>>");

        // 加载框架UI组件
        UIComponent UI = UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();
        UIForm userUI = UI.GetUIForm("Assets/GOS/Prefabs/UI/UserInfoUICanvas.prefab");
        if (userUI != null)
        {
            UI.CloseUIForm(userUI);
        }
       
        UI.OpenUIForm("Assets/GOS/Prefabs/UI/MyWalletUICanvas.prefab", "DefaultGroup");
        print(">>>>>>>>>>>>>");
    }










}