using Domain.Entities;

namespace Domain.Interfaces;

public interface IAnalisarVideoService
{
    Task AnalisarAsync(string id);
}
