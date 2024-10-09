using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, string description, string slug, decimal price)
        {
            Title = title;
            Description = description;
            Slug = slug;
            IsActive = true;
            Price = price;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public bool IsActive { get; private set; }
        public decimal Price { get; private set; }
    }
}
