using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EMM_API.Landing
{
    public class LandingModel
    {
        public string Time { get; private set; } = DateTime.Now.ToString();
        public LandingModel()
        {
            Update();
        }
        private async Task Update()
        {
            await Task.Run(() =>
            {
                Time = DateTime.Now.ToString();
            });
        }
    }
}