using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionality : MonoBehaviour
{

    public BrushDrawFromHere[] bdfh;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandFix() 
    {
        player.HandFixy();

    }


    public void NextAction()
    {
        bdfh = FindObjectsOfType<BrushDrawFromHere>();
        //find hands and cycle to next action
        foreach (BrushDrawFromHere b in bdfh)
        {
            b.NextAction();
        }
    }
}
