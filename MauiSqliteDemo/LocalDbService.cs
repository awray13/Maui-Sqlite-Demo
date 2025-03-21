﻿using SQLite;

namespace MauiSqliteDemo
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        //instantiate the connection object
        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Customer>();
        }

        // Get all customers
        public async Task<List<Customer>> GetCustomers()
        {
            return await _connection.Table<Customer>()
                .ToListAsync();
        }

        // Get a customer by id
        public async Task<Customer> GetById(int id)
        {
            return await _connection.Table<Customer>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        // Create a new customer
        public async Task Create(Customer customer)
        {
            await _connection.InsertAsync(customer);
        }

        // Update an existing customer
        public async Task Update(Customer customer)
        {
            await _connection.UpdateAsync(customer);
        }

        // Delete a customer
        public async Task Delete(Customer customer)
        {
            await _connection.DeleteAsync(customer);
        }
    }
}
