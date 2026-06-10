using System.Collections.Generic;
using System.Linq;

namespace BeneathThePines
{
    public class MapSystem
    {
        private readonly List<NodeDefinition> _nodes;

        public MapSystem(List<NodeDefinition> nodes)
        {
            _nodes = nodes;
        }

        public NodeDefinition GetCurrentNode(RunState run)
        {
            return _nodes.FirstOrDefault(node => node.NodeId == run.Map.CurrentNodeId);
        }

        public void MoveToNode(string nodeId, RunState run)
        {
            NodeDefinition nextNode = _nodes.FirstOrDefault(node => node.NodeId == nodeId);

            if (nextNode == null)
            {
                run.LogEntries.Add($"Node not found: {nodeId}");
                return;
            }

            run.Map.CurrentNodeId = nodeId;

            if (!run.Map.VisitedNodeIds.Contains(nodeId))
                run.Map.VisitedNodeIds.Add(nodeId);

            run.Map.AvailableNextNodeIds = new List<string>(nextNode.NextNodeIds);

            if (nextNode.NodeType == NodeType.Destination)
            {
                run.Map.DestinationReached = true;
                run.IsRunComplete = true;
            }

            run.LogEntries.Add($"Moved to node: {nextNode.DisplayName}");
        }
    }
}
