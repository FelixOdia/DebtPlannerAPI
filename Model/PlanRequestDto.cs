namespace DebtPlannerAPI.Model
{
    public class PlanRequestDto
    {
        public User User { get; set; }
        public List<Debt> Debts { get; set; }
    }
}
