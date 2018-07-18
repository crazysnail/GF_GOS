using System.Collections;
using GameFramework;

using UnityGameFramework.Runtime;
using UnityEngine.UI;
public class MyWalletUICanvas : UIFormLogic
{
    public Image ModifyInfo;
    public Image OverviewBoundInfo;
    public Image OverviewInfo;
    public InputField InputSetCode;


    // Use this for initialization
    void Start () {
        OverviewInfo.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
       // print(">>>>>>>>>>>>>>>>>>>>>>>>>zhege =====" + InputSetCode.text);
    }

    public void OnHelpClick()
    {

    }

    public void OnOverviewClick()
    {
        ModifyInfo.gameObject.SetActive(false);
        OverviewInfo.gameObject.SetActive(true);
        print(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" + ModifyInfo.flexibleWidth);
    }
    public void OnModifyClick()
    {
        ModifyInfo.gameObject.SetActive(true);
        OverviewInfo.gameObject.SetActive(false);
    }

    public void OnImportClick()
    {

        print(">>>>>>>>>>>>>>>>>>>>>>>>>zhege =====" + InputSetCode.text);
        print(">>>>>>>>>>>>>>>>>------OnImportClick---------------------------");
    }
   
}
