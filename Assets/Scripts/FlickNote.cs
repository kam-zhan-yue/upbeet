using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlickNote : Note
{
    private IObjectPool<FlickNote> pool;

    public void SetPool(IObjectPool<FlickNote> _pool)
    {
        pool = _pool;
    }

    public override void UnInit()
    {
        base.UnInit();
        pool.Release(this);
    }
}
