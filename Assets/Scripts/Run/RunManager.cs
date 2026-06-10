using UnityEngine;

namespace BeneathThePines
{
    public class RunManager : MonoBehaviour
    {
        public RunState CurrentRun { get; private set; }

        public void StartNewRun()
        {
            CurrentRun = RunFactory.CreateNewRun();
            Debug.Log("Run started.");
        }

        public void CompleteRun()
        {
            CurrentRun.IsRunComplete = true;
            Debug.Log("Run complete.");
        }

        public void FailRun()
        {
            CurrentRun.IsRunFailed = true;
            Debug.Log("Run failed.");
        }
    }
}
