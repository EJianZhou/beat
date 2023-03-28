using System.Collections;
using System.Collections.Generic;
using FixMath.NET;
using UnityEngine;

public class PhysicManager : MonoBehaviour
{
    public static PhysicManager Instance = null;
    public BEPUphysics.Space space = null;
    private void Awake()
    {
        if (PhysicManager.Instance!=null)
        {
            return;
        }

        //create
        PhysicManager.Instance = this;
        this.space = new BEPUphysics.Space();
        this.space.ForceUpdater.gravity = new BEPUutilities.Vector3(0,(Fix64)(-9.81m), 0);
        this.space.TimeStepSettings.TimeStepDuration = 1 / 60m;
        

        Physics.autoSimulation = false;
        Physics.autoSyncTransforms = false;

    }

    private void Update()
    {
        this.space.Update();
    }


}
