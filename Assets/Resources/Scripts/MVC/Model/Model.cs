using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型委托 当用户信息发生变化的时候就执行
/// </summary>
public delegate void OnValueChange(int value);
public class Model
{
    public OnValueChange OnCoinNumberChange;
    public OnValueChange OnDiamondNumberChange;

    private int coinNumber;
    private int diamondNumber;


    //single mode
    private static Model _Instance;

    public static Model GetInstance()
    {
        return _Instance ??= new Model();//如果 == null 那么就实例话一个新的出来
    }
    private Model(){}

    public int CoinNumber
    {
        get => coinNumber;
        set
        {
            coinNumber = value;
            OnCoinNumberChange?.Invoke(coinNumber);//如果委托对象不为空 就执行委托
        }
    }
    
    public int DiamondNumber
    {
        get => diamondNumber;
        set
        {
            diamondNumber = value;
            OnDiamondNumberChange?.Invoke(diamondNumber);//如果委托对象不为空 就执行委托
        }
    }

}
