using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Norr.Models
{
    public enum RequestStatus
    {
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Approved")]
        Approved,

        [Display(Name = "Declined")]
        Declined,

        [Display(Name = "Cancelled")]
        Cancelled
    }
    public enum LeaveType
    {
        [Display(Name = "Vacation")]
        Vacation,

        [Display(Name = "Parental")]
        Parental,

        [Display(Name = "Sick")]
        Sick,

        [Display(Name = "Unpaid")]
        Unpaid,

        [Display(Name = "Other")]
        Other
    }
    public class Leave
    {       
        public int Id { get; set; }

        [Required]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }

        [Required]
        [DisplayName("Leave type")]
        public LeaveType Type { get; set; }

        [DisplayName("Request status")]
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
        
        [DisplayName("Request date")]
        public DateTime Time { get; set; } = DateTime.Now;
     
        public int EmployeeId { get; set; }
        [DisplayName("Name")]
        public Employee? Employee { get; set; }

    }
}
