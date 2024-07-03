using System.Threading.Tasks;
using AddressBook.Models;
using AddressBook.Services;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AddressBook.ViewModels;

public partial class ContactViewModel : ViewModelBase
{
    private readonly HistoryRouter<ViewModelBase> _router;
    private readonly ContactService _contacts;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand), nameof(DeleteCommand))]
    private Contact? _contact = new();
    
    public ContactViewModel(HistoryRouter<ViewModelBase> router, ContactService contacts)
    {
        _router = router;
        _contacts = contacts;

        SaveCommand = new AsyncRelayCommand(Save, CanSave);
        DeleteCommand = new AsyncRelayCommand(Delete, CanDelete);
    }
    
    private bool CanSave() => !string.IsNullOrWhiteSpace(Contact?.Name);
    private bool CanDelete() => Contact?.Id != null;
    
    public IAsyncRelayCommand SaveCommand { get; }
    public IAsyncRelayCommand DeleteCommand { get; }

    [RelayCommand]
    public void CanExecuteChanged()
    {
        SaveCommand.NotifyCanExecuteChanged();
        DeleteCommand.NotifyCanExecuteChanged();
    }

    public async Task Save()
    {
        if (Contact.Id != null)
        {
            await _contacts.UpdateContact(Contact);
        }
        else
        {
            await _contacts.AddContact(Contact);
        }
        _router.Back();
    }

    public async Task Delete()
    {
        if (Contact.Id != null)
        {
            await _contacts.DeleteContact(Contact.Id.Value);
        }
        _router.Back();
    }

    [RelayCommand]
    public void Cancel()
    {
        _router.Back();
    }
}