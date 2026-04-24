using System.Text;
using System.Threading;
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

namespace FarmerWPF_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Farmer> farmers = new List<Farmer>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFarmerName.Text) &&
                !string.IsNullOrWhiteSpace(txtFarmerSurname.Text) &&
                !string.IsNullOrWhiteSpace(txtFarmCode.Text) &&
                !string.IsNullOrWhiteSpace(txtNumOfCrops.Text))
            {

                //Add a new student
                farmers.Add(new Farmer()
                {
                    FirstName = txtFarmerName.Text,
                    Surname = txtFarmerSurname.Text,
                    FarmCode = int.Parse(txtFarmCode.Text),
                    NoOfCrops = int.Parse(txtNumOfCrops.Text),
                });

                // Clear after successful save
                txtFarmerName.Text = "";
                txtFarmerSurname.Text = "";
                txtFarmCode.Text = "";
                txtNumOfCrops.Text = "";
                lstDisplay.Items.Add("Farmer added successfully");
            }
            else
            {
                // Display error messages after unsuccessful save
                if (string.IsNullOrWhiteSpace(txtFarmerName.Text))
                {
                    lstDisplay.Items.Add("Required: Enter the farmers first name");
                }
                else if (string.IsNullOrWhiteSpace(txtFarmerSurname.Text))
                {
                    lstDisplay.Items.Add("Required: Enter the farmers surname");
                }
                else if (string.IsNullOrWhiteSpace(txtFarmCode.Text))
                {
                    lstDisplay.Items.Add("Required: Enter the farmers farm code");
                }
                else if (string.IsNullOrWhiteSpace(txtNumOfCrops.Text))
                {
                    lstDisplay.Items.Add("Required: Enter the number of crops on the farmers farm");
                }
            }
        }

        private void btnShowResults_Click(object sender, RoutedEventArgs e)
        {
            //Disable button during typing effect
            btnShowResults.IsEnabled = false;

            lstDisplay.Items.Clear();

            Task.Run(() =>
            {
                //Simulate typing effect for "Farmers:"
                string headerText = "Farmers:";
                foreach (char c in headerText)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (lstDisplay.Items.Count == 0)
                        {
                            lstDisplay.Items.Add(c.ToString());
                        }
                        else
                        {
                            string lastItem = lstDisplay.Items[lstDisplay.Items.Count - 1].ToString();
                            lstDisplay.Items[lstDisplay.Items.Count - 1] = lastItem + c;
                        }
                    });
                    Thread.Sleep(50); //Typing delay
                }

                Thread.Sleep(200); //Pause after header

                // Typing effect for each farmer
                foreach (var farmer in farmers)
                {
                    string farmerText = $"{farmer.FirstName} : {farmer.Surname} : {farmer.FarmCode} : {farmer.NoOfCrops}";

                    Dispatcher.Invoke(() =>
                    {
                        lstDisplay.Items.Add(""); //Add empty item to type into
                    });

                    foreach (char c in farmerText)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            int lastIndex = lstDisplay.Items.Count - 1;
                            string currentText = lstDisplay.Items[lastIndex].ToString();
                            lstDisplay.Items[lastIndex] = currentText + c;
                        });
                        Thread.Sleep(30); //Typing delay
                    }

                    Thread.Sleep(100); //Pause between farmers
                }

                // Re-enable button when done
                Dispatcher.Invoke(() =>
                {
                    btnShowResults.IsEnabled = true;
                });
            });
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFarmerName.Text = "";
            txtFarmerSurname.Text = "";
            txtFarmCode.Text = "";
            txtNumOfCrops.Text = "";

            //Clear the listbox too
            lstDisplay.Items.Clear();

            //Cursor go back to farmer name
            txtFarmerName.Focus();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}