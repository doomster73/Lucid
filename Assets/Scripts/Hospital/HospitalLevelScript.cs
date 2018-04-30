using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HospitalLevelScript : MonoBehaviour
{

    //public List<GameObject> checkpoints = new List<GameObject>();

    List<GameObject> CheckPointList = new List<GameObject>();

    GameObject[] CheckPoints;

    GameObject startCheckpoint;

    //private string findName;


    //List<float> lerpPoints = new List<float>();

    // Use this for initialization
    void Start()
    {

        //GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint = GameObject.FindGameObjectWithTag("CheckPoint1").transform;

        CheckPoints = GameObject.FindGameObjectsWithTag("CheckPoint");

        for (int i = 0; i < CheckPoints.Length; i++)
        {
            CheckPointList.Add(CheckPoints[i]);

            if (CheckPoints[i].GetComponent<CheckPointScript>().CheckPointName == "start")
            {
                startCheckpoint = CheckPoints[i];
                GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().Matches = startCheckpoint.GetComponent<CheckPointScript>().NumberofMatches;
                GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint = startCheckpoint.transform;
            }
        }

        if (GameObject.Find("Matches"))
        {
            GameObject.Find("Matches").GetComponent<GUIText>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAwake()
    {
        GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().Matches = 0;
    }

    //private bool isName(string name)
    //{
    //    return (name == findName);	
    //}
}
