using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {
        public PageProduct()
        {
            InitializeComponent();
            
            option3.ItemsSource = Base.BD.Product.ToList();
            option3.SelectedValuePath = "ID";
            List<ProductType> pt = Base.BD.ProductType.ToList();
            ProductType productType = new ProductType()
            {
                Title = "Все типы"
            };
            pt.Insert(0, productType);
            Filt.ItemsSource = pt;
            Filt.SelectedValuePath = "ID";
            Filt.DisplayMemberPath = "Title";
            Filt.SelectedIndex = 0;
            Sort.SelectedIndex = 0;
            


        }
        public void Filter()
        {
            List<Product> products = Base.BD.Product.ToList();
            if (!String.IsNullOrEmpty(Poisk.Text))
            {
                products = products.Where(x => x.Title.ToLower().Contains(Poisk.Text.ToLower())).ToList();
                
            }

            switch (Sort.SelectedIndex)
            {
                case 1:
                    products = products.OrderBy(x => x.ProductionPersonCount).ToList();
                   
                    break;
                case 2:
                    products = products.OrderByDescending(x => x.ProductionPersonCount).ToList();
                    break;

            }
            
            if(Filt.SelectedIndex !=0)
            {
                products = products.Where(x => x.ProductTypeID == Convert.ToInt32(Filt.SelectedValue)).ToList();
            }

            option3.ItemsSource = products;

        }
        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        

        private void image_Loaded(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32((sender as Image).Uid);
            Product product = Base.BD.Product.FirstOrDefault(x => x.ID==id);
            if(!String.IsNullOrEmpty(product.Image))
            {
                string Path = Environment.CurrentDirectory.Replace("bin\\Debug",$"img\\{product.Image}");
                (sender as Image).Source = new BitmapImage(new Uri(Path));
            }
            else
            {
                string Path = Environment.CurrentDirectory.Replace("bin\\Debug", $"img\\imgg.png");
                (sender as Image).Source = new BitmapImage(new Uri(Path));
            }

        }

        private void Filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void option3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
