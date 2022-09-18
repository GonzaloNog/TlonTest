using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Spawner : MonoBehaviour {
    public List<Transform> targets;
    public Transform SavedPosition;

    public Alien prefabAlien;
    public int AlienCantMax;
	public Canvas prefabAlienUI;
	public Director director;

    private List<Alien> AliensList = new List<Alien>();
    private int indexSpawn;
    private bool error = false;
    private float time = 0.0f;

    void ResetAlien(Alien alien)
	{
		alien.ui.transform.position = alien.transform.position = targets[0].position;
		alien.transform.rotation = targets[0].transform.rotation;

		alien.Reset();
	}

	Alien CreateAlien()
	{
        var alien = Instantiate(prefabAlien, this.transform);
        alien.ui = Instantiate(prefabAlienUI, this.transform);
        alien.Live = false;
        ResetAlien(alien);
		return alien;
	}

	void Start () {
        for(int a = 0; a < AlienCantMax; a++)
        {
            Alien alien = CreateAlien();
            alien.SetTargets(targets);
            alien.SavedPosition = SavedPosition;
            AliensList.Add(alien);
        }
	}

    public void SpawnAlien()
    {
        bool live = false;
        int a = 0;
        while (!live)
        {
            if (!AliensList[a].Live)
                live = true;
            else
                a++;
            if(a >= AlienCantMax)
            {
                live = true;
                error = true;
            }

        }
        if (!error)
        {
            GameManager.Instance.PlusAlienCant(1);
            AliensList[a].SetPosition(targets[0]);
            AliensList[a].SetTargetIndex(1);
            AliensList[a].RestartAlien();
            AliensList[a].Reset();
        }
        else
        {
            print("Error: Cantidad de aliens insuficientes, Cantidad de aliens en lista: " + AliensList.Count);
            error = false;
        }
    }


    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1/GameManager.Instance.RateSpawn)
        {
            print("Spawn");
            SpawnAlien();
            time = 0;
        }
    }
    
    public float GetAngle()
    {
        float plusAngle = 0.0f;
        for(int a = 0; a < AliensList.Count; a++){
            if (AliensList[a].Live)
            {
                plusAngle += AliensList[a].GetAngle();
            }
        }
        plusAngle = plusAngle / GameManager.Instance.GetAlienCant();
        return plusAngle;
    }
}
