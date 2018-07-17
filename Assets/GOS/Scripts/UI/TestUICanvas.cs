using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;

public class TestUICanvas : UIFormLogic {


    public Text mtext;

    private GameObject buttonObj;
    public Image overview_panel;
    public Image modify_panel;
    public Button overview_btn;
    public Button modify_btn;
    public InputField set_password_field;

    // private GameObject modify_btn;
    // Use this for initialization
    void Start() {
        // buttonObj = GameObject.Find("overview_btn");
        // modify_btn = GameObject.Find("modify_btn");
        //buttonObj.GetComponent<Button>().onClick.AddListener(OnOvervierClick);
        // modify_panel.gameObject.SetActive(false);

       var phrase_field = modify_panel.transform.Find("phrase_field").gameObject;
        foreach(Transform child in transform)
        {

        }

    }

    // Update is called once per frame
    void Update() {


    }
    public void OnOvervierClick()
    {
        print("-----------OnOvervierClick-----------------");
        modify_panel.gameObject.SetActive(true);
        // overview_panel.gameObject.SetActive(false);

    }

    public void OnModifyClick()
    {
        print("-----------OnModifyClick-----------------");
    }

    public void OnRechargeClick()
    {
        print("-----------OnRechargeClick-----------------");
    }

    public void OnCashClick()
    {
        print("-----------OnCashClick-----------------");
    }


    public void OnLogoutClick()
    {
        print("-----------OnLogoutClick-----------------");
    }

}
