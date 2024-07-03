using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AddressBook.Models;
using AddressBook.Services;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using DynamicData;

namespace AddressBook.ViewModels;

public partial class ContactListViewModel : ViewModelBase
{
    private readonly HistoryRouter<ViewModelBase> _router;
    private readonly ContactService _contacts;

    public ContactListViewModel(HistoryRouter<ViewModelBase> router, ContactService contacts)
    {
        _router = router;
        _contacts = contacts;
    }

    public ObservableCollection<Contact> Contacts { get; } = [];
    
    [RelayCommand]
    private async Task Load()
    {
        Contacts.Clear();
        Contacts.Add(await _contacts.GetContacts());
    }

    [RelayCommand]
    private void AddContact()
    {
        var cvm = _router.GoTo<ContactViewModel>();
    }

    [RelayCommand]
    private void EditContact(Contact item)
    {
        var cvm = _router.GoTo<ContactDetailsViewModel>();
        cvm.Contact = item;
    }
}