using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChemsoftTest.Core.Database.Repositories;
using ChemsoftTest.Core.Entities;
using ChemsoftTest.Core.Models;
using ChemsoftTest.UI.Utils;
using ChemsoftTest.UI.Views.Base;
using ChemsoftTest.UI.Views.Models;

namespace ChemsoftTest.UI.Views;

public class MainWindowViewModel : BaseUiModel
{
    private readonly PersonRepository _personRepository;
    
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

    public MainWindowViewModel(PersonRepository personRepository)
    {
        _personRepository = personRepository;
        Task.Run(async () =>
        {
            var added = await personRepository.AddRangeAsync(new PersonEntity[]
            {
                new()
                {
                    FirstName = "kek",
                    LastName = "kek",
                    Birthday = DateTime.Today,
                    Email = "kek@kek.kek",
                    PatronymicName = "keks"
                }
            });
            var found = personRepository.GetByIdAsync(added.ElementAt(0).Id);
        });
    }
    
    private void CreatePerson()
    {
        var person = new PersonUi();
        Persons.Add(person);
        SelectedPerson = person;
    }
}