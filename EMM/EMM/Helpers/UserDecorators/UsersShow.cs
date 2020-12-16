using LifeHacks;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Helpers.UserDecorators
{
    public class UsersShow : BaseUserDecorator
    {
        public UsersShow(User user) : base(user)
        {
        }
        public string ShowRate()
        {
            return String.Format("Тарифная ставка: {0}", rate);
        }
        public string ShowPosition()
        {
            return String.Format("Должность: {0}", position);
        }

    }
}
