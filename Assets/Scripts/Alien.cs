using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Alien : MonoBehaviour {
	public Canvas ui;
	public Text txtAlien;
	public float speed = 20;
    public bool Live = true;
    public Transform SavedPosition;

    private int targetIndex = 1;
    private List<Transform> targets;
    private int Coc = 0;

	void Start()
	{
		txtAlien = ui.GetComponentInChildren<Text>();
		txtAlien.text = Coc.ToString();
		Reset();
	}

	public void Reset()
	{
		GetComponent<Animator>().Play("Grounded");
		GetComponent<Animator>().SetFloat("Forward", 1f);
	}

	void Update () {
		if(targets[targetIndex] != null && Live)
		{
			var delta = targets[targetIndex].position - transform.position;
			delta.y = 0f;
			var deltaLen = delta.magnitude;
			var move = Mathf.Min((speed * GameManager.Instance.Speed) * Time.deltaTime, deltaLen);
            if (deltaLen <= 5f)
                if (targetIndex < targets.Count -1)
                    targetIndex++;
                else
                {
                    Live = false;
                    GameManager.Instance.PlusAlienCant(-1);
                }               
            else
            {
                var direction = delta / deltaLen;
                transform.forward = Vector3.Slerp(transform.forward, direction, 10f * Time.deltaTime);
                GetComponent<CharacterController>().Move(move * transform.forward + Vector3.down * 3f);
            }
		}		
        else if (!Live)
        {
            this.transform.position = SavedPosition.position;
            this.gameObject.SetActive(false);
            ui.gameObject.SetActive(false);
        }
		if(ui != null)
		{
			ui.transform.position = transform.position;
		}
	}

    public void SetTargets(List<Transform> _targets)
    {
        targets = _targets;
    }
    public void SetTargetIndex(int _targetIndex)
    {
        targetIndex = _targetIndex;
    }
    public void SetPosition(Transform tran)
    {
        this.transform.position = tran.position;
    }
    public void RestartAlien()
    {
        Live = true;
        this.gameObject.SetActive(true);
        ui.gameObject.SetActive(true);
    }
    public float GetAngle()
    {
        return Math.Abs(this.transform.rotation.y) * 100;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cocoon")
        {
            Coc += 1;
            GameManager.Instance.PlusCocoonCant(1);
        }
        txtAlien.text = Coc.ToString();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cocoon")
        {
            Coc -= 1;
        }
        txtAlien.text = Coc.ToString();
    }
}
