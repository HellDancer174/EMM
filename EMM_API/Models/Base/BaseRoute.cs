using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMM_API.Models
{
    public class BaseRoute
    {
        private int id;
        private DateTime start;
        private DateTime finish;
        private List<BaseLocomotive> locomotives;
        private List<BaseTrain> trains;
        private List<BaseStation> stations;
        private List<BasePassanger> passangers;
        private string comment;
        public BaseRoute()
        {
            this.locomotives = new List<BaseLocomotive>();
            this.trains = new List<BaseTrain>();
            this.stations = new List<BaseStation>();
            this.passangers = new List<BasePassanger>();
            this.id = 0;
        }
        public BaseRoute(int id, DateTime start, DateTime finish, IEnumerable<BaseLocomotive> locomotives, IEnumerable<BaseTrain> trains, IEnumerable<BaseStation> stations, IEnumerable<BasePassanger> passangers, string comment)
        {
            this.id = id;
            this.start = start;
            this.finish = finish;
            this.locomotives = new List<BaseLocomotive>(locomotives);
            this.trains = new List<BaseTrain>(trains);
            this.stations = new List<BaseStation>(stations);
            this.passangers = new List<BasePassanger>(passangers);
            this.comment = comment;
        }

        public void ToAway(Action<DateTime, DateTime, IEnumerable<BaseLocomotive>, IEnumerable<BasePassanger>, IEnumerable<BaseTrain>, IEnumerable<BaseStation>, string> instructionsForMe)
        {
            instructionsForMe(start, finish, locomotives, passangers, trains, stations, comment);
        }
        public TResult ToAway<TResult>(Func<int, DateTime, DateTime, IEnumerable<BaseLocomotive>, IEnumerable<BasePassanger>, IEnumerable<BaseTrain>, IEnumerable<BaseStation>, string, TResult> instructions)
        {
            return instructions(id, start, finish, locomotives, passangers, trains, stations, comment);
        }

    }
}