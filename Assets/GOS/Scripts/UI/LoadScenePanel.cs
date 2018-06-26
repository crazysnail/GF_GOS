using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadScenePanel : MonoBehaviour
{
    public Text  mText;
    public Image mLoadbar;
    public Image mAddaptMask;

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
            mLoadbar.fillAmount += (float)1 / 120f;
            mText.text = ((i + 1) * 100 / 120).ToString() + "%";

            //小细节就是从0开始到99就跳转了，从1开始正好百分之百

            yield return new WaitForEndOfFrame();
        }

        mAddaptMask.gameObject.SetActive(true);
    }
}