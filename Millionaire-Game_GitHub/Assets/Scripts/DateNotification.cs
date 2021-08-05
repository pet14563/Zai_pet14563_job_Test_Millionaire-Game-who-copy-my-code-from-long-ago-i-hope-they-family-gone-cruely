using Unity.Notifications.Android;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateNotification : MonoBehaviour
{
    public Text timeTextNow;
    private string currenttime;

    public Text hourText;
    public float hourforText;

    public Text minText;
    public float minforText;

    // up here this is just show the time right now


    public float maxDaySec;

    public float midnightTimeLeft;

    public float minuteSetting;
    public float hourSetting;

    public float secHourSetting;
    public float secMinuteSetting;
    public float settingTotalTime;

    public float hour;
    public float minute;
    public float second;
    public float passtotaltime;

    public Menu menuScripts;
    public SaveLoad saveloadScripts;

    void Start()
    {
        menuScripts = GetComponent<Menu>();
        saveloadScripts = GetComponent<SaveLoad>();
        maxDaySec = 86400;
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        StartCoroutine("waitOneSecond");
    }

    private void Update()
    {

        if (passtotaltime >= settingTotalTime && passtotaltime <= settingTotalTime)
        {
            menuScripts.canplay = true;
        }
        currenttime = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm");
        timeTextNow.text = "" + currenttime + "";
        DayTimetoSec();
        HourMinSettingtoSec();

        hourText.text = ""+ hourSetting;
        minText.text = "" + minuteSetting;
    }
    // Update is called once per fram

    public void DayTimetoSec()
    {
        hour = System.DateTime.Now.Hour * 3600; // 1 hour have 3600 sec
        minute = System.DateTime.Now.Minute * 60; //1 min have 60 sec
        second = System.DateTime.Now.Second;
        passtotaltime = hour + minute + second; // the seconed we have pass in this day.

        midnightTimeLeft = maxDaySec - passtotaltime; //how much sec we will go to 00:00
    }
    public void HourMinSettingtoSec() // setting mean the time we setting on player action
    {
        secHourSetting = hourSetting * 3600;
        secMinuteSetting = minuteSetting * 60;
        settingTotalTime = secHourSetting + secMinuteSetting; //the sec time we setting start from 00 : 00
    }

    public void PressHourSettingUp()
    {
        if (hourSetting <= 23)
        {
            hourSetting += 1;
        }
    }
    public void PressHourSettingDown()
    {
        if (hourSetting >= 1)
        {
            hourSetting -= 1;
        }
    }
    public void PressMinuteSettingUp()
    {
        if (minuteSetting <= 59)
        {
            minuteSetting += 1;
        }
    }
    public void PressMinuteSettingDown()
    {
        if (minuteSetting >= 1)
        {
            minuteSetting -= 1;
        }
        
    }
    public void PressSave()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        CreateNotifChannel();
        SendNotification();
        saveloadScripts.SaveSecSettingTimeData();
        if (passtotaltime >= settingTotalTime)
        {
            menuScripts.canplay = true;
        }
        else
        {
            menuScripts.canplay = false;
        }

    }

    void CreateNotifChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Emilly: Game are Ready!";
        notification.Text = "Lets play the game!!!";
        //notification.FireTime = System.DateTime.Now.AddSeconds(midnightTimeLeft); // this mean notification will show at time 00:00
        // if current time is lower than our time setting
        if (passtotaltime <= settingTotalTime)
        {
            notification.FireTime = System.DateTime.Now.AddSeconds(settingTotalTime - passtotaltime);
            print("sent notification!!!!!!!!");

        }
        // if current time is more than our time setting
        if (passtotaltime >= settingTotalTime)
        {
            notification.FireTime = System.DateTime.Now.AddSeconds(midnightTimeLeft + settingTotalTime);
            print("sent notification!!!!!!!!2");

        }
        notification.LargeIcon = "icon_1"; // name of your icon
        // send the notifications
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //if the script is run and a message is already scheduled
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

    IEnumerator waitOneSecond()
    {
        yield return new WaitForSeconds(1);
        print("wait 1 sec after start()");
        if (passtotaltime <= settingTotalTime)
        {
            menuScripts.canplay = false;
        }
    }
}
