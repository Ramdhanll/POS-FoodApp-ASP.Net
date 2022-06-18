using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FoodShop.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        
        public async Task<ServiceResponse<Product>> CreateProductAsync(ProductCreateVM product)
        {
            List<Category> categories = new List<Category>();
            
            var response = new ServiceResponse<Product>();

            // validate category
            foreach (var categoryId in product.CategoriesId)
            {
                var categoryExist = await _context.Categories.FindAsync(categoryId);
                if (categoryExist == null)
                {
                    response.Success = false;
                    response.Message = "Category not found!";
                    return response;
                }
                categories.Add(categoryExist);
            }
            
            // insert image
            var imagePath = "";

                try
                {
                    imagePath = uploadImage(product.Image);
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                    return response;
                }

            // add product and category
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Image = imagePath,
                Price = product.Price
            };

            // add data to table ProductCategory (pivot table)
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            // get product with include category and insert to ProductCategory
            var productsCategories = await _context.Products.Where(x => x.Id == newProduct.Id).Include(x => x.Categories).FirstOrDefaultAsync();

            foreach (var category in categories)
            {
                productsCategories.Categories.Add(category);
            }
            await _context.SaveChangesAsync();

            response.Data = productsCategories;
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var product = await _context.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found!"
                };
            }

            return new ServiceResponse<Product>
            {
                Success = true,
                Data = product
            };
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var products = await _context.Products.Include(x => x.Categories).ToListAsync();
            return new ServiceResponse<List<Product>>
            {
                Success = true,
                Data = products
            };
        }

        public string uploadImage(IFormFile file)
        {
                if (file != null || file.Length == 0)
                {
                Console.WriteLine("ada");
                    string fileName = file.FileName;
                    string extension = Path.GetExtension(fileName);

                    string[] allow = { ".jpeg", ".jpg", ".png" };

                    if (!allow.Contains(extension.ToLower()))
                    {
                        throw new Exception("Image must be png/jpg/jpeg");
                    }
                
                    // getting file original name
                    string FileName = file.FileName;

                    // combining GUID to create unique name before saving in wwwroot
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName.Trim();

                    // getting full path inside wwwroot/images
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", uniqueFileName);

                    // copying file
                    file.CopyTo(new FileStream(imagePath, FileMode.Create));

                    return $"images/{uniqueFileName}";
                } else
                {
                Console.WriteLine("gada");
                throw new Exception("Image not found");
                }
          
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Product not found!"
                };
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true
            };
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(ProductUpdateVM product)
        {
            var dbProduct = await _context.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == product.Id);
            var response = new ServiceResponse<Product>();
            List<Category> categories = new List<Category>();

            // validate product
            if (dbProduct == null)
            {
                response.Success = false;
                response.Message = "Product not found!";
                return response;
            }

            // validate category
            foreach (var categoryId in product.CategoriesId)
            {
                var categoryExist = await _context.Categories.FindAsync(categoryId);
                if (categoryExist == null)
                {
                    response.Success = false;
                    response.Message = "Category not found!";
                    return response;
                }
                categories.Add(categoryExist);
            }

            // upload image to server
            if(product.IsImageUpdate)
            {
                try
                {
                    if (product.Image == null || product.Image.Length == 0)
                    {
                        response.Success = false;
                        response.Message = "Image not found!";
                        return response;
                    }
                    
                    dbProduct.Image = uploadImage(product.Image);
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                    return response;
                }                
            }

            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;

            // update product
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();

            // delete productCategery
            dbProduct.Categories.Clear();

            // update pivot table ProductCategory
            foreach (var category in categories)
            {
                dbProduct.Categories.Add(category);
            }
            await _context.SaveChangesAsync();

            response.Data = dbProduct;
            response.Success = true;

            return response;

        }
    }
}
