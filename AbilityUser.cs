using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{

    public AbilityBase ability;
    private float duration;
    private float maxDuration;

    void Start()
    {

        duration = Time.time - ability.cooldown;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            if (Time.time - duration >= ability.cooldown)
            {
                DoIt();
                duration = Time.time;

            }
            else
            {
                Debug.Log("Its in cooldown");
            }
        }
       

    }



    void DoIt()
    {
        Debug.Log("Time finished");


    }
}
