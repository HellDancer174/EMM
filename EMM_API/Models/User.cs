using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace EMM_API.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        private string position;
        [DataMember]
        private double rate;
        [DataMember]
        private int qualificationClass;
        private IDictionary<int, int> qualificationPremiumPairs;

        public User(string position, double rate, int qualificationClass)
        {
            this.position = position;
            this.rate = rate;
            this.qualificationClass = qualificationClass;
            qualificationPremiumPairs = new Dictionary<int, int>() { { 1, 20 }, { 2, 10 }, { 3, 5 }, { 4, 0 }, { 5, 5 }, { 6, 0 } };
            if (this.qualificationClass > 6) qualificationClass = 6;
        }
        public int CreateQualificationPercent(int disel, int electro)
        {
            int percent = qualificationPremiumPairs[qualificationClass];
            if (disel >= 6 && electro >= 6 && (qualificationClass != 6 && qualificationClass != 4)) percent += 5;
            return percent;
        }
        public ApplicationUser RebuildAppUser(ApplicationUser user)
        {
            user.Position = position;
            user.Rate = rate;
            user.QualificationClass = qualificationClass;
            return user;
        }
    }
}
