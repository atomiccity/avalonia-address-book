using System.Threading.Tasks;
using AddressBook.Models;
using AddressBook.Services;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AddressBook.ViewModels;

public partial class ContactDetailsViewModel : ViewModelBase
{
    private readonly HistoryRouter<ViewModelBase> _router;
    private readonly ContactService _contacts;

    [ObservableProperty] private Contact _contact;
    
    public IAsyncRelayCommand DeleteCommand { get; }

    public ContactDetailsViewModel(HistoryRouter<ViewModelBase> router, ContactService contacts)
    {
        _router = router;
        _contacts = contacts;
        
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    [RelayCommand]
    public void Cancel()
    {
        _router.Back();
    }

    [RelayCommand]
    public void Edit()
    {
        var cvm = _router.GoTo<ContactViewModel>();
        cvm.Contact = Contact;
    }

    public async Task Delete()
    {
        if (Contact.Id != null)
        {
            await _contacts.DeleteContact(Contact.Id.Value);
        }
        _router.Back();
    }
}