namespace SmartHomeManager.Domain.Common.DTOs
{

    public abstract class DTO<IDataObjectDTO>
    {
        public IEnumerable<IDataObjectDTO> Data { get; set; }

    }

}
