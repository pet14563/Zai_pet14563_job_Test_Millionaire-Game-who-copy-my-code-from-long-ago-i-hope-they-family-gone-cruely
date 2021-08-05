using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{

    public float hourSettingTimeData;
    public float minuteSettingTimeData;

    public DateNotification datenotificationScripts;
    // Start is called before the first frame update
    void Start()
    {
        datenotificationScripts = GetComponent<DateNotification>();
        LoadSecSettingTimeData(); // load secsetting data
        SendDataBack();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveSecSettingTimeData()
    {
        PlayerPrefs.SetFloat("hourSettingTimeData", datenotificationScripts.hourSetting);
        PlayerPrefs.SetFloat("minuteSettingTimeData", datenotificationScripts.minuteSetting);
    }
    public void LoadSecSettingTimeData()
    {
        hourSettingTimeData = PlayerPrefs.GetFloat("hourSettingTimeData");
        minuteSettingTimeData = PlayerPrefs.GetFloat("minuteSettingTimeData");
    }
    public void SendDataBack()
    {
        datenotificationScripts.hourSetting = hourSettingTimeData;
        datenotificationScripts.minuteSetting = minuteSettingTimeData;
    }
}
