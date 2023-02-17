using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChemsoftTest.Core.DataHandlers;
using ChemsoftTest.Core.Models;
using ChemsoftTest.Core.Models.Base;
using ChemsoftTest.Core.Utils;
using ChemsoftTest.UI.Localization;
using ChemsoftTest.UI.Utils;
using ChemsoftTest.UI.Views.Modals;

namespace ChemsoftTest.UI.Views;

public class MainWindowViewModel : BaseUiModel
{
    private const int DbUpdateTimeout = 1000;
    
    private readonly PersonDataHandler _personDataHandler;
    
    private ObservableRangeCollection<PersonUi> _people = new();
    private ObservableRangeCollection<PersonUi> _filteredPeople = new();
    private string _filter = string.Empty;
    private bool _loading = true;
    private PersonUi _selectedPerson = new();
    
    private ICommand _addPersonCommand;
    private ICommand _loadPeopleCommand;
    private ICommand _deletePersonCommand;
    private ICommand _saveChangesCommand;

    public ObservableRangeCollection<PersonUi> FilteredPeople
    {
        get => _filteredPeople;
        set
        {
            _filteredPeople = value;
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

    public string Filter
    {
        get => _filter;
        set
        {
            _filter = value;
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddPersonCommand =>
        _addPersonCommand ??= new RelayCommand(_ => CreatePerson());

    public ICommand LoadPeopleCommand =>
        _loadPeopleCommand ??= new RelayCommand(_ => Task.Run(LoadPeopleAsync));

    public ICommand DeletePersonCommand =>
        _deletePersonCommand ??= new RelayCommand(_ => DeletePerson());

    public ICommand SaveChangesCommand =>
        _saveChangesCommand ??= new RelayCommand(_ => Task.Run(SaveChangesAsync));

    public event EventHandler CloseRequested;

    public MainWindowViewModel(PersonDataHandler personDataHandler)
    {
        _personDataHandler = personDataHandler;
    }
    
    private void CreatePerson()
    {
        var person = new PersonUi();
        _people.Add(person);
        FilteredPeople.Add(person);
        SelectedPerson = person;
    }

    private void ApplyFilter()
    {
        FilteredPeople = _people
            .Where(i => 
                i.Email.Value.Contains(_filter) ||
                i.FirstName.Value.Contains(_filter) ||
                i.PatronymicName.Value.Contains(_filter) ||
                i.LastName.Value.Contains(_filter))
            .ToObservableRangeCollection();
        OnPropertyChanged(nameof(FilteredPeople));
    }

    private async Task LoadPeopleAsync()
    {
        Loading = true;
        if (_personDataHandler.Connected)
        {
            var result = await _personDataHandler.GetAllPeople();
            _people = result.Value;
            ApplyFilter();
            Loading = false;
        }
        else
        {
            AlertWindow.Show(MainWindowStrings.AlertErrorTitle, MainWindowStrings.AlertNoDatabase);
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DeletePerson()
    {
        var toDelete = SelectedPerson;
        var index = _people.IndexOf(SelectedPerson);
        _people.Remove(SelectedPerson);
        SelectedPerson = index < _people.Count ? _people[index] : null;
        Task.Run(() => _personDataHandler.DeleteAsync(toDelete));
    }

    private async Task SaveChangesAsync()
    {
        Loading = true;
        if (_people.All(i => i.Valid))
        {
            await _personDataHandler.UpdateRangeAsync(_people);
            await LoadPeopleAsync();
        }
        else
        {
            Loading = false;
            AlertWindow.Show(
                MainWindowStrings.AlertValidationErrorTitle,
                MainWindowStrings.AlertValidationErrorOnSave);
        }
        Loading = false;
    }
}