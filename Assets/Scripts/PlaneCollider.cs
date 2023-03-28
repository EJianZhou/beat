using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath.NET;
using BEPUphysics.BroadPhaseEntries;

public class PlaneCollider : MonoBehaviour
{
    private BEPUphysics.Entities.Prefabs.Box boxEntity = null;

    public float mass;
    public float width;
    public float height;
    public float length;
    public bool isStatic;


    // Start is called before the first frame update
    void Start()
    {
        if (this.isStatic)
        {
            this.boxEntity = new BEPUphysics.Entities.Prefabs.Box(ConversionHelper.MathConverter.Convert(this.transform.position), (Fix64)width, (Fix64)height, (Fix64)length);
        }
        else
        {
            this.boxEntity = new BEPUphysics.Entities.Prefabs.Box(ConversionHelper.MathConverter.Convert(this.transform.position), (Fix64)width, (Fix64)height, (Fix64)length, (Fix64)mass);
        }
        boxEntity.CollisionInformation.Entity.Tag = "Plane";
        boxEntity.Tag = "Plane";
        PhysicManager.Instance.space.Add(this.boxEntity);
        boxEntity.CollisionInformation.Events.PairCreated += CHandle;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this.isStatic) return;
        BEPUutilities.Vector3 pos = this.boxEntity.position;
        this.transform.position = ConversionHelper.MathConverter.Convert(pos);
    }

    void CHandle(BEPUphysics.BroadPhaseEntries.MobileCollidables.EntityCollidable sender, BroadPhaseEntry other, BEPUphysics.NarrowPhaseSystems.Pairs.NarrowPhasePair handlePair)
    {
        Debug.Log("Plane");
    }

}
