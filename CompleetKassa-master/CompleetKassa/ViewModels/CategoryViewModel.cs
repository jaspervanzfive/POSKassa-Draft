using CompleetKassa.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CompleetKassa.ViewModels
{
    public class CategoryViewModel : ViewModelBase<Category>
    {
        public override Category Model { get; set; }
        public CategoryViewModel(Category category)
        {
            this.Model = category;
        }
        public CategoryViewModel()
        {
            var rand = new Random();
            Model = new Category
            {
                Name = "Test",
                ProductImage = Properties.Resources.wea
             
            };
        }
     
        public string Name { get => Model.Name; set { Model.Name = value; OnPropertyChanged(); } }


        public Image ProductImage{ get => Model.ProductImage; set { Model.ProductImage = value; OnPropertyChanged(); } }
    }
}
