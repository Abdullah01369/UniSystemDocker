using SharedLayer.Dtos;
using UniSystem.Core.DTOs;

namespace UniSystem.Core.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> ProductList();

    }
}
