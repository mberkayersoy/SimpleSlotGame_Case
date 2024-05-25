public interface IDataService
{
    void SaveData<T>(string path,T data, bool isGameData = false);
    T LoadData<T>(string path, bool isGameData = false);
    bool CheckFileExistince(string path, bool isGameData = false);
}
