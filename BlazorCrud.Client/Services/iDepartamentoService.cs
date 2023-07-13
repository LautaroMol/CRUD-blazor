using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface iDepartamentoService
    {
        Task<List<DepartamentoDTO>> Lista();

    }
}
