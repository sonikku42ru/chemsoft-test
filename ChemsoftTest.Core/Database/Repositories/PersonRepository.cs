using ChemsoftTest.Core.Entities;

namespace ChemsoftTest.Core.Database.Repositories;

public class PersonRepository : BaseRepository<PersonEntity>
{
    public PersonRepository(AppDbContext context) : base(context) { }
}