using System.ComponentModel.DataAnnotations;

namespace IT_Norr.Models{
  
    public class Employee
    {       
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; }

        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
        public ICollection<LeaveHistory> LeaveHistories { get; set; } =new List<LeaveHistory>();    
        
        
    }
}
