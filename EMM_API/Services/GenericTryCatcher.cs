using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMM_API.Services
{
    public class GenericTryCatcher<T>: TryCatcher
    {
        public GenericTryCatcher()
        {
        }

        public virtual T Execute(Func<T> func)
        {
            T result = default(T);
            try
            {
                result = func.Invoke();
            }
            catch
            {
                result = ReFunction();
            }
            return result;

        }
        public virtual async Task<T> ExecuteAsync(Func<Task<T>> func)
        {
            T result = default(T);
            try
            {
                result = await func.Invoke();
            }
            catch
            {
                result = ReFunction();
            }
            return result;
        }


        protected virtual T ReFunction()
        {
            throw new InvalidOperationException();
        }

    }
}
