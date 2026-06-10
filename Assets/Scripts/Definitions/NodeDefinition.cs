using System.Collections.Generic;

namespace BeneathThePines
{
    public class NodeDefinition
    {
        public string NodeId;
        public string DisplayName;
        public NodeType NodeType;

        public List<string> NextNodeIds = new();
        public List<TrailCardDefinition> PossibleCards = new();
    }
}
