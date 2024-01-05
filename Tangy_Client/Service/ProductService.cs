using Tangy_Client.Service.IService;
using Tangy_Models;

namespace Tangy_Client.Service
{
    public class ProductService : IProductService
    {
        public Task<ProductDTO> Get(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
