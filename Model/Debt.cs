namespace DebtPlannerAPI.Model
{
    public class Debt
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MinimumPayment { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
