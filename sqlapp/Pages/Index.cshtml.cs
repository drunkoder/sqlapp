using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.Models;
using sqlapp.Services;

namespace sqlapp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;

        public void OnGet()
        {
            ProductService service = new ProductService();
            Products = service.GetProducts();
        }
    }
}