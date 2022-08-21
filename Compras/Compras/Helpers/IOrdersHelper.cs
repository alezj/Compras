using Compras.Common;
using Compras.Models;

namespace Compras.Helpers
{
    public interface IOrdersHelper
    {
        Task<Response> ProcessOrderAsync(ShowCartViewModel model);
        Task<Response> CancelOrderAsync(int id);
    }

}
