using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Shield Ability")]
public class ShieldAbility : AbilityBase
{
    public override void ActivateAbility()
    {
        // Slow down time
        //Time.timeScale = slowdownFactor;
        Debug.Log("Shield Activated");

        // Wait for the duration
        //WaitForSeconds wait = new WaitForSeconds(duration);
        //CoroutineRunner.Instance.StartCoroutine(ResetTimeScaleAfterDelay(wait));
    }

}
