using bookstoreapp.blazor.webassembly.ui.Services.Base;

namespace bookstoreapp.blazor.webassembly.ui.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> Get();
        Task<Response<AuthorDetailsDto>> Get(int id);
        Task<Response<AuthorUpdateDto>> GetForUpdate(int id);
        Task<Response<int>> Create(AuthorCreateDto author);
        Task<Response<int>> Edit(int id, AuthorUpdateDto author);
        Task<Response<int>> Delete(int id);
    }
}