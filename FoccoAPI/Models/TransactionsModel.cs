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
        //'A' == Ativo, 'X' == Excluido, 'I' == inativo, 'F' == Finalizado
        public char Status { get; set; } = 'A'; 
        public CashFlowEnum CashFlow { get; set; }
        public RecurrenceTypeEnum RecurrenceType { get; set; }
        //'T' == Valor Total, 'P' == Valor Parcela
        public int MonthlyQuantityIntallmentRepeat { get; set; }
        public char ValueType { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

