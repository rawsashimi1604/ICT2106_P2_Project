using System;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
	public class AnalysisDTOFactory : AbstractDTOFactory
	{
        public override ResponseDTO CreateResponseDTO(ResponseDTOType type, IEnumerable<IEntity> data, int statusCode, string statusMessage)
        {
            switch (type)
            {
                case ResponseDTOType.DEVICE_GETBYACCOUNT:
                    return CreateGetDevicesDTO((IEnumerable<Device>)data, statusCode, statusMessage);

                default:
                    return null;
            }

           
        }

        public GetDevicesDTO CreateGetDevicesDTO(
            IEnumerable<Device> devices,
            int statusCode,
            string statusMessage
        )
        {
            List<GetDevicesDTOData> getDevices = new List<GetDevicesDTOData>();

            if (devices != null)
            {
                foreach (var device in devices)
                {
                    getDevices.Add(new GetDevicesDTOData
                    {
                        DeviceID = device.DeviceId,
                        DeviceName = device.DeviceName,
                    });

                }
            }
            return new GetDevicesDTO
            {
                Data = getDevices,
                Response = new ResponseInformation
                {
                    ServerMessage = statusMessage,
                    StatusCode = statusCode
                }
            };
        }
    }
}

