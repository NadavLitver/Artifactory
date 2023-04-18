using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.assets.CamShaker.Shake(new DamageHandler() { amount = 40});
        GameManager.Instance.StartCoroutine(ResendToBase());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    IEnumerator ResendToBase()
    {
        yield return new WaitForSeconds(3f);
        yield return GameManager.Instance.assets.blackFade.GetFadeToBlackRoutine();
        
        Color endColor = GameManager.Instance.assets.ThanksForPlaying.color;
        GameManager.Instance.assets.ThanksForPlaying.color = new Color(GameManager.Instance.assets.ThanksForPlaying.color.r, GameManager.Instance.assets.ThanksForPlaying.color.g, GameManager.Instance.assets.ThanksForPlaying.color.b, 0);
        Color startColor = GameManager.Instance.assets.ThanksForPlaying.color;
        GameManager.Instance.assets.ThanksForPlaying.gameObject.SetActive(true);
        float counter = 0f;
        while (counter < 1)
        {
            GameManager.Instance.assets.ThanksForPlaying.color = Color.Lerp(startColor, endColor, counter);
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(4f);
        GameManager.Instance.assets.playerActor.PlayerRelicInventory.ClearRelics();
      GameManager.Instance.assets.blackFade.FadeFromBlack();
        GameManager.Instance.assets.ThanksForPlaying.gameObject.SetActive(false);
        GameManager.Instance.assets.baseFatherObject.SetActive(true);
        GameManager.Instance.assets.Player.transform.position = GameManager.Instance.assets.baseSpawnPlayerPositionObject.transform.position;
    }
}
