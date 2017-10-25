using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    /*
     Copyright Sigma Studios (c) 2017
     All Rights Reserved.
    */

    public displayRocket dispRocket;

    public Text buyResult;
    public GameObject buyResultObj;
    public GameObject[] moneyLabels;
    public GameObject[] buyButtons;
    public GameObject[] selectButtons;
    public Text moneyStatus;
    public int[] rocketIDs;
    int rocketID;

    void Start ()
    {
        CheckMem();
        moneyStatus.text = "$" + PlayerPrefs.GetInt("balance");
    }
    public void GetRocketID (int id)
    {
        rocketID = id;
    }
    public void BuyRocket (int cost)
    {
        if (PlayerPrefs.GetInt(rocketID.ToString()) != 1)
        {
           if (cost <= PlayerPrefs.GetInt("balance"))
           {
                int balance = PlayerPrefs.GetInt("balance");
                balance = balance - cost;
                PlayerPrefs.SetInt("balance", balance);
                AddToBought();
                SelectRocket(rocketID);
                moneyStatus.text = "$" + balance;

           }
           else
           {
               buyResultObj.SetActive(true);
               buyResult.text = "Insufficient Funds.";
           }
        } else
        {
            AddToBought();
            SelectRocket(rocketID);
        }   
    }
    public void SelectRocket (int rocketID)
    {
        PlayerPrefs.SetInt("currentRocketID", rocketID);
        selectButtons[rocketID].SetActive(false);
    }
    void AddToBought ()
    {
        moneyLabels[rocketID].SetActive(false);
        buyButtons[rocketID].SetActive(false);

        AddMem(rocketID);
    }
    void AddMem (int id)
    {
        PlayerPrefs.SetInt("rocketBoughtID" + id, 1);
    }
    void CheckMem ()
    {
        foreach (int id in rocketIDs)
        {
            if (PlayerPrefs.GetInt("rocketBoughtID" + id) == 1)
            {
                moneyLabels[id].SetActive(false);
                buyButtons[id].SetActive(false);
                if (PlayerPrefs.GetInt("currentRocketID") == id)
                {
                    selectButtons[id].SetActive(false);
                } else
                {
                    selectButtons[id].SetActive(true);
                }
            }
        }
    }   
}

