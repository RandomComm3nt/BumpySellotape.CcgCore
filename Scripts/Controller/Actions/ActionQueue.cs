using System;
using System.Collections.Generic;

namespace CcgCore.Controller.Actions
{
    public class ActionQueue
    {
        #region Data

        private List<Action> queuedActions;

        private bool inAction;
        private bool isWaiting;
        private List<object> busyProcesses;

        #endregion

        #region Methods

        public ActionQueue()
        {
            queuedActions = new List<Action>();
            inAction = false;
        }

        private void AdvanceToNextAction()
        {
            isWaiting = false;
            if (queuedActions.Count == 0)
            {
                inAction = false;
                return;
            }

            busyProcesses = new List<object>();
            inAction = true;
            Action a = queuedActions[0];
            queuedActions.RemoveAt(0);
            a.Invoke();
            // if UI has not told us it is doing something, advance immediately
            if (busyProcesses.Count == 0)
                AdvanceToNextAction();
            else
                isWaiting = true;
        }

        public void EnqueueAction(Action action)
        {
            queuedActions.Add(action);
            if (!inAction)
                AdvanceToNextAction();
        }

        public void PauseProcessing(object process)
        {
            if (!inAction)
                return;
            busyProcesses.Add(process);
        }

        public void ResumeProcessing(object process)
        {
            if (!inAction)
                return;
            busyProcesses.Remove(process);
            if (busyProcesses.Count == 0)
                AdvanceToNextAction();
        }

        #endregion
    }
}
