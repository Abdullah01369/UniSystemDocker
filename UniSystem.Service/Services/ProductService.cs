using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

        }

        public async Task<Response<List<ProductDto>>> ProductList()
        {

            var val = await _appDbContext.Products.Where(x => x.IsActive == true && x.IsSold == false).Include(x => x.Category).ToListAsync();

            var mapped = _mapper.Map<List<ProductDto>>(val);

            return Response<List<ProductDto>>.Success(mapped, 200);

        }

    }

}











