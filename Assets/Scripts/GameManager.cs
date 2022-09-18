using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public float Speed = 1.0f;
    public float RateSpawn = 3.0f;
    public float TimeCocoonUI = 1.0f;
    public ConfigPanel UIConfig;
    public Spawner Spawn;

    private int aliensCant = 0;
    private int CocoonCant = 0;
    //private float angleAlien = 0.0f;

    private float time = 0.0f;
    //public int 
    void Awake()
    {
        if (Instance == null) 
        {
            DontDestroyOnLoad(gameObject); 
            Instance = this;
        }
        else if (Instance != this) 
        {
            Destroy(gameObject); 
        }
    }

    //UI data////////////////////////////////////
    public int GetAlienCant()
    {
        return aliensCant;
    }
    public void SetAlienCant(int _AlienCant)
    {
        aliensCant = _AlienCant;
    }
    public void PlusAlienCant(int plus)
    {
        aliensCant += plus;
    }
    public int GetCocoonCant()
    {
        return CocoonCant;
    }
    public void SetCocoonCant(int _CocoonCant)
    {
        CocoonCant = _CocoonCant;
    }
    public void PlusCocoonCant(int plus)
    {
        CocoonCant += plus;
    }
    /////////////////////////////////////////////

    private void Update()
    {
        time += Time.deltaTime;
        UIConfig.UpdateAlienData(Spawn.GetAngle());
        if(time >= TimeCocoonUI)
        {
            time = 0.0f;
            UIConfig.UpdateCocoonData();
            CocoonCant = 0;
        }
    }
    private void LateUpdate()
    {
       //angleAlien = 0;
    }

}
