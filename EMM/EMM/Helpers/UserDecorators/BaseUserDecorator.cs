using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Helpers.UserDecorators
{
    public class BaseUserDecorator : User
    {
        protected readonly User user;

        public BaseUserDecorator(User user) : base(user)
        {
            this.user = user;
        }
    }
}
