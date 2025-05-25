using IBEXDATA.Models;



namespace DB

{

    public interface IFilesDB

    {

        Task<Magardoc> Add(Magardoc magardoc);

        //Task<Magardoc> AddMagardocAsync(Magardoc magardoc); 

        Task DeleteFileAsync(int id);

        Task<Magardoc[]> GetFilesByUniqIdAsync(string uniqId);

        Task<Magardoc> Update(int id, Magardoc magardoc);

        Task<TipeFile> GetIdFile(string fileName);

        Task<Magardoc?> GetFileMetadataAsync(string fileName);

    }

}