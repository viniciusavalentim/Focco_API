using FoccoAPI.Enum;

namespace FoccoAPI.Models
{
    public class TransactionsModel
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public CashFlowEnum CashFlow { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

