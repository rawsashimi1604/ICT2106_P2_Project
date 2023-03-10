namespace SmartHomeManager.Domain.Common.DTOs
{
    /*
     * Used for defining what possible DTOs are available to send from the server side
     * 
     * Extend the enums as different types of requests are made available in the different controllers...
     */
    public enum ResponseDTOType
    {
        GET_NOTIFICATION,
        ADD_NOTIFICATION
    }
}
