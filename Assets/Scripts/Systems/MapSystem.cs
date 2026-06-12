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

        public List<NodeConnectionDefinition> GetAvailableConnections(RunState run)
        {
            NodeDefinition currentNode = GetCurrentNode(run);

            if (currentNode == null)
                return new List<NodeConnectionDefinition>();

            return currentNode.Connections
                .Where(connection => connection.StartsRevealed)
                .ToList();
        }

        public void TravelConnection(NodeConnectionDefinition connection, RunState run)
        {
            if (connection == null)
            {
                run.LogEntries.Add("No route selected.");
                return;
            }

            if (run.Player.Daylight < connection.DaylightCost)
            {
                run.LogEntries.Add($"Not enough daylight for route: {connection.DisplayName}");
                return;
            }

            if (run.Player.Stamina < connection.StaminaCost)
            {
                run.LogEntries.Add($"Not enough stamina for route: {connection.DisplayName}");
                return;
            }

            run.Player.Daylight -= connection.DaylightCost;
            run.Player.Stamina -= connection.StaminaCost;

            run.LogEntries.Add(
                $"Took route: {connection.DisplayName} " +
                $"(-{connection.DaylightCost} daylight, -{connection.StaminaCost} stamina)"
            );

            MoveToNode(connection.TargetNodeId, run);
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

            run.Map.AvailableNextNodeIds = nextNode.Connections
                .Where(connection => connection.StartsRevealed)
                .Select(connection => connection.TargetNodeId)
                .ToList();

            if (nextNode.NodeType == NodeType.Destination)
            {
                run.Map.DestinationReached = true;
                run.IsRunComplete = true;
            }

            run.LogEntries.Add($"Moved to node: {nextNode.DisplayName}");
        }
    }
}