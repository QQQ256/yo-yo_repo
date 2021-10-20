using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public static CoinUI instance;
    public int coinNumber;
    public Text coinText;
    public int currentCoinNumber;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // coinNumber = LoadData.instance.coinNumber;
        // currentCoinNumber = coinNumber;
    }

    // Update is called once per frame
    void Update()
    {
        // coinText.text = currentCoinNumber.ToString();
    }
}
