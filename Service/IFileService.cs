using IBEXDATA.Models;



namespace Service

{

    public interface IFileService

    {

        Task<Magardoc> Add(Magardoc magardoc);

        Task DeleteFile(int id);

        Task<Magardoc[]> GetFilesByUniqId(string uniqId);

        Task<Magardoc> Update(int id, Magardoc magardoc);

        Task<TipeFile> GetIdFile(string fileName);

        Task<(Magardoc?, string)> GetFileAsync(string fileName);

    }

}