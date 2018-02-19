namespace demo.framework.Utils.Entities
{
    internal class SalaryData
    {
        public int WorkDays { get; }
        public int SickDays { get; }
        public int OverDays { get; }
        public string Month { get; }
        public int IsPrivilegy { get; }

        public SalaryData(int workDays, int sickDays, int overDays, string month, bool isPrivilegy)
        {
            WorkDays = workDays;
            SickDays = sickDays;
            OverDays = overDays;
            Month = month;
            IsPrivilegy = isPrivilegy ? 1 : 0;
        }
    }
}
