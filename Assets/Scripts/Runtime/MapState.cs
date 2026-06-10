using System;
using System.Collections.Generic;

namespace BeneathThePines
{
    [Serializable]
    public class MapState
    {
        public string CurrentNodeId;
        public List<string> VisitedNodeIds = new();
        public List<string> AvailableNextNodeIds = new();
        public bool DestinationReached;
    }
}
