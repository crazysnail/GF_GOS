using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;

public class LoadPanel : MonoBehaviour
{
    public Text text;
    public Image loadbar;

    // Use this for initialization
    //void Start()
    //{
    //
    //}

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator Start()
    {
        for (int i = 0; i < 120; i++)
        {
            loadbar.fillAmount += (float)1 / 120f;
            text.text = ((i + 1) * 100 / 120).ToString() + "%";

            //小细节就是从0开始到99就跳转了，从1开始正好百分之百

            yield return new WaitForEndOfFrame();
        }



        // 卸载所有场景
       //string[] loadedSceneAssetNames = Scene.GetLoadedSceneAssetNames();
       //for (int i = 0; i < loadedSceneAssetNames.Length; i++)
       //{
       //    Scene.UnloadScene(loadedSceneAssetNames[i]);
       //}


        SceneComponent scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
        // 切换场景
        scene.LoadScene("MainMenu", this);
        // 切换流程
        //ChangeState<ProcedureMainMenu>(procedureOwner);

    }
}