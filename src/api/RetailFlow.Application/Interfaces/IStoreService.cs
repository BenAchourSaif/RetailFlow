
using RetailFlow.Application.DTOs.Stores;

namespace RetailFlow.Application.Interfaces
{
    public interface IStoreService
    {
        StoreResponse Create(CreateStoreRequest request);
    }
}
