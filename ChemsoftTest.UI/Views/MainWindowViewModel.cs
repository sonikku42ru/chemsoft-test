using System.Windows.Input;
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
            FirstName = { Value = "lol2" },
            LastName = { Value = "lol" }
        }
    };
    
    private PersonUi _selectedPerson = new();

    private ICommand _addPersonCommand;

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

    public ICommand AddPersonCommand =>
        _addPersonCommand ??= new RelayCommand(_ => CreatePerson());
    
    private void CreatePerson()
    {
        var person = new PersonUi();
        Persons.Add(person);
        SelectedPerson = person;
    }
}