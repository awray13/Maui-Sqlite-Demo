namespace MauiSqliteDemo
{
    public partial class MainPage : ContentPage
    {
        // Define the local database service
        private readonly LocalDbService _dbService;

        // Define the edit customer id
        private int _editCustomerId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();

            // Initialize the local database service
            _dbService = dbService;

            // Get the list of customers
            Task.Run(async () => listView.ItemsSource = await _dbService.GetCustomers());
        }

        // Code to execute functionality when the save button is clicked
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (_editCustomerId == 0)
            {
                // Add new customer
                await _dbService.Create(new Customer
                {
                    CustomerName = nameEntryField.Text,
                    Mobile = mobileEntryField.Text,
                    Email = emailEntryField.Text
                });

            }
            else
            {
                // Edit existing customer details using Update method
                await _dbService.Update(new Customer
                {
                    Id = _editCustomerId,
                    CustomerName = nameEntryField.Text,
                    Mobile = mobileEntryField.Text,
                    Email = emailEntryField.Text
                });

                _editCustomerId = 0;

            }

            // Clear the entry fields
            nameEntryField.Text = string.Empty;
            mobileEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;

            // Refresh the list view
            listView.ItemsSource = await _dbService.GetCustomers();
        }

        // Code to execute functionality when an item is tapped
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the customer object
            var customer = (Customer)e.Item;

            // Displays an action sheet with options to edit or delete the customer
            var action = await DisplayActionSheet(
                "Action",
                "Cancel",
                null,
                "Edit",
                "Delete");

            // Switch case to handle the action
            switch (action)
            {
                // Edit the customer details
                case "Edit":
                    _editCustomerId = customer.Id;
                    nameEntryField.Text = customer.CustomerName;
                    mobileEntryField.Text = customer.Mobile;
                    emailEntryField.Text = customer.Email;
                    break;

                // Delete the customer
                case "Delete":
                    await _dbService.Delete(customer);
                    listView.ItemsSource = await _dbService.GetCustomers();
                    break;
            }
        }
    }
}


