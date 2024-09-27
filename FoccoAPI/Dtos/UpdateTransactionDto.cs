using FoccoAPI.Enum;

namespace FoccoAPI.Dtos
{
    public class UpdateTransactionDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public CashFlowEnum CashFlow { get; set; }

    }
}
