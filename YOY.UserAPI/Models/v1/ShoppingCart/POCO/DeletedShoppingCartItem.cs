using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.ShoppingCart.POCO
{
    public class DeletedShoppingCartItem
    {
        [Required]
        [NotNull]
        [DataType(DataType.Text)]
        public string UserId { set; get; }
        [Required]
        [NotNull]
        public Guid ItemId { set; get; }
        [Required]
        [NotNull]
        public int ActionType { set; get; }

    }
}
