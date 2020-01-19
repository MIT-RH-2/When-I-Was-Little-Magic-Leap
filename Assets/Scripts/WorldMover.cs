using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMover : MonoBehaviour
{
    public bool isFirstClick;

    public bool setUp;

    private Transform _controlObject;
    public Transform _player;

    public Vector3 controllerStart;
    public Vector3 controllerEnd;

    public GameObject startPt;
    public GameObject endPt;

    // Start is called before the first frame update
    void Start()
    {
        //_player = this.gameObject.transform.root;
        _controlObject = GameObject.FindGameObjectWithTag("env").transform;

        startPt = GameObject.FindGameObjectWithTag("startPt");
        endPt = GameObject.FindGameObjectWithTag("endPt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstClick()
    {
        //isFirstClick = !isFirstClick;

        //if (isFirstClick)
        //{

            startPt.transform.position = this.gameObject.transform.position;
            controllerStart = this.gameObject.transform.position;

            setUp = true;
            //didOnceTrig = true;
        //}
    }

    public void SecondClick()
    {
        endPt.transform.position = this.gameObject.transform.position;
        controllerEnd = this.gameObject.transform.position;
        _player.transform.position += controllerStart - controllerEnd;

        setUp = false;
        //didOnceTrig = false;

        //endPt.SetActive(false);
        //startPt.SetActive(false);

        Debug.Log("DID YOU CHANGE THE WORLD?");
    }

}
