using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.Web.Constants
{
    public class ProductConstant
    {
        public const string SuccessMessage = "Product created successfully.";
        public const string SuccessUpdateMessage = "Product updated successfully."; 
        public const string SuccessDeleteMessage = "Product deleted successfully.";
        public const int SuccessCode = 200;

        public const string ErrorMessage = "Error creating product.";
        public const int ErrorCode = 500;

        public const string BadRequestMessage = "Product does not exist.";
        public const int BadRequesCode = 400;
    }
}
