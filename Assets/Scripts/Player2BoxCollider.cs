using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath.NET;
using BEPUphysics.BroadPhaseEntries;

public class Player2BoxCollider : MonoBehaviour
{
    private static Player2BoxCollider _instance;
    public static Player2BoxCollider Instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    public BEPUphysics.Entities.Prefabs.Box boxEntity = null;

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
        PhysicManager.Instance.space.Add(this.boxEntity);
        boxEntity.CollisionInformation.Events.InitialCollisionDetected += CHandle;
        this.boxEntity.isDynamic = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this.isStatic) return;
        Vector3 pos = this.transform.position;
        this.boxEntity.position= ConversionHelper.MathConverter.Convert(pos);
        Debug.Log("iiii2" + this.transform.position);
    }

    void CHandle(BEPUphysics.BroadPhaseEntries.MobileCollidables.EntityCollidable sender, Collidable other, BEPUphysics.NarrowPhaseSystems.Pairs.NarrowPhasePair handlePair)
    {
        var otherEntityInformation = other as BEPUphysics.BroadPhaseEntries.MobileCollidables.EntityCollidable;
        Debug.Log(otherEntityInformation.Entity.Tag);
        Debug.Log(other.Tag);
        Debug.Log(other.GetType());
        Debug.Log("!!!");
    }

}
