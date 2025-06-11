using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_WinRunNGunState : PlayerSideScrollStateMachine
{
    public SideScroll_WinRunNGunState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        // Play Win Animation
        Debug.Log("Win State");
    }
    public override void Update()
    {
        
    }
    public override void FixedUpdate()
    {

    }
    public override void OntriggerEnter(Collider2D pCollider)
    {

    }
    public override void OntriggerExit(Collider2D pCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        
    }
    public override void Exit()
    {

    }
}
