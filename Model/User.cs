﻿namespace DebtPlannerAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte [] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public decimal[] MonthlyIncome { get; set; }
    }
}
