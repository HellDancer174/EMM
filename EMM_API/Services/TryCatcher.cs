using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM_API.Services
{
    public class TryCatcher
    {

        public virtual async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            catch
            {
                Reaction();
            }
        }
        public virtual void Execute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch
            {
                Reaction();
            }

        }

        protected virtual void Reaction()
        {
            throw new InvalidOperationException();
        }

    }
}
