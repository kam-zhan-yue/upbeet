using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HoldNote : Note
{
    private IObjectPool<HoldNote> pool;

    public void SetPool(IObjectPool<HoldNote> _pool)
    {
        pool = _pool;
    }
    
    public override void UnInit()
    {
        base.UnInit();
        pool.Release(this);
    }
}
