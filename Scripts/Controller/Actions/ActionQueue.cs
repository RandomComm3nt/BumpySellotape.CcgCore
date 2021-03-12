using System;
using System.Collections.Generic;

namespace CcgCore.Controller.Actions
{
    public class ActionQueue
    {
        #region Data

        private List<QueuedAction> queuedActions;

        private bool inAction;

        #endregion

        #region Methods

        public ActionQueue()
        {
            queuedActions = new List<QueuedAction>();
            inAction = false;
        }

        public void AdvanceToNextAction()
        {
            if (queuedActions.Count == 0)
            {
                inAction = false;
                return;
            }

            inAction = true;
            //Action a = queuedActions[0];
            queuedActions.RemoveAt(0);
            //a.Invoke();
        }

        public void EnqueueAction(Action action)
        {
            //queuedActions.Add(action);
            if (!inAction)
                AdvanceToNextAction();
        }

        public void EnqueueActionWithAdvance(Action action)
        {
            Action combinedAction = () =>
            {
                action.Invoke();
                AdvanceToNextAction();
            };
            EnqueueAction(combinedAction);
        }

        #endregion
    }
}
