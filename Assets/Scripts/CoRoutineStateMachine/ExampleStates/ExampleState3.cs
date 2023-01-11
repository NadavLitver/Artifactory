using System.Collections;
using UnityEngine;

public class ExampleState3 : CoRoutineState
{
    [SerializeField] float duration, cooldown, lastPerformed;
    private void Start()
    {
        lastPerformed = duration * -1;
    }
    public override IEnumerator StateRoutine()
    {
        float counter = 0;
        while (counter < duration)
        {

            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Debug.Log(this.name + "my priority is : " + priority);

        }
        lastPerformed = Time.time;

    }

    internal override bool myCondition()
    {
        if (Time.time - lastPerformed >= cooldown)
            return true;

        return false;
    }


}
