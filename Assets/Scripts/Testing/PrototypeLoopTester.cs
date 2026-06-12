using System.Collections.Generic;
using UnityEngine;

namespace BeneathThePines
{
    public class PrototypeLoopTester : MonoBehaviour
    {
        [SerializeField] private List<NodeDefinition> testNodes = new();

        private RunState _run;

        private PlayerStatSystem _playerStats;
        private TrailCardResolver _cardResolver;
        private CampSystem _campSystem;
        private NightResolutionSystem _nightSystem;
        private MapSystem _mapSystem;

        private void Start()
        {
            _playerStats = new PlayerStatSystem();
            _cardResolver = new TrailCardResolver(_playerStats);
            _campSystem = new CampSystem();
            _nightSystem = new NightResolutionSystem(_playerStats);

            _run = RunFactory.CreateNewRun();

            _mapSystem = new MapSystem(testNodes);

            Debug.Log("=== Beneath the Pines Scriptable Node Route Test ===");

            RunTestLoop();

            PrintLog();
            PrintFinalStats();
        }

        private void RunTestLoop()
        {
            _mapSystem.MoveToNode("start", _run);

            PrintAvailableRoutes();
            TravelFirstAvailableRoute();

            ResolveMuddyTrailCard();

            PrintAvailableRoutes();
            TravelFirstAvailableRoute();

            PerformBasicCampActions();

            _nightSystem.ResolveNight(_run);

            PrintAvailableRoutes();
            TravelFirstAvailableRoute();
        }

        private void ResolveMuddyTrailCard()
        {
            TrailCardDefinition muddyTrail = new TrailCardDefinition
            {
                CardId = "card_muddy_trail",
                DisplayName = "Muddy Trail",
                DaylightCost = 15,
                StaminaCost = 10,
                WarmthDelta = -5,
                MoraleDelta = 0,
                WaterDelta = 0,
                FoodDelta = 0,
                AppliedStatusEffect = StatusEffectType.Soaked
            };

            _cardResolver.Resolve(muddyTrail, _run);
        }

        private void PerformBasicCampActions()
        {
            CampActionDefinition shelter = new CampActionDefinition
            {
                ActionId = "set_shelter",
                DisplayName = "Set Shelter",
                IsMandatory = true,
                DaylightCost = 20
            };

            CampActionDefinition water = new CampActionDefinition
            {
                ActionId = "filter_water",
                DisplayName = "Filter Water",
                IsMandatory = true,
                DaylightCost = 10
            };

            CampActionDefinition meal = new CampActionDefinition
            {
                ActionId = "eat_meal",
                DisplayName = "Eat Meal",
                IsMandatory = true,
                DaylightCost = 10,
                FoodCost = 1
            };

            _campSystem.PerformAction(shelter, _run);
            _campSystem.PerformAction(water, _run);
            _campSystem.PerformAction(meal, _run);
        }

        private void TravelFirstAvailableRoute()
        {
            List<NodeConnectionDefinition> availableRoutes = _mapSystem.GetAvailableConnections(_run);

            if (availableRoutes.Count == 0)
            {
                _run.LogEntries.Add("No available routes from current node.");
                return;
            }

            NodeConnectionDefinition selectedRoute = availableRoutes[0];

            _run.LogEntries.Add($"Selected route: {selectedRoute.DisplayName}");

            _mapSystem.TravelConnection(selectedRoute, _run);
        }

        private void PrintAvailableRoutes()
        {
            NodeDefinition currentNode = _mapSystem.GetCurrentNode(_run);

            if (currentNode == null)
            {
                _run.LogEntries.Add("Current node not found.");
                return;
            }

            _run.LogEntries.Add($"Current node: {currentNode.DisplayName}");

            List<NodeConnectionDefinition> availableRoutes = _mapSystem.GetAvailableConnections(_run);

            if (availableRoutes.Count == 0)
            {
                _run.LogEntries.Add("Available routes: none");
                return;
            }

            _run.LogEntries.Add("Available routes:");

            foreach (NodeConnectionDefinition route in availableRoutes)
            {
                _run.LogEntries.Add(
                    $"- {route.DisplayName}: {route.Description} " +
                    $"({route.DaylightCost} daylight, {route.StaminaCost} stamina)"
                );
            }
        }

        private void PrintLog()
        {
            foreach (string entry in _run.LogEntries)
            {
                Debug.Log(entry);
            }
        }

        private void PrintFinalStats()
        {
            Debug.Log($"Day: {_run.DayIndex}");
            Debug.Log($"Stamina: {_run.Player.Stamina}");
            Debug.Log($"Warmth: {_run.Player.Warmth}");
            Debug.Log($"Morale: {_run.Player.Morale}");
            Debug.Log($"Daylight: {_run.Player.Daylight}");
            Debug.Log($"Water: {_run.Player.WaterUnits}");
            Debug.Log($"Food: {_run.Player.FoodUnits}");
            Debug.Log($"Complete: {_run.IsRunComplete}");
        }
    }
}