using ChemsoftTest.UI.Utils;
using ChemsoftTest.UI.Views.Base;
using ChemsoftTest.UI.Views.Models;

namespace ChemsoftTest.UI.Views;

public class MainWindowViewModel : BaseUiModel
{
    private ObservableRangeCollection<PersonUi> _persons = new()
    {
        new PersonUi()
        {
            FirstName = { Value = "lol" },
            LastName = { Value = "lol" }
        }
    };
    
    private PersonUi _selectedPerson = new();

    public ObservableRangeCollection<PersonUi> Persons
    {
        get => _persons;
        set
        {
            _persons = value;
            OnPropertyChanged();
        }
    }

    public PersonUi SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            
            _selectedPerson = value;
            OnPropertyChanged();
        }
    }
}