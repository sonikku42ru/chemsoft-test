using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChemsoftTest.Core.Database.Repositories;
using ChemsoftTest.Core.Entities;
using ChemsoftTest.Core.Models;
using ChemsoftTest.Core.Models.Base;
using ChemsoftTest.Core.Utils;

namespace ChemsoftTest.Core.DataHandlers;

public class PersonDataHandler : BaseUiModel
{
    private readonly PersonRepository _personRepository;
    
    public PersonDataHandler(PersonRepository repository)
    {
        _personRepository = repository;
    }

    public bool Connected => _personRepository.Connected;
    
    public async Task<Result<ObservableRangeCollection<PersonUi>, bool>> GetAllPeople()
    {
        IQueryable<PersonEntity> entities = null;
        return await Task.Run(() =>
        {
            try
            {
                entities = _personRepository.GetAll();
            }
            catch (Exception)
            {
                return new Result<ObservableRangeCollection<PersonUi>, bool>(
                    new ObservableRangeCollection<PersonUi>(), 
                    false);
            }

            return new Result<ObservableRangeCollection<PersonUi>, bool>(
                entities.ToCollection(), 
                true);
        });
    }

    public async Task<Result<PersonUi, bool>> AddAsync(PersonUi person)
    {
        var entity = person.ToEntity();
        var added = await _personRepository.AddOrUpdateAsync(entity);
        return added != default 
            ? new Result<PersonUi, bool>(added.ToUiModel(), true) 
            : new Result<PersonUi, bool>(null, false);
    }

    public async Task<Result<IEnumerable<PersonUi>, bool>> AddRangeAsync(IEnumerable<PersonUi> people)
    {
        var entities = people.ToEntities();
        var added = await _personRepository.AddRangeAsync(entities);
        return added != null
            ? new Result<IEnumerable<PersonUi>, bool>(added.ToCollection(), true)
            : new Result<IEnumerable<PersonUi>, bool>(new ObservableRangeCollection<PersonUi>(), false);
    }

    public async Task<bool> UpdateAsync(PersonUi person)
    {
        var entity = person.ToEntity();
        var updated = await _personRepository.AddOrUpdateAsync(entity, entity.Id);
        return updated != null;
    }

    public async Task UpdateRangeAsync(IEnumerable<PersonUi> models)
    {
        var entities = models.ToEntities();
        await _personRepository.AddOrUpdateRangeAsync(entities);
    }

    public async Task<bool> DeleteAsync(PersonUi person) =>
        await _personRepository.DeleteAsync(person.ToEntity());

    public async Task<bool> DeleteRangeAsync(IEnumerable<PersonUi> people) =>
        await _personRepository.DeleteRangeAsync(people.ToEntities());
}