using Photon.Pun;
using UnityEngine;

namespace Playground.Weapons
{
    public class WeaponsAnimations : StateMachineBehaviour
    {
        [PunRPC]
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetInteger("IsFighting", 0);
        }

        [PunRPC]
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (animator.gameObject.CompareTag("Weapons"))
                animator.gameObject.GetComponent<Weapon>().ToogleActivation();
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