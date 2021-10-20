using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : Singleton<Controller>
{
    // public static Controller instance;

    private void Awake()
    {
        base.Awake();
        // instance = this;
    }

    void Start()
    {
        Model.GetInstance().CoinNumber = LoadData.instance.coinNumber;
        Model.GetInstance().DiamondNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
                 
    }

    #region COIN
    public static void AddCoinNumber()//在collectCoin中调用逻辑方法
    {
        Model.GetInstance().CoinNumber += 1;
    }
    
     public static int SaveCoinNumber() 
     { 
         return Model.GetInstance().CoinNumber;
     }
     
     #endregion

    #region DIAMOND
    public static void AddDiamondNumber()//collectDiamond -> animation event
    {
        Model.GetInstance().DiamondNumber += 1;
    }
    
    public static int GetCurrentDiamondNumber() //有加static就不要instance了。。。
    { 
        return Model.GetInstance().DiamondNumber;
    }

    #endregion
    
   
    
}
