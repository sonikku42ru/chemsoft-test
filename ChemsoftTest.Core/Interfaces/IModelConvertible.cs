namespace ChemsoftTest.Core.Interfaces;

public interface IModelConvertible<TModel>
{
    public TModel ToModel();
}