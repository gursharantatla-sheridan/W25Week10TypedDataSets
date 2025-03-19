using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace W25Week10TypedDataSets;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // table adapter
    NorthwindDataSetTableAdapters.ProductsTableAdapter adpProducts = new NorthwindDataSetTableAdapters.ProductsTableAdapter();

    // data table
    NorthwindDataSet.ProductsDataTable tblProducts = new NorthwindDataSet.ProductsDataTable();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void GetAllProducts()
    {
        // Fill()
        //adpProducts.Fill(tblProducts);

        // Get()
        tblProducts = adpProducts.GetProducts();

        grdProducts.ItemsSource = tblProducts;
    }

    private void btnLoadAllProducts_Click(object sender, RoutedEventArgs e)
    {
        GetAllProducts();
    }

    private void btnFind_Click(object sender, RoutedEventArgs e)
    {
        int id = int.Parse(txtId.Text);
        var row = tblProducts.FindByProductID(id);

        if (row != null)
        {
            txtName.Text = row.ProductName;
            txtPrice.Text = row.UnitPrice.ToString();
            txtQuantity.Text = row.UnitsInStock.ToString();
        }
        else
        {
            txtName.Text = txtPrice.Text = txtQuantity.Text = "";
            MessageBox.Show("Invalid ID. Please try again");
        }
    }

    private void btnInsert_Click(object sender, RoutedEventArgs e)
    {
        string name = txtName.Text;
        decimal price = decimal.Parse(txtPrice.Text);
        short quantity = short.Parse(txtQuantity.Text);

        adpProducts.Insert(name, price, quantity);

        GetAllProducts();
        MessageBox.Show("New product added");
    }

    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
        int id = int.Parse(txtId.Text);
        string name = txtName.Text;
        decimal price = decimal.Parse(txtPrice.Text);
        short quantity = short.Parse(txtQuantity.Text);

        adpProducts.Update(name, price, quantity, id);

        GetAllProducts();
        MessageBox.Show("Product updated");
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        int id = int.Parse(txtId.Text);

        adpProducts.Delete(id);

        GetAllProducts();
        MessageBox.Show("Product deleted");
    }
}