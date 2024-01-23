using System;
using System.Collections.Generic;
using System.IO;
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

namespace IB_AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<IndividualCOntact> contactList = new List<IndividualCOntact>();
        Dictionary<int, string> thingToDisplay = new Dictionary<int, string>();
        FileManipulators fm = new FileManipulators();
        public MainWindow()
        {
            List<string> rawContact = new List<string>();
            string[] temp = new string[] { };

            InitializeComponent();

            contactList = new List<IndividualCOntact>();
            thingToDisplay = new Dictionary<int, string>();
            if(File.Exists("ContactList.csv"))
            {
                rawContact = fm.FileRead("ContactList.csv");
            }

            if(rawContact.Count > 0)
            {
                foreach(string contact in  rawContact)
                {
                    temp = contact.Split(',');
                    IndividualCOntact ic = new IndividualCOntact(
                        int.Parse(temp[0]), temp[1], temp[2], temp[3], temp[4]);
                    contactList.Add(ic);
                    thingToDisplay[int.Parse(temp[0])] = temp[1];
                }
                updateListBoxDisplay();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtbName.Text.Length == 0
                || txtbAdd.Text.Length == 0
                || txtbCNum.Text.Length == 0
                || txtbEAdd.Text.Length == 0) 
            {
                MessageBox.Show("All fields must be filled up.");
            }
            else
            {
                IndividualCOntact ic = new IndividualCOntact(
                    contactList.Count + 1, txtbName.Text, txtbAdd.Text
                    , txtbCNum.Text, txtbEAdd.Text);
                contactList.Add(ic);
                thingToDisplay[contactList.Count + 1] = txtbName.Text;
                updateListBoxDisplay();
                clearTextBox();
            }
        }

        private void updateListBoxDisplay()
        {
            // having selected index at -1 means nothing is selected
            // so the items clear will not have any conflicts with the selected index
            lbContactList.SelectedIndex = -1;
            lbContactList.Items.Clear();

            foreach(KeyValuePair<int, string> kvp in thingToDisplay)
            {
                lbContactList.Items.Add(kvp.Value);
            }
        }

        private void clearTextBox()
        {
            txtbName.Text = string.Empty;
            txtbAdd.Text = string.Empty;
            txtbCNum.Text = string.Empty;
            txtbEAdd.Text = string.Empty;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearTextBox();
            lbContactList.SelectedIndex = -1;
        }

        private void myWindow_Closed(object sender, EventArgs e)
        {
            List<string> contacts = new List<string>();
            foreach(IndividualCOntact ic in contactList)
            {
                contacts.Add(ic.icInfoDump());
            }

            fm.FileWrite(contacts, "ContactList.csv");
        }

        private void lbContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(lbContactList.SelectedValue.ToString());
        }
    }
}
