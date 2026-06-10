using System.Collections.Generic;
using UnityEngine;

namespace BeneathThePines
{
    public class PrototypeLoopTester : MonoBehaviour
    {
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

            List<NodeDefinition> nodes = CreateTestNodes();
            _mapSystem = new MapSystem(nodes);

            Debug.Log("=== Beneath the Pines Prototype Test ===");

            RunTestLoop();

            PrintLog();
            PrintFinalStats();
        }

        private void RunTestLoop()
        {
            _mapSystem.MoveToNode("trail_01", _run);

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

            _mapSystem.MoveToNode("camp_01", _run);

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

            _nightSystem.ResolveNight(_run);

            _mapSystem.MoveToNode("destination", _run);
        }

        private List<NodeDefinition> CreateTestNodes()
        {
            return new List<NodeDefinition>
            {
                new NodeDefinition
                {
                    NodeId = "start",
                    DisplayName = "Trailhead",
                    NodeType = NodeType.Start,
                    NextNodeIds = new List<string> { "trail_01" }
                },
                new NodeDefinition
                {
                    NodeId = "trail_01",
                    DisplayName = "Muddy Forest Path",
                    NodeType = NodeType.Trail,
                    NextNodeIds = new List<string> { "camp_01" }
                },
                new NodeDefinition
                {
                    NodeId = "camp_01",
                    DisplayName = "Pine Hollow Camp",
                    NodeType = NodeType.Camp,
                    NextNodeIds = new List<string> { "destination" }
                },
                new NodeDefinition
                {
                    NodeId = "destination",
                    DisplayName = "Fire Tower Lookout",
                    NodeType = NodeType.Destination,
                    NextNodeIds = new List<string>()
                }
            };
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
