using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUser : MonoBehaviour
{

    public AbilityBase ability;
    private float duration;
    private float skillDuration;
    private bool active;

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


        if (active)
        {
            skillDuration -= Time.deltaTime;
            if (skillDuration <= 0f)
            {
                Debug.Log("Skill Deactivated");
                active = false;
                
            }
        }
    }



    void DoIt()
    {
        ability.ActivateAbility();
        skillDuration = ability.duration;
        active = true;

        //Debug.Log("Time finished");


    }
}
