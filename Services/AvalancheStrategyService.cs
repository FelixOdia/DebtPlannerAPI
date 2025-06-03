using DebtPlannerAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DebtPlannerAPI.Services
{
    public class AvalancheStrategyService
    {
        public List<RepaymentPlan> GeneratePlan(User user, List<Debt> debts)
        {
            var plans = new List<RepaymentPlan>();
            var monthlyIncome = user.MonthlyIncome.FirstOrDefault();

            if (monthlyIncome <= 0)
                return plans; // We'll just use the first value for now
            var currentDate = DateTime.Now;

            // Clone the debts list to avoid modifying original
            var debtList = debts.Select(d => new Debt
            {
                Name = d.Name,
                Balance = d.Balance,
                InterestRate = d.InterestRate,
                MinimumPayment = d.MinimumPayment,
                User = d.User
            }).ToList();

            // Keep looping until all debts are paid off
            while (debtList.Any(d => d.Balance > 0))
            {
                // Select the unpaid debt with the highest interest rate
                var highest = debtList
                    .Where(d => d.Balance > 0)
                    .OrderByDescending(d => d.InterestRate)
                    .FirstOrDefault();

                if (highest == null) break;

                // Calculate monthly interest
                var interestPaid = highest.Balance * (highest.InterestRate / 12);
                var principalPaid = monthlyIncome - interestPaid;

                // Cap the total payment to remaining debt + interest (avoid overpaying)
                var totalPayment = Math.Min(monthlyIncome, highest.Balance + interestPaid);

                // Add this month's plan
                plans.Add(new RepaymentPlan
                {
                    UserId = user.Id,
                    DebtName = highest.Name,
                    PaymentDate = currentDate,
                    PaymentAmount = totalPayment,
                    InterestPaid = interestPaid,
                    PrincipalPaid = totalPayment - interestPaid,
                    RemainingBalance = highest.Balance - (totalPayment - interestPaid),
                    User = user
                });

                // Update balance for next month
                highest.Balance -= (totalPayment - interestPaid);
                if (highest.Balance < 0) highest.Balance = 0;

                // Go to next month
                currentDate = currentDate.AddMonths(1);
            }

            return plans;
        }
    }
}
