using Blazored.LocalStorage;

namespace ClientLibrary.Helpers
{
    public class LocalStorageService
    {
        public LocalStorageService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        private const string StorageKey = "authentication-key";
        private readonly ILocalStorageService localStorageService;

        public async Task<string> GetToken() => await localStorageService.GetItemAsStringAsync(StorageKey);
        public async Task SetToken(string item) => await localStorageService.SetItemAsStringAsync(StorageKey, item);
        public async Task RemoveToken() => await localStorageService.RemoveItemAsync(StorageKey);

    }
}
