using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{

    private Entity player;

    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }
    

    public void DamageTargets(){
        player.DamageTargets();
    }

    private void DisableMovementAndJump(){
        player.EnableMovementAndJump(false);
    }

    private void EnableMovementAndJump(){
        player.EnableMovementAndJump(true);
    }
}
