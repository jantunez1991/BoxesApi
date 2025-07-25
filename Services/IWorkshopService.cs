namespace BoxesApi.Services
{
    public interface IWorkshopService
    {
        Task<bool> IsValidPlaceAsync(int placeId);
    }
}
