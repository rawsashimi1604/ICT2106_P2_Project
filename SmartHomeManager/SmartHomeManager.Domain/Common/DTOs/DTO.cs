namespace SmartHomeManager.Domain.Common.DTOs
{

    public abstract class DTO
    {
        public IEnumerable<IDataObjectDTO> Data { get; set; }

    }

}
