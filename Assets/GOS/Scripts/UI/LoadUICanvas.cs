using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.UI;

public class LoadUICanvas : UIFormLogic
{
    public Text mText;
    public Image mLoadBar;
    public Image mAddaptMask;
    public Transform mActor;

    private bool mInit = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(UpdateProcess());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator UpdateProcess()
    {
        for (int i = 0; i < 120; i++)
        {
            OnUpdateText(i, 120);
            yield return new WaitForEndOfFrame();
        }
        OnEnableActor(false);
        OnEnableMask(true);
    }

    public void OnUpdateText(int step, int all)
    {
        mText.text = ((step + 1) * 100 / 120).ToString() + "%";
        mLoadBar.fillAmount += (float)1 / (float)all;
    }

    public void OnEnableMask(bool enable)
    {
        mAddaptMask.gameObject.SetActive(enable);
    }

    public void OnEnableActor(bool enable)
    {
        mActor.gameObject.SetActive(enable);
    }

}