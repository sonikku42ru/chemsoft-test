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

    public async Task<ObservableRangeCollection<PersonUi>> GetAllPeople()
    {
        IQueryable<PersonEntity> entities = null;
        await Task.Run(() =>
        {
            entities = _personRepository.GetAll();
        });
        return entities != null 
            ? entities.ToCollection() 
            : new ObservableRangeCollection<PersonUi>();
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