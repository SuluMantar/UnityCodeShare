using UnityEngine;

// Script for the Slow Time ability
[CreateAssetMenu(menuName = "Abilities/Slow Time Ability")]
public class SlowTimeAbility : AbilityBase
{
    public float slowdownFactor = 0.5f;
    //public float duration = 3f;

    public override void ActivateAbility()
    {
        // Slow down time
        Time.timeScale = slowdownFactor;
        Debug.Log("Time Slowed");

        // Wait for the duration
        //WaitForSeconds wait = new WaitForSeconds(duration);
        //CoroutineRunner.Instance.StartCoroutine(ResetTimeScaleAfterDelay(wait));
    }

    /*private IEnumerator ResetTimeScaleAfterDelay(WaitForSeconds wait)
    {
        yield return wait;

        // Reset the time scale
        Time.timeScale = 1f;
    }*/
}
