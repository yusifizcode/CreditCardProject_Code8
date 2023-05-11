using TechnestHackhaton.Application.DTOs.Card;

namespace TechnestHackhaton.Application.Common.Interfaces;

public interface ICardOperationChecker
{
    public Task<int> CheckCart(CardInfoRequestObjectDto cardInfoRequestObjectDto, CardCreatorData cardCreatorData);
}
