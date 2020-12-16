using EMM.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMM.Services
{
    public class PageTryCatcher: TryCatcher
    {
        protected readonly ICommandPage page;

        public PageTryCatcher(ICommandPage page)
        {
            this.page = page;
        }
        public PageTryCatcher(PageTryCatcher other)
        {
            page = other.page;
        }
    }
}
