using UnityEngine;

namespace Playground.Weapons
{
    public class WeaponsAnimations : StateMachineBehaviour
    {
        public Weapon weapon;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger("IsFighting", 0);
            Debug.Log("OnStateExit");
            Debug.Log(animator.name);
            if (!(weapon is null))
            {
                Debug.Log(weapon.gameObject.GetComponent<SpriteRenderer>());
                weapon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
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