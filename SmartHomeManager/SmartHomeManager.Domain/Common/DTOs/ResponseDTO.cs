namespace SmartHomeManager.Domain.Common.DTOs
{
    
    public class ResponseDTO<IDataObjectDTO> : DTO<IDataObjectDTO>
    {

        public ResponseInformationDTO Response { get; set; }

    }

}
