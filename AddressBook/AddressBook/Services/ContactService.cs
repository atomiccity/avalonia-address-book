using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AddressBook.Models;
using SQLite;

namespace AddressBook.Services;

public class ContactService
{
    private static readonly string DatabasePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AddressBook.db");

    private readonly SQLiteAsyncConnection _db = new SQLiteAsyncConnection(DatabasePath);

    public ContactService()
    {
        _db.CreateTableAsync<Contact>();
    }

    public async Task<int> AddContact(Contact contact)
    {
        return await _db.InsertAsync(contact);
    }

    public async Task<int> DeleteContact(int id)
    {
        return await _db.DeleteAsync<Contact>(id);
    }

    public async Task<int> UpdateContact(Contact contact)
    {
        return await _db.UpdateAsync(contact);
    }

    public async Task<List<Contact>> GetContacts()
    {
        return await _db.Table<Contact>().ToListAsync();
    }
}