using TechnestHackhaton.Domain.Enums;

namespace TechnestHackhaton.Application.DTOs.Card;

public class CardInfoRequestObjectDto
{
    public float Salary { get; set; }
    public TechnestHackhaton.Domain.Enums.HomeOwnership HomeOwnership { get; set; }
    public float EmploymentTime { get; set; }
    public LoanPurposes LoanPurposes { get; set; }
    public float CreditAmount { get; set; }
}
