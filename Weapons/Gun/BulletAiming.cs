using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAiming : MonoBehaviour, IShootingObserver
{
    [SerializeField] private ShootingSubject shootingSubject;
    [SerializeField] private Transform aimingPivot;
    [SerializeField] private Transform normalBulletSpawn;
    private void OnEnable()
    {
        shootingSubject.AddShootingObserver(this);
    }
    private void OnDisable()
    {
        shootingSubject.RemoveShootingObserver(this);
    }
    public void OnShootingNotify(ShootingAction shootingAction)
    {
        switch (shootingAction)
        {
            case(ShootingAction.aimright):
                AimStraightRight();
                return;
            case (ShootingAction.aim45topright):
                Aim45TopRight();
                return;
            case (ShootingAction.aim45downright):
                Aim45BottomRight();
                return;
            case (ShootingAction.aimleft):
                AimStraightLeft();
                return;
            case (ShootingAction.aim45topleft):
                Aim45TopLeft();
                return;
            case (ShootingAction.aim45downleft):
                Aim45BottomLeft();
                return;
            case (ShootingAction.aimtop):
                AimTop();
                return;
            case (ShootingAction.aimdown):
                AimBottom();
                return;
        }
    }
    private void AimStraightRight()
    {
        aimingPivot.localRotation = Quaternion.Euler(0,0,-90);
    }
    private void Aim45TopRight()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, -45);
    }
    private void Aim45BottomRight()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, -135);
    }
    private void AimStraightLeft()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, 90);
    }
    private void Aim45TopLeft()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, 45);
    }
    private void Aim45BottomLeft()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, 135);
    }
    private void AimTop()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, 0);
    }
    private void AimBottom()
    {
        aimingPivot.localRotation = Quaternion.Euler(0, 0, -180);
    }
}
