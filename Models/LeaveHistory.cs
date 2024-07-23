namespace IT_Norr.Models
{
    public class LeaveHistory
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveId { get; set; }

        public Employee? Employee { get; set; }
        public Leave? Leave { get; set; }
        
    }
}
