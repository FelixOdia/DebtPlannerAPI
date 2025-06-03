namespace DebtPlannerAPI.Model
{
    public class RepaymentPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DebtName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal InterestPaid { get; set; }
        public decimal PrincipalPaid { get; set; }
        public decimal RemainingBalance { get; set; }

        public User User { get; set; }
    }
}
