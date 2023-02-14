namespace ChemsoftTest.Core.Interfaces;

public interface IEntityConvertible<TEntity>
{
    public TEntity ToEntity();
}