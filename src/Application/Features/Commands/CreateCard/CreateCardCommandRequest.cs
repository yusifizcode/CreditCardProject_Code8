using MediatR;
using TechnestHackhaton.Domain.Enums;

namespace TechnestHackhaton.Application.Features.Commands.CreateCard;

public class CreateCardCommandRequest : IRequest<CreateCardCommandResponse>
{
    public int Age { get; set; }
    public float Salary { get; set; }
    public HomeOwnership HomeOwnership { get; set; }
    public float EmploymentTime { get; set; }
    public LoanPurposes LoanPurposes { get; set; }
    public float CreditAmount { get; set; }
    public float LoanRate { get; set; }
    public bool CreditStatus { get; set; }
    public int LoanPercentage { get; set; }
    public int PaymentHistory { get; set; }
    public int CreditHistoryLength { get; set; }
}
