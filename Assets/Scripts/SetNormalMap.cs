using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNormalMap : StateMachineBehaviour
{
    public Material material;
    public Texture2D normalMap;

    // This will be called when the animator first transitions to this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        material.SetTexture("_NormalMap", normalMap);
    }
}
