using System;

namespace BeneathThePines
{
    [Serializable]
    public class NodeConnectionDefinition
    {
        public string TargetNodeId;
        public string DisplayName;
        public string Description;

        public int DaylightCost;
        public int StaminaCost;

        public bool StartsRevealed = true;
    }
}