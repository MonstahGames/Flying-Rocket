using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    /*
     Copyright Sigma Studios (c) 2017
     All Rights Reserved.

        (no judging the ugly code thanks)
    */

    public Text buyResult;
    public GameObject buyResultObj;
    public GameObject[] moneyLabels;
    public GameObject[] buttons;
    public Text moneyStatus;
    public int moneyInspector;
    int rocketID;

    void Start ()
    {
        AddMoney();
        moneyStatus.text = "$" + PlayerPrefs.GetInt("balance");
    }
    void AddMoney ()
    {
        PlayerPrefs.SetInt("balance", moneyInspector);
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
        Debug.Log("Selected.");
    }
    void AddToBought ()
    {
        moneyLabels[rocketID].SetActive(false);
        buttons[rocketID].SetActive(false);
        PlayerPrefs.SetInt("id " + rocketID.ToString(), 1);
        Debug.Log(PlayerPrefs.GetInt(rocketID.ToString()));
    }

    //Only for testing, won't be in final 1.3
    public void DelKey ()
    {
        PlayerPrefs.DeleteKey("0");
        PlayerPrefs.DeleteKey("1");
        PlayerPrefs.DeleteKey("2");
        PlayerPrefs.DeleteKey("currentRocketID");
    }
}

