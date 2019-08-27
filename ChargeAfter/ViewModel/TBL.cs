using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChargeAfter.Models;

namespace ChargeAfter.ViewModel
{
    public class TBL
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string lender { get; set; }
        public DateTime DOB { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public Nullable<int> ReqID { get; set; }
        public long UserID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? rDate { get; set; }
        public string Status { get; set; }
        public string EmpStatus { get; set; }
        public string HousingStatus { get; set; }
        public string GrossIndividual { get; set; }
        public string socialSecurity { get; set; }
        public string YearsAtResidance { get; set; }
        public string monthlyRent { get; set; }
        public string DriverLicense { get; set; }
        public string DRiveState { get; set; }
        public string NetIncom { get; set; }
        public string SalaryFrequency { get; set; }
        public long OffID { get; set; }

        public Nullable<decimal> ApprovedAmmount { get; set; }
        public Nullable<decimal> TotalPayback { get; set; }
        public Nullable<int> IncreasePercent { get; set; }
        public Nullable<decimal> MonthlyAmount { get; set; }
        public Nullable<int> MonthCount { get; set; }


        public string Price { get; set; }
        public int Id { get; set; }
        public long? ReqId { get; set; }


    }
}