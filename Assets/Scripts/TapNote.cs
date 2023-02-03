using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TapNote : Note
{
    private IObjectPool<TapNote> pool;

    public void SetPool(IObjectPool<TapNote> _pool)
    {
        pool = _pool;
    }
    
    public override void UnInit()
    {
        base.UnInit();
        pool.Release(this);
    }
}
