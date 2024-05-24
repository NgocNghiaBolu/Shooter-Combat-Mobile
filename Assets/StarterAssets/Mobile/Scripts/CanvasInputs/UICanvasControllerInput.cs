using StarterAssets;
using UnityEngine;


    public class UICanvasControllerInput : MonoBehaviour
    {

    [Header("Output")]
    public StarterAssetsInputs starterAssetsInputs;
    //public PlayerInputSystem inputs;

        //public void VirtualMoveInput(Vector2 virtualMoveDirection)
        //{
        //    starterAssetsInputs.MoveInput(virtualMoveDirection);
        //}

    //public void VirtualLookInput(Vector2 virtualLookDirection)
    //{
    //    starterAssetsInputs.LookInput(virtualLookDirection);
    //}

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.jump = virtualJumpState;
            //starterAssetsInputs.JumpInput(virtualJumpState);
            //inputs.jump = virtualJumpState;
        }

    public void VirtualRunInput(bool virtualRunState)
    {
        starterAssetsInputs.run = virtualRunState;
        //starterAssetsInputs.JumpInput(virtualJumpState);
        //inputs.jump = virtualJumpState;
    }

}


