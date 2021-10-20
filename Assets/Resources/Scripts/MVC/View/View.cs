using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : Singleton<View>
{
    // [HideInInspector]//金币和宝石的显示
    public Text coinNumber;
    public Text diamondNumber;
    
    public GameObject[] skillObjects;
    private int[] _skillStatus;
    
    //CD Images
    public Image[] lockImages;
    
    //基础技能图片和技能图片
    // public Image[] skillImages;
    // public Image[] _basicskillObjects;//基础攻击的CD图片

    private new void Awake()
    {
        skillObjects = new GameObject[4];
        _skillStatus = new int[4];
        lockImages = new Image[4];//Image数组使用需要初始化！
        // skillImages = new Image[3];
        // _basicskillObjects = new Image[3];
    }

    private void Start()
    {
        OpenSkillObjects();
        FindLockImage();
        LockImageStatus();
        //金币
        Model.GetInstance().OnCoinNumberChange += SetCoinNumber;
        coinNumber.text = Model.GetInstance().CoinNumber.ToString();
        //钻石
        Model.GetInstance().OnDiamondNumberChange += SetDiamondNumber;
        diamondNumber.text = 0.ToString();

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 显示金币的数量给用户看
    /// </summary>
    /// <param name="number"></param>
    private void SetCoinNumber(int number)
    {
        if (coinNumber == null) return;
        coinNumber.text = number.ToString();
        // Debug.Log("222");
        // coinNumber.text = number.ToString();
    }

    /// <summary>
    /// 显示宝石的数量给用户看
    /// </summary>
    /// <param name="number"></param>
    private void SetDiamondNumber(int number)
    {
        if (diamondNumber == null) return;
        diamondNumber.text = number.ToString();
    }

    #region 读取字典 判断技能购买状态

    /// <summary>
    /// 如果bool为1。那么就挂脚本 否则就不挂
    /// 顺便查找挂在的物体
    /// </summary>
    private void OpenSkillObjects()
    {
        GetSkillStatus();
        skillObjects[0] = GameObject.Find("Player Controller/Skill01").gameObject;
        skillObjects[1] = GameObject.Find("Player Controller/Skill02").gameObject;
        skillObjects[2] = GameObject.Find("Player Controller/Skill03").gameObject;
        skillObjects[3] = GameObject.Find("Player Controller/Skill04").gameObject;
        // skillObjects[0].GetComponent<Skill01>().enabled = _skillStatus[0] == 1;
        // skillObjects[1].GetComponent<Skill02>().enabled = _skillStatus[1] == 1;
        // skillObjects[2].GetComponent<Skill03>().enabled = _skillStatus[2] == 1;
        // skillObjects[3].GetComponent<Skill04>().enabled = _skillStatus[3] == 1;
        //i == 1 --> true || i==0 --> false
    }

    /// <summary>
    /// 直接从字典里拿bool判断是否已经购买技能
    /// </summary>
    private void GetSkillStatus()
    {
        for (var i = 0; i < LoadData.SK.Count; i++)
        {
            _skillStatus[i] = Convert.ToInt32(LoadData.SK[i]);
        }
    }

    #endregion

    /// <summary>
    /// 查找面板中的锁定图片
    /// </summary>
    private void FindLockImage()
    {
        var go = GameObject.Find("Basic Panel");
        lockImages[0] = go.transform.Find("Skill01").GetChild(1).GetChild(1).GetComponent<Image>();
        lockImages[1] = go.transform.Find("Skill02").GetChild(1).GetChild(1).GetComponent<Image>();
        lockImages[2] = go.transform.Find("Skill03").GetChild(1).GetChild(1).GetComponent<Image>();
        lockImages[3] = go.transform.Find("Skill04").GetChild(1).GetChild(1).GetComponent<Image>();
    }
    
    /// <summary>
    /// 通过读取Json，写入字典的操作来控制锁定图片是否显示
    /// </summary>
    private void LockImageStatus()
    {
        for (var i = 0; i < lockImages.Length; i++)
        {
            if (LoadData.SK.TryGetValue(i,out var temp))
            {
                switch (temp)
                {
                    case true:
                        lockImages[i].gameObject.SetActive(false);
                        break;
                    case false:
                        continue;
                }
            }
        }
    }
    
    /// <summary>
    /// 将特定的技能开启！
    /// </summary>
    // public void JudgeSkillStatus(int id)
    // {
    //     switch (id-1)
    //     {
    //         case 0:
    //             if (skillObjects[0] != null)
    //             {
    //                 skillObjects[0] = GameObject.Find("Player Controller/Skill01").gameObject;
    //                 skillObjects[0].GetComponent<Skill01>().enabled = true;
    //                 return;
    //             }
    //             Debug.Log("skill 01 null");
    //
    //             break;
    //         case 1:
    //             skillObjects[id-1].GetComponent<Skill02>().enabled = true;
    //             break;
    //         case 2:
    //             skillObjects[id-1].GetComponent<Skill03>().enabled = true;
    //             break;
    //         case 3:
    //             skillObjects[id-1].GetComponent<Skill04>().enabled = true;
    //             break;
    //     }
    // }
    
    // private void FindSkillImagesInHierarchy()
    // {
    //     var go = GameObject.Find("Basic Panel");
    //     // Debug.Log("111");
    //     skillImages[0] = go.transform.Find("Skill01/SK01_image/Fill").GetComponent<Image>();
    //     // Debug.Log("222");
    //     skillImages[1] = go.transform.Find("Skill02/SK02_image/Fill").GetComponent<Image>();
    //     skillImages[2] = go.transform.Find("Skill04/SK04_image/Fill").GetComponent<Image>();
    //     _basicskillObjects[0] = go.transform.Find("BasicSkill01/BSK01_image/Fill").GetComponent<Image>();
    //     _basicskillObjects[1] = go.transform.Find("BasicSkill02/BSK02_image/Fill").GetComponent<Image>();
    //     _basicskillObjects[2] = go.transform.Find("BasicSkill03/BSK03_image/Fill").GetComponent<Image>();
    // }
   
}
