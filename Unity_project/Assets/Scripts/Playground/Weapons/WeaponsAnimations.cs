using UnityEngine;

namespace Playground.Weapons
{
    public class WeaponsAnimations : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            Debug.Log("LOULO" + animator.gameObject);
            animator.SetInteger("IsFighting", 0);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (animator.gameObject.CompareTag("Weapons"))
                animator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }
    }
}