using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public enum RowTriggers
    {
        Load,Add, Modify
    }
    public enum RowStates
    {
        Loaded, Added, Modified
    }
    public class DataStateMachine:Stateless.StateMachine<RowTriggers, RowStates>
    {
        public DataStateMachine(Func<RowTriggers> stateAccessor, Action<RowTriggers> stateMutator) : base(stateAccessor, stateMutator)
        {

        }

        public DataStateMachine(RowTriggers initialState) : base(initialState)
        {

        }
    }
}
