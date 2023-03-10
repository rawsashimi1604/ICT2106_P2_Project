using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.Domain.Common
{
    public abstract class AbstractDTOFactory<ResponseType, RequestType>
    {
        public abstract ResponseDTO<ResponseType> CreateResponseDTO (
            ResponseDTOType type,
            IEnumerable<IEntity> data,
            int statusCode,
            string message
         );

        public abstract RequestDTO<RequestType> CreateRequestDTO (
            RequestDTOType type,
            IEnumerable<IEntity> data
        );
    }
}
