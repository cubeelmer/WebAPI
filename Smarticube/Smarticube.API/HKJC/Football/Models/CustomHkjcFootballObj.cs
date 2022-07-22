using Smarticube.API.Utils;

namespace Smarticube.API.HKJC.Football.Models
{
    public class CustomHkjcFootballObj:HkjcDataPool
    {
        public string? Url { get; set; }
        public string? Matchid { get; set; }

        public string? CombintedKey { get {
                return this.Weekday.Trim()
                + this.Matchtype.Trim()
                + this.Matchname.Trim()
                + this.Matchdate.Trim(); } }

        public DateTime? Matchdt { get { return GetMatchDate(base.Matchdate); } }

        public decimal NoOfHours
        {
            get
            {
                return DateTimeExtension.GetNoOfHours(DateTimeExtension.GetChinaStandardDatetime(), this.Matchdt.Value);
            }
        }


        private DateTime? GetMatchDate(string matchDate)
        {
            DateTime? result = null;

            try
            {
                DateTime currentDate = DateTimeExtension.GetChinaStandardDatetime();
                int targetYear = currentDate.Year;
                //int currentMonth = currentDate.Month;

                List<string> l1 = matchDate.Trim().Split(' ').ToList();
                List<string> l1_1 = l1[0].Trim().Split('/').ToList();
                List<string> l1_2 = l1[1].Trim().Split(':').ToList();

                if (currentDate.Month.Equals(12) && Int32.Parse(l1_1[1]).Equals(1))
                {
                    targetYear = currentDate.Year + 1;
                }

                result = new DateTime(targetYear, Int32.Parse(l1_1[1]), Int32.Parse(l1_1[0]), Int32.Parse(l1_2[0]), Int32.Parse(l1_2[1]), 0);
            }
            catch (Exception ex)
            {

            }


            return result;

        }
       

    }
}
