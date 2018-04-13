using CompleetKassa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CompleetKassa.ViewModels
{
    public class MainWindowViewModel
    {
        private List<Category> categories = new List<Category>
        {
            new Category
            {
                Name = "Amazing Bags for only $100",
                ProductImage = Properties.Resources.wea
       
            },
            new Category
            {
                Name = "Milk",
                ProductImage = Properties.Resources.wea
            },
            new Category
            {
                Name = "Planes",
                ProductImage = Properties.Resources.wea
            }
        };
        public ObservableViewModelCollection<CategoryViewModel, Category> CategoryViewModels { get; }
        public MainWindowViewModel()
        {
            CategoryViewModels = new ObservableViewModelCollection<CategoryViewModel, Category>(categories, c => new CategoryViewModel(c));
        }
    }
}
