using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfigPanel : MonoBehaviour {

    public Slider sliderRate;
    public Slider sliderSpeed;
    public Text txtRate;
    public Text txtSpeed;

    public Text txtCocoonDate;
    public Text txtAliensDate;
    public Text txtAngleDate;

    private int rate;

    void Start() {
        rate = Mathf.RoundToInt(GameManager.Instance.RateSpawn);
        sliderRate.value = rate;
        txtRate.text = rate.ToString();
        txtSpeed.text = GameManager.Instance.Speed.ToString();
        sliderSpeed.value = GameManager.Instance.Speed;
    }

    public void UpdateRate()
    {
        GameManager.Instance.RateSpawn = sliderRate.value;
        rate = Mathf.RoundToInt(GameManager.Instance.RateSpawn);
        txtRate.text = rate.ToString();
    }
    public void UpdateSpeed()
    {
        GameManager.Instance.Speed = sliderSpeed.value;
        txtSpeed.text = GameManager.Instance.Speed.ToString();
    }
    public void UpdateCocoonData()
    {
        txtCocoonDate.text = GameManager.Instance.GetCocoonCant().ToString();
    }

    public void UpdateAlienData(float angle)
    {
        txtAliensDate.text = GameManager.Instance.GetAlienCant().ToString();
        txtAngleDate.text = angle.ToString() + "°";
    }
}
