using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    [Serializable]
    public class LaneData
    {
        public Lane lane;
        public TapBox tapBox;
    }

    public List<LaneData> laneDataList = new();
}
