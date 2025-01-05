using InventorySystem.Shared.Responses;

namespace InventorySystem.Shared.Interfaces.Services
{
    public interface IHelperService
    {
        GenericResponse<T> CreateErrorResponse<T>(string messageKey, string lang, Exception? exp = null);

        GenericResponse<T> CreateErrorResponse<T>(List<string> listMessage, string lang);

        GenericResponse<T> CreateErrorResponse<T>(List<ResponseMessage> listMessage, string lang);

        GenericResponse<T> CreateWarningResponse<T>(string messageKey, string lang);

        GenericResponse<T> CreateWarningResponse<T>(List<ResponseMessage> listMessage, string lang);

        GenericResponse<T> CreateSuccessResponse<T>(string messageKey, string lang, T data, long? totalRecords = null, long? page = null, long? pageSize = null);

        GenericResponse<T> CreateSuccessResponse<T>(T data, long? totalRecords = null, long? page = null, long? pageSize = null);

        GenericResponse<T> CreateSuccessResponse<T>(T data, long? page = null, long? pageSize = null);

        GenericResponse<T> CreateSuccessResponse<T>(T data);
    }
}
