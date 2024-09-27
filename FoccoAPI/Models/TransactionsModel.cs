using FoccoAPI.Enum;
using System.Text.Json.Serialization;

namespace FoccoAPI.Models
{
    public class TransactionsModel
    {

        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public char Status { get; set; } = 'A'; 
        public CashFlowEnum CashFlow { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

