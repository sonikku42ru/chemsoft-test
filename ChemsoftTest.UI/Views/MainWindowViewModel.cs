using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChemsoftTest.Core.Database.Repositories;
using ChemsoftTest.UI.Utils;
using ChemsoftTest.UI.Views.Base;
using ChemsoftTest.UI.Views.Models;

namespace ChemsoftTest.UI.Views;

public class MainWindowViewModel : BaseUiModel
{
    private readonly PersonRepository _personRepository;
    
    private ObservableRangeCollection<PersonUi> _people = new();

    private string _filter = "";
    
    private PersonUi _selectedPerson = new();

    private ICommand _addPersonCommand;
    private ICommand _loadPeopleCommand;

    public ObservableRangeCollection<PersonUi> People => _people
        .Where(i => 
            i.Email.Value.Contains(_filter) ||
            i.FirstName.Value.Contains(_filter) ||
            i.PatronymicName.Value.Contains(_filter) ||
            i.LastName.Value.Contains(_filter))
        .ToObservableRangeCollection();

    public PersonUi SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            _selectedPerson = value;
            OnPropertyChanged();
        }
    }

    public string Filter
    {
        get => _filter;
        set
        {
            _filter = value;
            OnPropertyChanged(nameof(People));
        }
    }

    public ICommand AddPersonCommand =>
        _addPersonCommand ??= new RelayCommand(_ => CreatePerson());

    public ICommand LoadPeopleCommand =>
        _loadPeopleCommand ??= new RelayCommand(_ => Task.Run(LoadPeopleAsync));

    public MainWindowViewModel(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    private void CreatePerson()
    {
        var person = new PersonUi();
        _people.Add(person);
        OnPropertyChanged(nameof(People));
        SelectedPerson = person;
    }

    private async Task LoadPeopleAsync()
    {
        ObservableRangeCollection<PersonUi> people = new();
        await Task.Run(() =>
        {
            people = _personRepository
                .GetAll()
                .ToCollection();
        });
        _people = people;
        OnPropertyChanged(nameof(People));
    }
}