using System.Collections.Generic;
using UnityEngine;

namespace BeneathThePines
{
    [CreateAssetMenu(fileName = "NodeDefinition", menuName = "Beneath the Pines/Node Definition")]
    public class NodeDefinition : ScriptableObject
    {
        public string NodeId;
        public string DisplayName;
        public NodeType NodeType;

        public List<NodeConnectionDefinition> Connections = new();

        public Vector2 MapPosition;

        public List<TrailCardDefinition> PossibleCards = new();
    }
}