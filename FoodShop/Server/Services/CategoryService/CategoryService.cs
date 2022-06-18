namespace FoodShop.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<Category>> createCategoryAsync(CategoryCreateVM Category)
        {
            var response = new ServiceResponse<Category>();

            // insert image
            var imagePath = "";

            try
            {
                imagePath = uploadImage(Category.Icon);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                return response;
            }

            // add category
            var newCategory = new Category
            {
                Name = Category.Name,
                Icon = imagePath,
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            response.Data = newCategory;
            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Category not found!"
                };
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return new ServiceResponse<bool>
            {
                Success = true,
                Data = true
            };
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return new ServiceResponse<List<Category>>
            {
                Data = categories,
                Success = true
            };
        }

        public Task<ServiceResponse<Category>> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Category>> updateCategoryAsync(CategoryUpdateVM Category)
        {
            var dbCategory = await _context.Categories.FindAsync(Category.Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<Category>
                {
                    Success = false,
                    Message = "Category not found!"
                };
            }

            // update category
            dbCategory.Name = Category.Name;
            
            if (Category.IsIconUpdate)
            {
                // update image
                try
                {
                    dbCategory.Icon = uploadImage(Category.Icon);
                }
                catch (Exception e)
                {
                    return new ServiceResponse<Category>
                    {
                        Success = false,
                        Message = e.Message
                    };
                }
            }
            await _context.SaveChangesAsync();

            return new ServiceResponse<Category>
            {
                Data = dbCategory,
                Success = true
            };
        }

        public string uploadImage(IFormFile file)
        {
            if (file != null || file.Length == 0)
            {
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
            }
            else
            {
                throw new Exception("Image not found");
            }

        }
    }
}
